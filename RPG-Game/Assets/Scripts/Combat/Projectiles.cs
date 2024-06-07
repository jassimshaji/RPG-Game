using RPG.Core;
using UnityEngine;


namespace RPG.Combat
{
    public class Projectiles : MonoBehaviour
    {
        Health target = null;
        [SerializeField] float projectileSpeed = 3f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject impactOfProjectile = null;
        float damage = 0;

        private void Start() {
            transform.LookAt(GetAimLocation());
        }
        private void Update() {

            if(target == null) return; 

            if(isHoming && !target.IsDead()){
            transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

            
        }

        public void SetTarget(Health Target, float damage){
            this.target = Target;
            this.damage = damage;


        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null){
                return target.transform.position;   
            }
  
            
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
           
        }

        private void OnTriggerEnter(Collider other) {
            if(other.GetComponent<Health>() != target) return;
            if(target.IsDead())return;
            target.TakeDamage(damage);

            if(impactOfProjectile != null){
                Instantiate(impactOfProjectile, GetAimLocation(), transform.rotation);
            }
            Destroy(gameObject);
        }
    }
    
}
