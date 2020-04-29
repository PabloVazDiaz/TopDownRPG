
using RPG.Attributes;
using RPG.Control;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] float respawnTime = 5f;
        [SerializeField] CursorType cursorType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PickUp(other.gameObject);
            }
        }

        private void PickUp(GameObject subject)
        {
            if (weapon != null) 
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
            if (healthToRestore > 0)
                subject.GetComponent<Health>().Heal(healthToRestore);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true);
        }

        private void ShowPickUp(bool shouldShow)
        {
            GetComponent<CapsuleCollider>().enabled = shouldShow;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(callingController.gameObject);
            }
            return true;
        }

        
        public CursorType GetCursorType()
        {
            return cursorType;
        }
    }
}
