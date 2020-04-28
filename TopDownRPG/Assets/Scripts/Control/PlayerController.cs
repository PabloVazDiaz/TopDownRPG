using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Health health;


        private void Awake()
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }
        
        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target!=null)
                {
                    Fighter fighter = GetComponent<Fighter>();
                    if (!fighter.CanAttack(target.gameObject)) continue;
                    if (Input.GetButton("Fire1"))
                    {
                        fighter.Attack(hit.transform.gameObject);
                    }
                        return true;
                }
            }
            return false;
        }



        private bool InteractWithMovement()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
            {
                if(Input.GetButton("Fire1"))
                    mover.StartMoveAction(hit.point, 1f);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
