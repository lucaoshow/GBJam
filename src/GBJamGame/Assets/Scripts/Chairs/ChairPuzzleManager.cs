using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Root.Chair
{
    public class ChairPuzzleManager
    {
        [SerializeField] private List<ChairInteractable> chairs;
        [SerializeField] private List<ChairPlaceInteractable> chairsPlaces;
        private ChairInteractable currentChair;
        private bool completed;

        public void InteractedWithChair(ChairInteractable chair)
        {
            this.currentChair = chair;
            this.chairs.ForEach(c => c.DisableInteraction());
            this.chairsPlaces.ForEach(cp => { cp.gameObject.SetActive(true); cp.EnableInteraction(); });
        }

        public void InteractedWithChairPlace(ChairPlaceInteractable chairPlace)
        {
            this.completed = this.chairs.All(c => c.SameOrientationAsPlace());
            this.currentChair = null;
            if (!this.completed)
            {
                this.chairs.ForEach(c => c.EnableInteraction());
            }
            this.chairsPlaces.ForEach(cp => { cp.gameObject.SetActive(false); cp.DisableInteraction(); });
        }

        public ChairInteractable GetChair()
        {
            return this.currentChair;
        }

        public bool HasChair()
        {
            return this.currentChair != null;
        }

        public bool IsCompleted()
        {
            return this.completed;
        }
    }
}
