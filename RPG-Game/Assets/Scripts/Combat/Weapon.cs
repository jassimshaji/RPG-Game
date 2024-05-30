
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float WeaponDamage = 5f;
        [SerializeField] float WeaponRange = 2f;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectiles projectile = null;

        public void Spawn(Transform righthand, Transform lefthand, Animator animator){

            if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(righthand, lefthand);
                Instantiate(equippedPrefab, handTransform);
            }

            if (animatorOverride != null){
            animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public void SpawnProjectile(Transform righthand, Transform lefthand, Health target){
            Projectiles spawnedProjectile = Instantiate(projectile, GetTransform(righthand, lefthand).position, Quaternion.identity);
            spawnedProjectile.SetTarget(target);

        }



        public bool HasProjectile(){
            return projectile != null;
        }

        public float GetDamage(){
            return WeaponDamage;
        }

        public float GetRange(){
            return WeaponRange;
        }

        private Transform GetTransform(Transform righthand, Transform lefthand)
        {
            Transform handTransform;
            if (isRightHand) handTransform = righthand;
            else handTransform = lefthand;
            return handTransform;
        }

    
    
}
}