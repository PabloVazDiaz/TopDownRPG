using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using UnityEngine;
using RPG.Resources;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {

        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] string defaultWeaponName = "Unarmed";

        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon;
        Animator animator;

        private void Start()
        {
            if (currentWeapon == null) 
                EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (CanAttack(target))
            {
                if (!GetIsInRange())
                {
                    GetComponent<Mover>().MoveTo(target.transform.position, 1f);
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            Health healthToTest = combatTarget.GetComponent<Health>();
            return healthToTest != null && !healthToTest.IsDead();
        }
        public bool CanAttack(Health healthToTest)
        {
            return healthToTest != null && !healthToTest.IsDead();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                animator.ResetTrigger("cancelAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }


        public Health GetTarget()
        {
            return target;
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        //Animation Event
        void Hit()
        {
            if (target != null) 
                target.TakeDamage(gameObject, GetComponent<BaseStats>().GetStat(Stat.Damage));
        }

        //Animation Event
        void Shoot()
        {
            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, GetComponent<BaseStats>().GetStat(Stat.Damage));
        }

        public void Cancel()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("cancelAttack");
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetWeaponDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat== Stat.Damage)
            {
                yield return currentWeapon.GetPercentageBonus();
            }
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

    }
}
