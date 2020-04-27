using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float hitPoints = 100f;

        bool isDead = false;

        private void Start()
        {
            hitPoints = GetComponent<BaseStats>().GetHealth();
        }


        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(float damage)
        {
            hitPoints = Mathf.Max(hitPoints - damage, 0);
            if (!isDead && hitPoints <= 0 )
            {
                Die();
            }
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
        }

        public object CaptureState()
        {
            return hitPoints;
        }

        public float GetPercentage()
        {
            return hitPoints / GetComponent<BaseStats>().GetHealth() * 100;
        }

        public void RestoreState(object state)
        {
            hitPoints = (float)state;
            TakeDamage(0);
        }
    }
}
