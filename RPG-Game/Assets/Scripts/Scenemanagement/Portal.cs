using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    //Used to identify the portal connected to each other portal across the scenes
    enum DestinationIdentifier
    {
        A, B, C, D, E
    }
   
   
   public class Portal : MonoBehaviour {
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform SpawnPos;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        //other is the "other" gameobject which comes in contact with the collider 
        private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      StartCoroutine(Transition());
    }
   
   }
   //Transition() is the coroutine 

   private IEnumerator Transition(){
    if(sceneToLoad < 0){
        Debug.LogError("SceneToLoad is not set...");
        yield break;
    }

    
    DontDestroyOnLoad(gameObject);

    Fader fader = FindObjectOfType<Fader>();

    yield return fader.Fadeout(fadeOutTime);

    SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
    wrapper.Save();

    yield return SceneManager.LoadSceneAsync(sceneToLoad);

    wrapper.Load();

    Portal otherportal = GetOtherPortal();

    PlayerUpdate(otherportal);

    wrapper.Save();
   

    yield return new WaitForSeconds(fadeWaitTime);

    yield return fader.Fadein(fadeInTime);
            

    Destroy(gameObject); 

   }

        private void PlayerUpdate(Portal otherportal)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<NavMeshAgent>().Warp(otherportal.SpawnPos.position);
            Player.transform.rotation = otherportal.SpawnPos.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>()){
               if(portal == this) continue;
               if(portal.destination != destination) continue;
               return portal;

            }
            return null;
        }
    } 
    
}
