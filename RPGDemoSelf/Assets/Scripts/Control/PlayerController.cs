using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {

            MouseInteract();
        }

        private void MouseInteract()
        {
            Ray lastHitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (CombatInteract(lastHitRay))
            {
                return;
            }
            if (MovementInteract(lastHitRay))
            {
                return;
            }
            //print("nothing to do");
        }

        private bool CombatInteract(Ray ray)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (var h in hits)
            {
                if (h.transform.CompareTag("Enemy"))
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        CombatTarget cbTar = h.transform.GetComponent<CombatTarget>();
                        if (cbTar != null)
                        {
                            GetComponent<Fighter>()?.Attack(cbTar);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private bool MovementInteract(Ray ray)
        {
            RaycastHit lastHitRayCast;
            if (Physics.Raycast(ray, out lastHitRayCast))
            {
                if (Input.GetMouseButton(1))
                {
                    GetComponent<Mover>().MoveTo(lastHitRayCast.point);
                }
                return true;
            }
            return false;
        }
    }


}