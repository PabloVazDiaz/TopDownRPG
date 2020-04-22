using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{

    public class PatrolPath : MonoBehaviour
    {
        const float waypointRadius = 0.25f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), waypointRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(NextIndex(i)));
            }
        }

        public int NextIndex(int i)
        {
            if (i == transform.childCount - 1)
                return 0;
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}