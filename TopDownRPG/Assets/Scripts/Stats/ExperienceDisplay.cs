using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        Text text;

        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            text = GetComponent<Text>();
        }

        private void Update()
        {
            text.text = string.Format("{0:0}", experience.GetExperience());
        }
    }

}
