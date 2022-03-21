using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _traTarget = null;

        private void LateUpdate()
        {
            transform.position = _traTarget.position;
        }
    }
}