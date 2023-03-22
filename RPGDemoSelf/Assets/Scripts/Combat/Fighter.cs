using System;
using RPG.Core;
using RPG.Move;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {

        [SerializeField] private Transform _traWeaponLeftHolder = null;
        [SerializeField] private Transform _traWeaponRightHolder = null;
        [SerializeField] private AssetReference _weaponRef = null;
        
        

        private Transform _target;
        private ActionScheduler _actionScheduler;
        private Mover _mover;
        private Animator _animator;
        private Health _targetHealth;
        private Weapon _currentWeapon;
        private float _height;

        private bool _isInit = false;

        private float _timeSinceLastAttack = 10;

        private void Awake()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
            _mover = GetComponent<Mover>();
            _height = GetComponent<CapsuleCollider>().height;  //TODO 待优化
        }

        private void Start()
        {
            if (!this) return;
            EquipWeapon(_weaponRef);
        }

        public void EquipWeapon(AssetReference weaponRef)
        {
            if (string.IsNullOrEmpty(weaponRef.AssetGUID)) return;

            Addressables.LoadAssetAsync<Weapon>(weaponRef).Completed +=
                (result) =>
                {
                    if (result.IsDone && result.Result != null)
                    {
                        _isInit = true;
                        _currentWeapon = result.Result;
                        _currentWeapon.Spawn(_traWeaponLeftHolder, _traWeaponRightHolder, _animator);
                    }
                };
        }

        private void Update()
        {
            if (!_isInit) return;
            
            _timeSinceLastAttack += Time.deltaTime;
            if(_target == null) return;
            if (_targetHealth.IsDead())
            {
                Cancel();
                return;
            }

            if (Vector3.Distance(_target.position, transform.position) <= _currentWeapon.GetRange())
            {
                _mover.Cancel();
                if (_timeSinceLastAttack >= _currentWeapon.GetTimeBetweenAttack())
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

        public Vector3 GetAimPosition()
        {
            return transform.position + Vector3.up * _height / 2;
        }


        /// <summary>
        /// Attack Animation Event
        /// </summary>
        void Hit()
        {
            if (_targetHealth != null)
            {
                _targetHealth.TakeDamage(_currentWeapon.GetDamage());
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