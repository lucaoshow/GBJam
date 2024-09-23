using UnityEngine;
using Root.Interactions;
using Root.GameManagement;

namespace Root.Doors
{
    public class Door : Interactable
    {
        [SerializeField] private Transform destination;
        [SerializeField] private bool usedOnce;
        [SerializeField] private bool opened;

        public override void Start()
        {
            base.Start();
            this.interactWhenCollide = true;
            this.interactionType = InteractionTypes.Door;
        }

        public override void Interact()
        {
            if (this.opened)
            {
                GameManager.Instance.TransportPlayerTo(this.destination.position);
                if (this.usedOnce)
                {
                    this.DisableInteraction();
                }
            }
        }

        public void Open()
        {
            this.opened = true;
        }
    }
}
