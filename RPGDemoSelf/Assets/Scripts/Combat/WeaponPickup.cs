using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private AssetReference _weaponRef = null;
    private void OnTriggerEnter(Collider other)
    {
        
        print(other.tag);
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            other.GetComponent<Fighter>().EquipWeapon(_weaponRef);
            
            Destroy(gameObject);
        }
    }
}
