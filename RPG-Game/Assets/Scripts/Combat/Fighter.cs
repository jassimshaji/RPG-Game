using RPG.Movement;
using RPG.Core;
using UnityEngine;
using System;




namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction

    {

        [SerializeField] float WeaponRange = 2f;
        [SerializeField] float TimeBetweenAttacks = 1f;
        [SerializeField] float Damage = 5f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        Health target;
        float TimeSinceLastAttack = Mathf.Infinity;

        Mover Move;
        private void Start()
        {
            
            Move = GetComponent<Mover>();
            if(weaponPrefab == null || handTransform == null ) return;
            SpawnWeapon();
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = weaponOverride;
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


        private void SpawnWeapon()
        {
            Instantiate(weaponPrefab, handTransform);
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
            target.TakeDamage(Damage);
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < WeaponRange;
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