using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float hitPoints = 100f;

        bool isDead = false;

        private void Start()
        {
            hitPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }


        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            hitPoints = Mathf.Max(hitPoints - damage, 0);
            if (!isDead && hitPoints <= 0 )
            {
                Die();
                AwardExperience(instigator);
            }
        }


        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<NavMeshAgent>().enabled = false;
        }

        private void AwardExperience(GameObject instigator)
        {
            float XPReward = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            Experience experience = instigator.GetComponent<Experience>();
            if (experience != null) 
                experience.GainExperience(XPReward);
        }

        public object CaptureState()
        {
            return hitPoints;
        }

        public float GetPercentage()
        {
            return hitPoints / GetComponent<BaseStats>().GetStat(Stat.Health) * 100;
        }

        public void RestoreState(object state)
        {
            hitPoints = (float)state;
            if (hitPoints <= 0)
                Die();
        }
    }
}
