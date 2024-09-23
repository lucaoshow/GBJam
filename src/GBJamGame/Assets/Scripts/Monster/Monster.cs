using Root.Audio;
using Root.Dialogues;
using Root.GameManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace Root.Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform dialoguesParent;
        [SerializeField] private GameObject dialogueToWakeUp;
        [SerializeField] private Transform playerRespawn;
        [SerializeField] private MonsterStates state;
        [SerializeField] private Transform introductionDestination;
        [SerializeField] private GameObject introductionActivate;
        [SerializeField] private DialogueInteractable finalDialogue;
        [SerializeField] private Sprite pixelSprite;
        [SerializeField] private GameObject pixelSpeech;
        private float introductionCount;
        private Rigidbody2D rb;
        private Vector3 initialPos;
        private bool reseted = false;
        private bool playedSound = false;

        void Start()
        {
            this.rb = this.GetComponent<Rigidbody2D>();
            this.initialPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.gameObject.SetActive(false);
        }

        void Update()
        {
            switch (this.state)
            {
                case MonsterStates.Introduction:
                    if (!this.playedSound)
                    {
                        AudioManager.Instance.PlaySoundtrack(Soundtracks.Chase);
                    }
                    this.rb.velocity = Vector3.right * this.moveSpeed;
                    if (Vector3.Distance(this.transform.position, this.introductionDestination.position) < 0.01 || this.transform.position.x > this.introductionDestination.position.x)
                    {
                        this.introductionActivate.SetActive(true);
                        Destroy(this.gameObject);
                    }
                    this.introductionCount += Time.deltaTime;
                    break;

                case MonsterStates.Follow:
                    this.rb.velocity = this.GetDirectionFromVelocity(this.transform.position - this.target.position) * this.moveSpeed;
                    this.spriteRenderer.flipX = this.rb.velocity.x < 0;
                    break;

                case MonsterStates.Final:
                    if (!this.finalDialogue.CanInteract())
                    {
                        this.spriteRenderer.material = this.defaultMaterial;
                        this.spriteRenderer.sprite = this.pixelSprite;
                        this.pixelSpeech.SetActive(true);
                        AudioManager.Instance.PlaySoundtrack(Soundtracks.Magic);
                        Destroy(this);
                    }
                    break;

                case MonsterStates.Waiting:
                    Debug.Log(GameManager.Instance.fadeInHalf);
                    if (!this.reseted && GameManager.Instance.fadeInHalf)
                    {
                        
                        GameObject dialogue = Instantiate(this.dialogueToWakeUp, Vector3.zero, Quaternion.identity, this.dialoguesParent);
                        dialogue.SetActive(true);
                        dialogue.GetComponent<DialogueInteractable>().EnableInteraction();
                        GameManager.Instance.TransportPlayerTo(this.playerRespawn.position);
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

            return finalDir;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player") && this.state == MonsterStates.Follow)
            {
                GameManager.Instance.Fade(true);
                this.state = MonsterStates.Waiting;
                this.rb.velocity = Vector3.zero;
                this.reseted = false;
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
