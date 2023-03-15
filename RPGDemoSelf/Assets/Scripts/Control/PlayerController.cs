using RPG.Combat;
using RPG.Core;
using RPG.Move;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera _mainCamera = null;
        private Fighter _fighter = null;
        private Health _health;


        public void Awake()
        {
            _mover = GetComponent<Mover>();
            _mainCamera = Camera.main;
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private Ray GetMouseRay()
        {
            return _mainCamera.ScreenPointToRay(Input.mousePosition);
        }

        void Update()
        {
            if (_health.IsDead()) return;
            if(InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private void PickPlayMouseClick()
        {
            
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out var hit))
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hit.point,1f);
                    // if (hit.collider.CompareTag("Terrain"))
                    // {
                    // }
                }

                return true;
            }

            return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = new RaycastHit[5];
            int size = Physics.RaycastNonAlloc(GetMouseRay(), hits);
            for (int i = 0; i < size; i++)
            {
                //待优化
                if (hits[i].transform.TryGetComponent( out CombatTarget target))
                {
                    if(target == null || !target.GetComponent<Fighter>().CanAttack(target.gameObject))
                        continue;
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        transform.LookAt(target.transform);
                        _fighter.Attack(target.gameObject);
                    }

                    return true;
                }
            }

            return false;
        }
    }

}