using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HeathDisplay : MonoBehaviour
    {
        Health health;
        Text text;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            text = GetComponent<Text>();
        }

        private void Update()
        {
            text.text = string.Format("{0:0} / {1:0}", health.GetHitPoints(), health.GetMaxHitPoints());
        }
    }

}