using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class TriggerCinematics : MonoBehaviour,ISaveable
    {
        bool alreadyTriggered;



        private void OnTriggerEnter(Collider Player) {
            
            if(!alreadyTriggered && Player.gameObject.tag == "Player"){
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
                
            }
        }
        public object CaptureState()
        {
            return alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            alreadyTriggered = (bool) state;
        }


    }

}