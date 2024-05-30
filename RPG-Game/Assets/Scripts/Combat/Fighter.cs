using RPG.Movement;
using RPG.Core;
using UnityEngine;
using System;




namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction

    {

        
        [SerializeField] float TimeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        
        Health target;
        float TimeSinceLastAttack = Mathf.Infinity;

        Mover Move;

        Weapon currentWeapon = null;
        private void Start()
        {

            Move = GetComponent<Mover>();
            EquipWeapon(defaultWeapon);

        }


        void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetInRange())
            {
                Move.MoveTo(target.transform.position, 1f);
            }
            else
            {
                Move.Cancel();
                AttackBehaviour();
            }

        }




        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return combatTarget != null && !targetToTest.IsDead();

        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();

        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            currentWeapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }


        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (TimeSinceLastAttack > TimeBetweenAttacks)
            {
                //Here Hit() will get triggered
                AttackTrigger();
                TimeSinceLastAttack = 0;
            }

        }

        private void AttackTrigger()
        {
            GetComponent<Animator>().ResetTrigger("Stopattack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        void Hit()
        {
            if (target == null) return;

            if(currentWeapon.HasProjectile()){
                currentWeapon.SpawnProjectile(rightHandTransform, leftHandTransform, target);
            }
            else{
            target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        void Shoot(){
            Hit();

        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }





        public void Cancel()
        {
            StopAttack();
            target = null;
            Move.Cancel();

        }

        public void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("Stopattack");
        }
    }



}