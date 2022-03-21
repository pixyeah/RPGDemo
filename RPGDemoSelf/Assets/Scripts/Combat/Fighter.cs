using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float _weaponRange = 2;

        private Transform _target = null;
        private void Update()
        {
            if (_target != null)
            {
                if (Vector3.Distance(transform.position, _target.position) > _weaponRange)
                {
                    GetComponent<Mover>().MoveTo(_target.transform.position);
                }
                else
                {
                    GetComponent<Mover>().Stop();
                }
            }
        }
        public void Attack(CombatTarget tar)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = tar.transform ;
        }
        public void CancelAttack()
        {
            _target = null;
        }

        public void Cancel()
        {
            CancelAttack();
        }
    }

}