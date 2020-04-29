using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {

        [SerializeField] TakeDamageEvent takeDamage;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {

        }

        [SerializeField] UnityEvent onDie;

        LazyValue<float> hitPoints;
        BaseStats baseStats;

        bool isDead = false;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
            hitPoints = new LazyValue<float>(GetInitialHitpoints);
        }
            
        private float GetInitialHitpoints()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void Start()
        {
            hitPoints.ForceInit();
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }
            

        public bool IsDead()
        {
            return isDead;
        }


        public void TakeDamage(GameObject instigator, float damage)
        {
            hitPoints.value = Mathf.Max(hitPoints.value - damage, 0);
            takeDamage.Invoke(damage);
            if (!isDead && hitPoints.value <= 0 )
            {
                onDie.Invoke();
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
            hitPoints.value = baseStats.GetStat(Stat.Health);
        }

        public float GetHitPoints()
        {
            return hitPoints.value;
        }

        public float GetMaxHitPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }


        public float GetPercentage()
        {
            return hitPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health) * 100;
        }

        public object CaptureState()
        {
            return hitPoints.value;
        }


        public void RestoreState(object state)
        {
            hitPoints.value = (float)state;
            if (hitPoints.value <= 0)
                Die();
        }
    }
}
