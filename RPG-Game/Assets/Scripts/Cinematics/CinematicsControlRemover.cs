using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject Player;


        private void Start() {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            Player = GameObject.FindGameObjectWithTag("Player");


        }

        void DisableControl(PlayableDirector pd)
        {
             
             Player.GetComponent<ActionScheduler>().CancelCurrentAction();
             Player.GetComponent<Playercontrol>().enabled = false;

        }

        void EnableControl(PlayableDirector pd){
            Player.GetComponent<Playercontrol>().enabled = true;
        }
        


    }

}