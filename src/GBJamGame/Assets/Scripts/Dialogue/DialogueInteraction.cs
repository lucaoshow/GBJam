using Root.Interactions;
using UnityEngine;

namespace Root.Dialogues
{
    public class DialogueInteraction : Interactable
    {
        [SerializeField] private TextAsset inkJson;
        private bool firstInteraction = true;
        public override void Interact()
        {
            if (this.firstInteraction)
            {
                DialogueManager.Instance.StartDialogue(this.inkJson);
                this.firstInteraction = false;
            }
            else if (this.CanInteract())
            {
                DialogueManager.Instance.ContinueDialogue();
                this.stillInteractable = !DialogueManager.Instance.EndedDialogue();
            }
        }
    }
}
