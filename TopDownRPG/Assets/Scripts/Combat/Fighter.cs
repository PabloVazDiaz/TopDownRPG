using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform target;
        [SerializeField] float weaponRange;

        private void Update()
        {
            if (target != null)
            {
                if (!GetIsInRange())
                {
                    GetComponent<Mover>().MoveTo(target.position);
                    print("moviendo");
                }
                else
                {
                    GetComponent<Mover>().Stop();
                    print("parando");
                }
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
