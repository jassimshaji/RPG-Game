using System.Reflection;
using JetBrains.Annotations;
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

        public void Spawn(Transform handTransform, Animator animator){

            if(equippedPrefab != null){
            Instantiate(equippedPrefab, handTransform);
            }

            if(animatorOverride != null){
            animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public float GetDamage(){
            return WeaponDamage;
        }

        public float GetRange(){
            return WeaponRange;
        }

    
    
}
}