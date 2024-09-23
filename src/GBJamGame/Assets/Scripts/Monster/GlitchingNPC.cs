using UnityEngine;
using Root.Dialogues;
using Root.GameManagement;

namespace Root.Monster
{
    public class GlitchingNPC : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private DialogueInteractable dialogue;
        [SerializeField] private Sprite monsterSprite;
        [SerializeField] private Transform takePlayerTo;
        [SerializeField] private GameObject dialogueToActivate;
        [SerializeField] private float secondsToKill;
        private float timer = 0;
        private bool turnedIntoMonster = false;

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        void Update()
        {
            if (!this.turnedIntoMonster)
            {
                if (!this.dialogue.CanInteract())
                {
                    this.spriteRenderer.material = this.defaultMaterial;
                    this.spriteRenderer.sprite = this.monsterSprite;
                    this.turnedIntoMonster = true;
                }
            }
            else if (!GameManager.Instance.fading)
            {
                if (this.timer >= this.secondsToKill)
                {
                    GameManager.Instance.Fade();
                }
                
                this.timer += Time.deltaTime;
            }
            else if (GameManager.Instance.fadeInHalf)
            {
                GameManager.Instance.TransportPlayerTo(this.takePlayerTo.position);
                this.dialogueToActivate.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
