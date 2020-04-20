using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float hitPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            hitPoints = Mathf.Max(hitPoints - damage, 0);
            if (!isDead && hitPoints <= 0 )
            {
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            }
        }
    }
}
