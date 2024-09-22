using UnityEngine;

namespace Root.Interactions
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private GameObject interactionIcon;
        protected bool stillInteractable = true;
        protected bool interactWhenCollide = false;
        
        public virtual void Interact() { }

        public virtual void Start()
        {
            this.interactionIcon.SetActive(false);
        }

        public virtual InteractionTypes GetInteractionType() 
        {
            return InteractionTypes.Dialogue;
        }

        public virtual bool CanInteract()
        {
            return this.stillInteractable;
        }

        public bool InteractOnCollision()
        {
            return this.interactWhenCollide;
        }
        
        public void InteractionIconVisible(bool visible)
        {
            if (this.stillInteractable)
            {
                this.interactionIcon.SetActive(visible);
            }
        }
        
        public void DisableInteraction()
        {
            this.InteractionIconVisible(false);
            this.stillInteractable = false;
        }

        public void EnableInteraction()
        {
            this.stillInteractable = true;
        }
    }       
}
