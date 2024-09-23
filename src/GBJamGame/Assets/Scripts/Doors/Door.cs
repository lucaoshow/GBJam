using UnityEngine;
using Root.Interactions;
using Root.GameManagement;
using Root.Audio;

namespace Root.Doors
{
    public class Door : Interactable
    {
        [SerializeField] private Transform destination;
        [SerializeField] private bool usedOnce;
        [SerializeField] private bool changeSoundtrack;
        [SerializeField] private Soundtracks soundtrack;

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
            if (this.changeSoundtrack)
            {
                AudioManager.Instance.PlaySoundtrack(this.soundtrack);
            }
        }
    }
}
