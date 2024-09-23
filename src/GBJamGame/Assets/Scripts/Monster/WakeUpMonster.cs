using Root.Interactions;
using UnityEngine;

namespace Root.Monster
{
    public class WakeUpMonster : MonoBehaviour
    {
        [SerializeField] private Monster monster;
        [SerializeField] private Interactable interactable;
        private bool started;

        private void Start()
        {
            this.started = false;
        }

        private void Update()
        {
            if (!this.interactable.CanInteract() && !this.started)
            {
                this.monster.StartFollow();
                this.started = true;
            }
        }
    }
}
