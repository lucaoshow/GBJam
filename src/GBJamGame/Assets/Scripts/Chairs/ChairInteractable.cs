using Root.Interactions;
using UnityEngine;

namespace Root.Chair
{
    public class ChairInteractable : Interactable
    {
        [SerializeField] private ChairPuzzleManager chairsManager;
        [SerializeField] private ChairOrientations chairOrientation;
        private ChairPlaceInteractable chairPlace;

        public override void Start()
        {
            base.Start();
            this.interactionType = InteractionTypes.Chair;
        }

        public override void Interact()
        {
            if (!this.chairsManager.HasChair() && this.CanInteract())
            {
                if (this.chairPlace != null)
                {
                    this.chairPlace.Deoccupy();
                    this.chairPlace = null;
                }
                this.chairsManager.InteractedWithChair(this);
            }
        }

        public void SetChairPlace(ChairPlaceInteractable chairPlace)
        {
            this.chairPlace = chairPlace;
        }

        public bool SameOrientationAsPlace()
        {
            return this.chairPlace != null && this.chairOrientation == this.chairPlace.orientation;
        }
    }
}
