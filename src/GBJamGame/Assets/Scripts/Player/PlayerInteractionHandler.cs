using Root.Interactions;
using UnityEngine;
using System.Collections.Generic;

namespace Root.Player
{
    public class PlayerInteractionHandler : MonoBehaviour
    {
        private List<Interactable> interactables = new List<Interactable>();
        private Interactable closestInteractable;

        private void Update()
        {
            if (this.interactables.Count > 0)
            {
                this.SetClosestInteractable();
            }
        }

        public bool CanInteract()
        {
            return this.closestInteractable != null && this.closestInteractable.CanInteract();
        }

        public InteractionTypes GetInteractionType()
        {
            return this.closestInteractable.GetInteractionType();
        }

        public void Interact()
        {
            this.closestInteractable.Interact();
            this.RemoveInteractable(this.closestInteractable);
        }

        private void SetClosestInteractable()
        {
            Interactable closest = this.interactables[0];
            float minDistance = Vector2.Distance(this.transform.position, closest.transform.position);
            for (int i = 1; i < this.interactables.Count; i++)
            {
                float distance = Vector2.Distance(this.transform.position, this.interactables[i].transform.position);
                if (distance < minDistance)
                {
                    closest = this.interactables[i];
                    minDistance = distance;
                }
            }

            if (this.closestInteractable != closest)
            {
                this.closestInteractable.InteractionIconVisible(false);
                this.closestInteractable = closest;
                this.closestInteractable.InteractionIconVisible(true);
            }
        }

        private void AddInteractable(Interactable interactable)
        {
            this.interactables.Add(interactable);
            if (this.interactables.Count == 1)
            {
                this.closestInteractable = this.interactables[0];
                this.closestInteractable.InteractionIconVisible(true);
            }
        }

        private void RemoveInteractable(Interactable interactable)
        {
            this.interactables.Remove(interactable);
            if (this.closestInteractable == interactable)
            {
                this.closestInteractable.InteractionIconVisible(false);
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Interactable>(out Interactable interactable))
            {
                if (interactable.CanInteract())
                {
                    this.AddInteractable(interactable);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Interactable>(out Interactable interactable))
            {
                this.RemoveInteractable(interactable);
            }
        }
    }
}
