using Ink.Runtime;
using TMPro;
using UnityEngine;

namespace Root.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        private GameObject dialogueBox;
        private TextMeshProUGUI dialogueText;
        private Story currentDialogue;

        private string completeDialogueText = "";
        private bool dialogueEnded = false;

        private static DialogueManager instance;
        public static DialogueManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (new GameObject("DialogueManagerContainer")).AddComponent<DialogueManager>();
                }
                return instance;
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            this.dialogueBox = Instantiate(Resources.Load<GameObject>("Prefabs/Dialogue/DialogueBox"), this.transform);
            this.dialogueText = this.dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
            this.dialogueBox.SetActive(false);
        }

        private void Update()
        {
            if (this.WritingText())
            {
                this.dialogueText.text += this.completeDialogueText[this.dialogueText.text.Length];
            }
        }

        public void StartDialogue(TextAsset inkJson)
        {
            this.currentDialogue = new Story(inkJson.text);
            this.dialogueEnded = false;
            this.dialogueBox.SetActive(true);
            this.ContinueDialogue();
        }

        public void ContinueDialogue()
        {
            if (this.WritingText())
            {
                this.dialogueText.text = this.completeDialogueText;
            }
            else if (this.currentDialogue.canContinue)
            {
                this.dialogueText.text = "";
                this.completeDialogueText = this.currentDialogue.Continue();
            }
            else
            {
                this.EndDialogue();
            }
        }

        public bool EndedDialogue()
        {
            return this.dialogueEnded;
        }

        private bool WritingText()
        {
            return this.dialogueText.text.Length < this.completeDialogueText.Length;
        }

        private void EndDialogue()
        {
            this.dialogueBox.SetActive(false);
            this.completeDialogueText = "";
            this.dialogueText.text = "";
            this.dialogueEnded = true;
        }
    }
}
