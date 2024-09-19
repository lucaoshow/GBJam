using Ink.Runtime;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Root.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private GameObject dialogueChoice;
        [SerializeField] private Vector2 choicesOffset;
        private TextMeshProUGUI dialogueText;
        private Rect dialogueChoiceRect;
        private Story currentDialogue;
        private List<GameObject> activeChoices = new List<GameObject>();

        private string completeDialogueText = "";
        private bool dialogueEnded = false;
        private bool dialogueHasChoices;

        private static DialogueManager instance;
        public static DialogueManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            instance = this;

            Canvas canvas = this.gameObject.AddComponent<Canvas>();
            canvas.pixelPerfect = true;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = this.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(160, 144);
            scaler.referencePixelsPerUnit = 16;
            this.gameObject.AddComponent<GraphicRaycaster>();

            this.dialogueBox = Instantiate(this.dialogueBox, canvas.transform);
            this.dialogueText = this.dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
            this.dialogueChoice = Instantiate(this.dialogueChoice, this.dialogueBox.transform);
            this.dialogueChoiceRect = this.dialogueChoice.GetComponentInChildren<RectTransform>().rect;
            this.dialogueChoice.SetActive(false);
            this.dialogueBox.SetActive(false);
        }

        private void Update()
        {
            if (this.WritingText())
            {
                this.dialogueText.text += this.completeDialogueText[this.dialogueText.text.Length];
            }
            else if (this.dialogueHasChoices)
            {
                this.ShowChoices();
                this.dialogueHasChoices = false;
            }
        }

        public void StartDialogue(TextAsset inkJson)
        {
            this.currentDialogue = new Story(inkJson.text);
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
                this.dialogueHasChoices = this.currentDialogue.currentChoices.Count > 0;
            }
            else if (this.activeChoices.Count > 0)
            {
                this.MakeChoice(this.activeChoices.IndexOf(EventSystem.current.currentSelectedGameObject));
            }
            else
            {
                this.EndDialogue();
            }
        }

        public bool EndedDialogue()
        {
            bool ended = this.dialogueEnded;
            this.dialogueEnded = false;
            return ended;
        }

        private void ShowChoices()
        {
            foreach (Choice choice in this.currentDialogue.currentChoices)
            {
                Vector3 offset = new Vector3( (this.dialogueChoiceRect.width + this.choicesOffset.x) * (choice.index % 2), (this.dialogueChoiceRect.height + this.choicesOffset.y) * (choice.index / 2), 0 );
                GameObject choiceObject = Instantiate(this.dialogueChoice, this.transform);
                choiceObject.transform.position += offset;
                choiceObject.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                choiceObject.SetActive(true);
                if (choice.index == 0) { choiceObject.GetComponent<Button>().Select(); }

                this.activeChoices.Insert(choice.index, choiceObject);
            }
        }

        private void MakeChoice(int index)
        {
            this.activeChoices.ForEach(choice => Destroy(choice) );
            this.activeChoices.Clear();
            this.currentDialogue.ChooseChoiceIndex(index);
            this.ContinueDialogue();
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
