using UnityEngine;

namespace RPG.Attributes
{
    class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas canvas;

        void Update()
        {
            float healthFraction = healthComponent.GetHitPoints() / healthComponent.GetMaxHitPoints();
            
            if(Mathf.Approximately(healthFraction, 1) || Mathf.Approximately(healthFraction, 0))
            {
                canvas.enabled = false;
            }
            else
            {
                canvas.enabled = true;
                foreground.localScale = new Vector3(healthFraction, 1, 1);
            }
        }
    }
}
