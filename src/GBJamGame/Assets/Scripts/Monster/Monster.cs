using Root.GameManagement;
using UnityEngine;
using UnityEngine.AI;

namespace Root.Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        [SerializeField] private GameObject dialogueToWakeUp;
        [SerializeField] private MonsterStates state;
        [SerializeField] private float introductionTimer;
        [SerializeField] private Transform introductionDestination;
        [SerializeField] private GameObject introductionActivate;
        private float introductionCount;
        private NavMeshAgent agent;
        private Vector3 initialPos;
        private bool reseted = false;

        void Start()
        {
            this.agent = this.GetComponent<NavMeshAgent>();
            this.agent.updateRotation = false;
            this.agent.updateUpAxis = false;
            this.initialPos = this.transform.position;
            this.gameObject.SetActive(false);
        }

        void Update()
        {
            switch (this.state)
            {
                case MonsterStates.Introduction:
                    this.transform.position = Vector3.Lerp(this.transform.position, this.introductionDestination.position, this.introductionCount / this.introductionTimer);
                    if (this.introductionCount >= this.introductionTimer)
                    {
                        this.introductionActivate.SetActive(true);
                        Destroy(this.gameObject);
                    }
                    this.introductionCount += Time.deltaTime;
                    break;

                case MonsterStates.Follow:
                    this.agent.SetDestination(this.target.position);
                    this.agent.velocity = this.GetDirectionFromVelocity(this.agent.desiredVelocity) * this.moveSpeed;
                    this.spriteRenderer.flipX = this.agent.velocity.x < 0;
                    break;

                case MonsterStates.Final:
                    break;

                case MonsterStates.Waiting:
                    if (!this.reseted && GameManager.Instance.fadeInHalf)
                    {
                        GameObject dialogue = Instantiate(this.dialogueToWakeUp);
                        dialogue.SetActive(true);
                        this.transform.position = this.initialPos;
                        this.reseted = true;
                    }
                    break;
            }
        }

        public void StartFollow()
        {
            this.state = MonsterStates.Follow;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                GameManager.Instance.Fade();
                this.state = MonsterStates.Waiting;
            }
        }
        private enum MonsterStates
        {
            Introduction,
            Follow,
            Final,
            Waiting
        }
    }
}
