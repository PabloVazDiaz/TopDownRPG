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
        float hitPoints = -1f;
        BaseStats baseStats;

        bool isDead = false;

        private void Start()
        {
            baseStats = GetComponent<BaseStats>();
            if (hitPoints <= 0f) 
                hitPoints = baseStats.GetStat(Stat.Health);
            baseStats.onLevelUp += RegenerateHealth;

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
            float XPReward = baseStats.GetStat(Stat.ExperienceReward);
            Experience experience = instigator.GetComponent<Experience>();
            if (experience != null) 
                experience.GainExperience(XPReward);
        }

        private void RegenerateHealth()
        {
            hitPoints = baseStats.GetStat(Stat.Health);
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
