using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using System.Collections.Generic;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        
        NavMeshAgent navMeshAgent;
        Health health;

       

        // [SerializeField] Transform target;
        [SerializeField] float Maxspeed = 6f;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
           
            

        }
        // Update is called once per frame

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }
        public void StartMoveAction(Vector3 destination,float Speedfraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, Speedfraction);

        }
        public void MoveTo(Vector3 destination, float Speedfraction)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = Maxspeed * Mathf.Clamp01(Speedfraction);
            navMeshAgent.destination = destination;
        }
        
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("Forwardspeed", speed);
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
