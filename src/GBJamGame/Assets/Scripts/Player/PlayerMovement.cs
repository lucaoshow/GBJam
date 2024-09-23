using UnityEngine;

namespace Root.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;

        public void Stop()
        {
            this.rb.velocity = Vector3.zero;
            this.animator.SetBool("moving", false);
        }

        public void Walk(Vector3 direction)
        {
            this.rb.velocity = direction * this.walkSpeed;
            this.spriteRenderer.flipX = direction.x < 0;
            this.animator.SetInteger("x", (int) direction.x);
            this.animator.SetInteger("y", (int) direction.y);
            this.animator.SetBool("moving", true);
        }

        public void Run(Vector3 direction)
        {
            this.rb.velocity = direction * this.runSpeed;
            this.spriteRenderer.flipX = direction.x < 0;
            this.animator.SetInteger("x", (int) direction.x);
            this.animator.SetInteger("y", (int) direction.y);
            this.animator.SetBool("moving", true);
        }
    }
}
