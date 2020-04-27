using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        Health health;
        Text text;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            text = GetComponent<Text>();
        }

        private void Update()
        {
            health = fighter.GetTarget();
            if (health != null)
                text.text = string.Format("{0:0} / {1:0}", health.GetHitPoints(), health.GetMaxHitPoints());
        }
    }

}