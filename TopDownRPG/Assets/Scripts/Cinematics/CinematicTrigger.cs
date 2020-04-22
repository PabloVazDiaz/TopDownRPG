using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isAlreadyPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!isAlreadyPlayed && other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                isAlreadyPlayed = true;
            }
        }
    }

}