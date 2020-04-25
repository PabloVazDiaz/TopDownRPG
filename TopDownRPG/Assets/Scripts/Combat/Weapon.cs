using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/ New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] float weaponRange;
        [SerializeField] float weaponDamage;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            Transform handTransform = isRightHanded ? rightHand : leftHand;
            if (weaponPrefab != null)
                Instantiate(weaponPrefab, handTransform);
            if (animatorOverride != null) 
                animator.runtimeAnimatorController = animatorOverride;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    }
}
