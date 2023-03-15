using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        public Action _dieAction = null;

        private Animator _animator;

        private bool _isAlive = true;
        private void Awake()
        {

            _animator = GetComponent<Animator>();
        }

        public bool IsDead()
        {
            return !_isAlive;
        }

        public void TakeDamage(float damage)
        {
            if (!_isAlive) return;
            healthPoints = Mathf.Max(0, healthPoints - damage);
            if (healthPoints == 0)
            {
                Die();
            }

            // print("take damage ,curhealth : "+healthPoints);
        }

        private void Die()
        {
            _isAlive = false;
            _animator.SetTrigger(Animator.StringToHash(Constants.ANIMATION_PARA_CHAR_DIE));
            GetComponent<ActionScheduler>().CancelCurAction();
            
            _dieAction?.Invoke();
        }
    }

}