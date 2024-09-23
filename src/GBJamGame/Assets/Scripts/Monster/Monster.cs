using UnityEngine;
using UnityEngine.AI;

namespace Root.Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        private NavMeshAgent agent;

        void Start()
        {
            this.agent = this.GetComponent<NavMeshAgent>();
            this.agent.updateRotation = false;
            this.agent.updateUpAxis = false;
        }

        void Update()
        {
            this.agent.SetDestination(this.target.position);
            this.agent.velocity = this.GetDirectionFromVelocity(this.agent.desiredVelocity) * this.moveSpeed;
            this.spriteRenderer.flipX = this.agent.velocity.x < 0;
        }

        private Vector3 GetDirectionFromVelocity(Vector3 velocity)
        {
            Vector3[] directions = { Vector3.down, Vector3.up, Vector3.left, Vector3.right };
            float smallest = 1;
            Vector3 finalDir = Vector3.zero;

            foreach (Vector3 dir in directions)
            {
                float current = Vector3.Dot(velocity, dir);
                if (current < smallest)
                {
                    smallest = current;
                    finalDir = dir;
                }
            }

            return -finalDir;
        }


    }
}
