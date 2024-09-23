using UnityEngine;
using Root.Interactions;

namespace Root.Chair
{
    public class ChairPlaceInteractable : Interactable
    {
        [SerializeField] private ChairPuzzleManager chairsManager;
        public ChairOrientations orientation { get; private set; }
        private ChairInteractable chair;
        
        public override void Start()
        {
            base.Start();
            this.interactionType = InteractionTypes.Chair;
            this.gameObject.SetActive(false);
        }

        public override void Interact()
        {
            if (this.CanInteract() && this.chairsManager.HasChair())
            {
                this.chair = this.chairsManager.GetChair();
                this.chair.transform.position = this.transform.position;
                this.chair.SetChairPlace(this);
                this.chair.gameObject.SetActive(true);
                this.chairsManager.InteractedWithChairPlace(this);
            }
        }
        
        public bool Vague()
        {
            return this.chair == null;
        }

        public void Deoccupy()
        {
            this.chair = null;
        }
    }
}
