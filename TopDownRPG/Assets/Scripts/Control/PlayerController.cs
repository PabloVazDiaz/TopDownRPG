using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<CombatTarget>())
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        GetComponent<Fighter>().Attack(hit.collider.GetComponent<CombatTarget>());
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
                    mover.StartMoveAction(hit.point);
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
