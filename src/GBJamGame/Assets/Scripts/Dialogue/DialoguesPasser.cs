using Root.General.Utils.Scenes;
using System.Collections.Generic;
using UnityEngine;

namespace Root.Dialogues
{
    public class DialoguesPasser : MonoBehaviour
    {
        [SerializeField] private List<DialogueInteractable> dialogues;
        private DialogueInteractable currentDialogue;
        private int index = 0;
        private bool passed = false;

        private void Start()
        {
            this.currentDialogue = this.dialogues[this.index];
            this.index++;
        }

        private void Update()
        {
            if (!this.currentDialogue.CanInteract() && !this.passed)
            {
                if (this.index >= this.dialogues.Count)
                {
                    SceneHelper.LoadScene("GameScene");
                    this.passed = true;
                    return;
                }
                this.currentDialogue = this.dialogues[this.index];
                this.currentDialogue.gameObject.SetActive(true);
                this.index++;
            }
        }
    }
}
