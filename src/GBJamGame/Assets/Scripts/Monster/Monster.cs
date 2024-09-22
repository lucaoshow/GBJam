using UnityEngine;
using UnityEngine.AI;

namespace Root.Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private Transform target;
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
        }
    }
}
