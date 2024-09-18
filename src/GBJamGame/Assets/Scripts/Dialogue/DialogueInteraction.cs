using Root.Interactions;
using UnityEngine;

namespace Root.Dialogues
{
    public class DialogueInteraction : Interactable
    {
        [SerializeField] private TextAsset inkJson;

        public override void Interact()
        {
            if (this.stillInteractable)
            {
                DialogueManager.Instance.StartDialogue(this.inkJson);
                this.DisableInteraction();
            }
            else if (this.CanInteract())
            {
                DialogueManager.Instance.ContinueDialogue();
            }
        }

        public override bool CanInteract()
        {
            return !DialogueManager.Instance.EndedDialogue();
        }
    }
}
