using UnityEngine;

namespace RPG.Control
{

    public class Patrolroute : MonoBehaviour
    {
        private void OnDrawGizmos() {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                //Sphere Acting as Check Point
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetRoutePoint(i), 0.3f);
                //Line Connecting Route Checkpoints
                Gizmos.color = Color.white;
                Gizmos.DrawLine(GetRoutePoint(i), GetRoutePoint(j));


            }

        }

        public  int GetNextIndex(int i)
        {
            if(i + 1 == transform.childCount){
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetRoutePoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

    }

