using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

namespace RPG.SceneManagement
{
    
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            A, B, C, D
        }

        [SerializeField] DestinationIdentifier destination;
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 1f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);
            SavingWrapper wraper = FindObjectOfType<SavingWrapper>();

            wraper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            wraper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            wraper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }
            return null;
        }
    }

}