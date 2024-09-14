using UnityEngine;

namespace Root.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private int walkSpeed;
        [SerializeField] private int runSpeed;

        public void Stop()
        {
            this.rb.velocity = Vector3.zero;
        }

        public void Walk(Vector3 direction)
        {
            this.rb.velocity = direction * this.walkSpeed;
        }

        public void Run(Vector3 direction)
        {
            this.rb.velocity = direction * this.runSpeed;
        }
    }
}
