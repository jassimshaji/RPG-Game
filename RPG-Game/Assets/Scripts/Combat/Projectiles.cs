using RPG.Core;
using UnityEngine;


namespace RPG.Combat
{
    public class Projectiles : MonoBehaviour
    {
        Health target = null;
        [SerializeField] float projectileSpeed = 3f;
        private void Update() {

            if(target == null) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

            
        }

        public void SetTarget(Health Target){
            this.target = Target;


        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null){
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2; 
        }
    }
    
}
