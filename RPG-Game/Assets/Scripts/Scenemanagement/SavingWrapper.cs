using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSavingFile = "save";
        [SerializeField] float fadeInTime = 0.2f;

        IEnumerator  Start() {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();

            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSavingFile);
            yield return fader.Fadein(fadeInTime);
            
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.S)){
                Save();
            }

        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSavingFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSavingFile);
        }
    }
    
}
