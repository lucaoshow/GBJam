using Root.Dialogues;
using UnityEngine;

namespace Root.Trees
{
    public class GlitchedTree : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite cleanTree;
        [SerializeField] private DialogueInteractable dialogue;
        [SerializeField] private TreePuzzleManager manager;

        void Update()
        {
            if (!this.dialogue.CanInteract())
            {
                this.spriteRenderer.sprite = this.cleanTree;
                this.manager.InteractedWithTree();
                Destroy(this);
            }
        }
    }
}
