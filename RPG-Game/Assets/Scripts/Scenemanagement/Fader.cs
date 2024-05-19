using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvas;
        private void Start() {
            canvas = GetComponent<CanvasGroup>();
              
        }

        public void FadeOutImmediately(){
            canvas.alpha = 1;
        }



       public IEnumerator Fadeout(float time){

            while(canvas.alpha < 1){
                canvas.alpha += Time.deltaTime / time;
                yield return null;

            }
            
            

        }

        public IEnumerator Fadein(float time)
        {

            while (canvas.alpha > 0)
            {
                canvas.alpha -= Time.deltaTime / time;
                yield return null;

            }
            

        }

    }
}
