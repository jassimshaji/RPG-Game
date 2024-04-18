using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class TriggerCinematics : MonoBehaviour
    {
        bool alreadyTriggered = false;
        private void OnTriggerEnter(Collider Player) {
            
            if(!alreadyTriggered && Player.gameObject.tag == "Player"){
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
                
            }
        }


    }

}