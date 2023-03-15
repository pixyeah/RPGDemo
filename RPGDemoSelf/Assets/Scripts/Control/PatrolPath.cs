using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        
        private void OnDrawGizmos()
        {
            int count = transform.childCount;
            if(count <2 ) return;
            for (int i = 0; i < count-1; i++)
            {
                Gizmos.DrawLine(transform.GetChild(i).position,transform.GetChild(i+1).position);
                Gizmos.DrawSphere(transform.GetChild(i).position,0.1f);
            }
            Gizmos.DrawLine(transform.GetChild(count-1).position,transform.GetChild(0).position);
            Gizmos.DrawSphere(transform.GetChild(count-1).position,0.1f);
        }

        public int GetNextIndex(int i)
        {
            return i < transform.childCount-1 ? i + 1 : 0;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public float GetCurSpeed()
        {
            return 0;
        }
    }

    
}
