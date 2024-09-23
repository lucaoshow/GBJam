using UnityEngine;
using Root.Interactions;
using Root.GameManagement;

namespace Root.Doors
{
    public class Door : Interactable
    {
        [SerializeField] private Transform destination;
        [SerializeField] private bool usedOnce;

        public override void Start()
        {
            base.Start();
            this.interactWhenCollide = true;
            this.interactionType = InteractionTypes.Door;
        }

        public override void Interact()
        {
            GameManager.Instance.TransportPlayerTo(this.destination.position);
            if (this.usedOnce)
            {
                this.DisableInteraction();
            }
        }
    }
}
