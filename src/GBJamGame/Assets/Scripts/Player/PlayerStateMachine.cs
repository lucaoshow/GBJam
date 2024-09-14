using UnityEngine;

namespace Root.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        private Vector3 moveDir;
        private PlayerStates state;

        void Update()
        {
            this.ChangeStateBasedOnInput();

            switch (this.state)
            {
                case PlayerStates.Idle:
                    this.playerMovement.Stop();
                    break;

                case PlayerStates.Walking:
                    this.playerMovement.Walk(this.moveDir);
                    break;

                case PlayerStates.Running:
                    this.playerMovement.Run(this.moveDir);
                    break;
            }
        }

        private void ChangeStateBasedOnInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            bool running = Input.GetKey(KeyCode.J);

            if (horizontal != 0 && vertical != 0)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    this.moveDir = new Vector3(0, vertical, 0);
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    this.moveDir = new Vector3(horizontal, 0, 0);
                }

                this.state = running ? PlayerStates.Running : PlayerStates.Walking;
            }
            else if (horizontal != 0 || vertical != 0)
            {
                this.moveDir = new Vector3(horizontal, vertical, 0);
                this.state = running ? PlayerStates.Running : PlayerStates.Walking;
            }
            else
            {
                this.moveDir = Vector3.zero;
                this.state = PlayerStates.Idle;
            }
        }
    }

    public enum PlayerStates
    {
        Idle,
        Walking,
        Running
    }
}
