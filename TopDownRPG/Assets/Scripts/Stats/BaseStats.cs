using UnityEngine;
using System;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,10)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject LvlUpFX = null;
        [SerializeField] bool shouldUseModifiers = false;

        public event Action onLevelUp; 
        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                Instantiate(LvlUpFX, transform);
                currentLevel = newLevel;
                onLevelUp();
            }
        }

        public float GetStat( Stat stat)
        {
            float baseStat = progression.GetStat(stat, characterClass, GetLevel());
            return (baseStat + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }


        private float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float totalModifier = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifiers in provider.GetAdditiveModifiers(stat))
                {
                    totalModifier += modifiers;
                }
            }
            return totalModifier;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float totalPercentage = 0;
            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    totalPercentage += modifier;
                }
            }
            return totalPercentage;
        }
        
        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null)
                return startingLevel;
            float currentXP = experience.GetExperience();

            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
        
    }

}