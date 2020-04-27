using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaulSaveFile = "save";

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaulSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
           
            yield return fader.FadeIn(0.2f);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaulSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaulSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaulSaveFile);
        }
    }

}