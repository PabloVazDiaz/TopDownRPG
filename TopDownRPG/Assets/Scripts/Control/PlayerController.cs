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
            InteractWithMovement();
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<CombatTarget>())
                {
                    if(Input.GetButtonDown("Fire1"))
                        GetComponent<Fighter>().Attack();
                    return true;
                }

            }
            return false;
        }

        private void InteractWithMovement()
        {
            if (Input.GetButton("Fire1"))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            if (Physics.Raycast(GetMouseRay(), out hit))
                mover.MoveTo(hit.point);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
