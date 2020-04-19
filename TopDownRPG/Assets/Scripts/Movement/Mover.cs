using RPG.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour
    {

        Animator animator;
        NavMeshAgent navMeshAgent;
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);
            float speed = localvelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}
