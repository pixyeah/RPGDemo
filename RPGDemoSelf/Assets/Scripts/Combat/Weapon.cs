using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName="Weapon",menuName="Weapons/Make New Weapon",order=0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AssetReference _prefabWeapon = null;
        [SerializeField] private AssetReference _animatorOverrider = null;
        
        [SerializeField] private float _weaponRange = 2;
        [SerializeField] private float _timeBetweenAttacks = 1f;        
        [SerializeField] private float _weaponDamage = 5f;
        [SerializeField] private bool _isRightHand = true;
        public void Spawn(Transform traLeftHand,Transform traRightHand, Animator animator)
        {
            
            if (!string.IsNullOrEmpty(_prefabWeapon.AssetGUID))
            {
                Addressables.InstantiateAsync(_prefabWeapon, _isRightHand ? traRightHand : traLeftHand);
                Addressables.LoadAssetAsync<AnimatorOverrideController>(_animatorOverrider).Completed +=
                    (result) =>
                    {

                        animator.runtimeAnimatorController = result.Result;
                    };
            }
        }

        public float GetDamage()
        {
            return _weaponDamage;
        }

        public float GetRange()
        {
            return _weaponRange;
        }

        public float GetTimeBetweenAttack()
        {
            return _timeBetweenAttacks;
        }
    }
}