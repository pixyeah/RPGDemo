using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Move;
using UnityEngine;

namespace RPG.Control
{
    public enum AIStatus
    {
        Guard,
        Attack,
        Wander
    }

    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5;
        [SerializeField] private float timeLostTargetWander = 2;
        [SerializeField] private float waypointTolerance = 1;
        [SerializeField] private float waypointDwellTime = 3;
        [Range(0,1)]
        [SerializeField] private float patrolSpeedFraction = 0.2f;
        [SerializeField] private PatrolPath _patrolPath;
        private Fighter _fighter;
        private Health _health;

        private Transform _player;
        private Transform _self;
        private Vector3 _guardPosition;
        private float _timeSinceLastSawPlayer = 1000;
        private float _timeSinceLastWaypoint = 1000;
        private int _curWayPointIndex = 0;
        private AIStatus _curStatus = AIStatus.Guard;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _player = GameObject.FindWithTag(Constants.TAG_PLAYER).transform;
            _self = transform;
            _guardPosition = _self.position;
        }

        private void Update()
        {
            if (_health.IsDead()) return;

            if (_curStatus != AIStatus.Attack)
            {
                if (SearchPlayer())
                {
                    AttackBehaviour();
                }
                else if(_curStatus == AIStatus.Wander)
                {
                    WanderBehaviour();
                }
                else if (_curStatus == AIStatus.Guard)
                {
                    GuardBehaviour();
                }
            }
            else
            {
                if (!SearchPlayer())
                {
                    _fighter.Cancel();
                    _curStatus = AIStatus.Wander;
                }
            }

            /*
            //会重复调用
            if (SearchPlayer())
            {
                AttackBehaviour();
            }
            else
            {
                if(_curStatus == AIStatus.Wander)
                {
                    WanderBehaviour();
                }
                else if (_curStatus == AIStatus.Guard)
                {
                    GuardBehaviour();
                }else if (_curStatus == AIStatus.Attack)
                {
                    _fighter.Cancel();
                    _curStatus = AIStatus.Wander;
                }

            }*/
        }

        private bool SearchPlayer()
        {
            return (Vector3.Distance(_self.position, _player.position) < chaseDistance) && _fighter.CanAttack(_player.gameObject);
        }

        private void AttackBehaviour()
        {
            _curStatus = AIStatus.Attack;
            transform.LookAt(_player.transform);
            _fighter.Attack(_player.gameObject);
            print(gameObject.name+" Player Detected! Chasing! ");
        }

        private void WanderBehaviour()
        {
            //待优化
            GetComponent<ActionScheduler>().CancelCurAction();
            _timeSinceLastSawPlayer += Time.deltaTime;
            if (_timeSinceLastSawPlayer >= timeLostTargetWander)
            {
                GuardBehaviour();
            }
        }

        private void GuardBehaviour()
        {
            _curStatus = AIStatus.Guard;
            _timeSinceLastSawPlayer = 0;
            Vector3 nextMovePosition = _guardPosition;
            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceLastWaypoint += Time.deltaTime;
                    if (_timeSinceLastWaypoint >= waypointDwellTime)
                    {
                        _timeSinceLastWaypoint = 0;
                        CycleWaypoint();     
                    }
                }
                nextMovePosition = GetCurrentWayPoint();
            }
            GetComponent<Mover>().StartMoveAction(nextMovePosition, patrolSpeedFraction);
        }

        private bool AtWaypoint()
        {
            float distance = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distance < waypointTolerance;
        }

        private Vector3 GetCurrentWayPoint()
        {
            return _patrolPath.GetWayPoint(_curWayPointIndex);
        }

        private void CycleWaypoint()
        {
            _curWayPointIndex = _patrolPath.GetNextIndex(_curWayPointIndex);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
}