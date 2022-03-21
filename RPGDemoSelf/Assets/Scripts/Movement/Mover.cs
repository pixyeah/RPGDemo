using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _playAgent = null;
        private Animator _playerAnim = null;
        void Awake()
        {
            _playAgent = GetComponent<NavMeshAgent>();
            _playerAnim = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _playAgent.destination = destination;
            _playAgent.isStopped = false;
        }
        public void Stop()
        {
            _playAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {
            Vector3 v = _playAgent.velocity;
            _playerAnim.SetFloat("forwardSpeed", transform.InverseTransformDirection(v).z);
        }

        public void Cancel()
        {
            _playAgent.isStopped = true;
        }
    }


}