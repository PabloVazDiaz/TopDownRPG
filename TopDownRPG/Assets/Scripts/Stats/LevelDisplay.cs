using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        Text text;

        private void Awake()
        {
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
            text = GetComponent<Text>();
        }

        private void Update()
        {
            text.text = string.Format("{0:0}", baseStats.GetLevel());
        }
    }

}
