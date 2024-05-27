using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    
    public class Weaponpickup : MonoBehaviour
    {
        [SerializeField] Weapon swordWeapon = null;
    
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player"){
                other.GetComponent<Fighter>().EquipWeapon(swordWeapon);
                Destroy(gameObject);
            }
            
        }
    }
}
