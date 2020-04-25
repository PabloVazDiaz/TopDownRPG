using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;

        Health target;
        float damage = 0;


        // Update is called once per frame
        void Update()
        {
            if (target == null)
                Destroy(gameObject);
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Health>() == target)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
