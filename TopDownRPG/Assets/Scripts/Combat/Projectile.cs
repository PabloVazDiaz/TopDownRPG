using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] DestroyOnHit = null;
        [SerializeField] float lifeAfterHit = 0.2f;

        Health target;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
                Destroy(gameObject);
            if (isHoming && !target.IsDead()) 
                transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
                return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        public void setTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, maxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (target.IsDead()) return;
            if (other.GetComponent<Health>() != target) return;

            target.TakeDamage(damage);
            speed = 0;
            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            
            foreach (GameObject toDestroy in DestroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterHit);
        }
    }
}
