using RPG.Core;
using RPG.Move;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float _weaponRange = 2;
        [SerializeField] private float _timeBetweenAttacks = 1f;        
        [SerializeField] private float _weaponDamage = 5f;

        private Transform _target;
        private ActionScheduler _actionScheduler;
        private Mover _mover;
        private Animator _animator;
        private Health _targetHealth;

        private float _timeSinceLastAttack = 10;

        private void Awake()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if(_target == null) return;
            if (_targetHealth.IsDead())
            {
                Cancel();
                return;
            }

            if (Vector3.Distance(_target.position, transform.position) <= _weaponRange)
            {
                _mover.Cancel();
                if (_timeSinceLastAttack >= _timeBetweenAttacks)
                {
                    _timeSinceLastAttack = 0;
                    _animator.SetTrigger(Animator.StringToHash(Constants.ANIMATION_PARA_CHAR_ATTACK));
                    // print("set attack trigger : i 'm gonna attack");
                }

            }
            else
            {
                _mover.MoveTo(_target.position,1f);
            }
        }

        public void Attack(GameObject target)
        {
            if (_target != null && target == _target.gameObject){ print("重复 不用管");return;}
            _animator.ResetTrigger(Animator.StringToHash(Constants.ANIMATION_PARA_CHAR_STOPATTACK));
            _actionScheduler.StartAction(this);
            _target = target.transform;
            _targetHealth = target.GetComponent<Health>();
            // print(" attack");
        }

        public bool CanAttack(GameObject obj)
        {
            return !obj.GetComponent<Health>().IsDead();
        }


        /// <summary>
        /// Attack Animation Event
        /// </summary>
        void Hit()
        {
            if (_targetHealth != null)
            {
                _targetHealth.TakeDamage(_weaponDamage);
            }
        }
        public void Cancel()
        {
            _target = null;
            _targetHealth = null;
            _animator.SetTrigger(Animator.StringToHash(Constants.ANIMATION_PARA_CHAR_STOPATTACK));
            
            _mover.Cancel();
            
            // print(gameObject.name+"cancel attack");
        }

    }

    
}