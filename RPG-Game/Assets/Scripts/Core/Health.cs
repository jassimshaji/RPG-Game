using UnityEngine;
namespace RPG.Core
{
    
    public class Health : MonoBehaviour
    {
        
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead(){
            return isDead;
        }
        public void TakeDamage(float Damage)
        {
            healthPoints = Mathf.Max(healthPoints - Damage, 0);
            if (healthPoints == 0)
            {
                Dead();
            }

        }

        private void Dead()
        {
            if(isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }

}