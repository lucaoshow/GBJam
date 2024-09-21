using UnityEngine;
using Root.Interactions;

namespace Root.Chair
{
    public class ChairPlaceInteractable : Interactable
    {
        [SerializeField] private ChairPuzzleManager chairsManager;
        [SerializeField] private ChairOrientations placeOrientation;
        public override void Interact()
        {
            if (this.CanInteract() && this.chairsManager.HasChair())
            {
                ChairInteractable chair = this.chairsManager.GetChair();
                chair.transform.position = this.transform.position;
                chair.SetPlaceOrientation(this.placeOrientation);
                chair.gameObject.SetActive(true);
                this.chairsManager.InteractedWithChairPlace(this);
            }
        }
    }
}
