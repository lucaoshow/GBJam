using Root.Audio;
using UnityEngine;

namespace Root.Trees
{
    public class TreePuzzleManager : MonoBehaviour
    {
        [SerializeField] private GameObject monster;
        [SerializeField] private GameObject dialogue;
        private int interacted = 0;
        public void InteractedWithTree()
        {
            this.interacted++;
            if (this.interacted >= 3)
            {
                this.monster.SetActive(true);
                this.dialogue.SetActive(true);
                this.dialogue.GetComponent<PlaySound>().Play();
            }
        }
    }
}
