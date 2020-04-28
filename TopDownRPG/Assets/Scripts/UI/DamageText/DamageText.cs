using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        
        public void setTextNumber(float amount)
        {
            GetComponentInChildren<Text>().text = amount.ToString();
        }

    }

}