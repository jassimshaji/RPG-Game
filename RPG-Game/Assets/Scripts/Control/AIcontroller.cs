using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;


namespace RPG.Control
{
    public class AIcontroller : MonoBehaviour
    {


        GameObject player;
        Fighter enemy;
        Health health;
        Vector3 guardPosition;
        Vector3 guardRotation;
        Mover mover;
        Patrolroute route;
        ActionScheduler actionScheduler;
        [SerializeField] float ChaseDistance = 5f;
        [SerializeField] float SuspicousTime = 3f;
        [SerializeField] Patrolroute patrolroute;
        [SerializeField] float Checkpointtolerance = 1f;
        [SerializeField] float Dweltime = 3f;
        [Range(0, 1)]
        [SerializeField] float patrolspeedfraction = 1f;
        int Currentcheckpointindex = 0;

        float TimeSinceLastSawPlayer = Mathf.Infinity;
        float TimeSinceLastDwel = Mathf.Infinity;


        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GetComponent<Fighter>();
            health = GetComponent<Health>();
            guardPosition = transform.position;
            guardRotation = transform.eulerAngles;
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();


        }


        private void Update()
        {

            if (health.IsDead()) return;

            if (CanChase() && enemy.CanAttack(player))
            {

                AttackBehaviour();
            }


            //Suspicous
            else if (TimeSinceLastSawPlayer < SuspicousTime)
            {
                SuspiciousBehaviour();
            }

            else
            {
                PatrolBehaviour();

            }
            UpdateTimers();


        }




        private void UpdateTimers()
        {
            TimeSinceLastSawPlayer += Time.deltaTime;
            TimeSinceLastDwel += Time.deltaTime;

        }

        private void PatrolBehaviour()
        {
            Vector3 Nextpathposition = guardPosition;
            if (patrolroute != null)
            {
                if (AtCheckPoint())
                {

                    CyclePath();
                }
                Nextpathposition = GetCurrentRoute();
            }
            if (TimeSinceLastDwel > Dweltime)
            {
                mover.StartMoveAction(Nextpathposition, patrolspeedfraction);

            }

        }

        private Vector3 GetCurrentRoute()
        {
            return patrolroute.GetRoutePoint(Currentcheckpointindex);

        }

        private void CyclePath()
        {
            TimeSinceLastDwel = 0;
            Currentcheckpointindex = patrolroute.GetNextIndex(Currentcheckpointindex);

        }

        private bool AtCheckPoint()
        {
            float DistanceToCheckPoint = Vector3.Distance(transform.position, GetCurrentRoute());
            return DistanceToCheckPoint < Checkpointtolerance;

        }

        private void SuspiciousBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            TimeSinceLastSawPlayer = 0;
            enemy.Attack(player);
        }

        private bool CanChase()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < ChaseDistance) return true;
            return false;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
        }
    }
}
