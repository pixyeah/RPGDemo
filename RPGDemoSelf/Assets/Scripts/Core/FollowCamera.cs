using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;
    
        void LateUpdate()
        {
            transform.position = _target.position;
        }
    }

}