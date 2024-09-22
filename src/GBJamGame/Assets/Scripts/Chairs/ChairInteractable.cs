using Root.Interactions;
using UnityEngine;

namespace Root.Chair
{
    public class ChairInteractable : Interactable
    {
        [SerializeField] private ChairPuzzleManager chairsManager;
        [SerializeField] private ChairOrientations chairOrientation;
        private ChairOrientations placeOrientation;
        public override void Interact()
        {
            if (!this.chairsManager.HasChair() && this.CanInteract())
            {
                this.gameObject.SetActive(false);
                this.chairsManager.InteractedWithChair(this);
            }
        }

        public void SetPlaceOrientation(ChairOrientations orientation)
        {
            this.placeOrientation = orientation;
        }

        public bool SameOrientationAsPlace()
        {
            return this.chairOrientation == this.placeOrientation;
        }
    }
}
