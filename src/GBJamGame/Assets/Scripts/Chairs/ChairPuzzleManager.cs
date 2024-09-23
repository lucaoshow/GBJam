using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Root.Chair
{
    public class ChairPuzzleManager : MonoBehaviour
    {
        [SerializeField] private GameObject completedDialogue;
        [SerializeField] private GameObject completedCoin;
        [SerializeField] private List<ChairInteractable> chairs;
        [SerializeField] private List<ChairPlaceInteractable> chairsPlaces;
        private ChairInteractable currentChair;
        private bool completed;

        public void InteractedWithChair(ChairInteractable chair)
        {
            this.currentChair = chair;
            this.chairs.ForEach(c => c.DisableInteraction());
            this.chairsPlaces.ForEach(cp => { cp.gameObject.SetActive(cp.Vague()); cp.EnableInteraction(); });
            this.currentChair.gameObject.SetActive(false);
        }

        public void InteractedWithChairPlace(ChairPlaceInteractable chairPlace)
        {
            this.currentChair.gameObject.SetActive(true);
            this.completed = this.chairs.All(c => c.SameOrientationAsPlace());
            this.currentChair = null;
            this.chairsPlaces.ForEach(cp => { cp.gameObject.SetActive(false); cp.DisableInteraction(); });
            if (this.completed)
            {
                this.completedDialogue.SetActive(true);
                this.completedCoin.SetActive(true);
                return;
            }
            this.chairs.ForEach(c => c.EnableInteraction());
        }

        public ChairInteractable GetChair()
        {
            return this.currentChair;
        }

        public bool HasChair()
        {
            return this.currentChair != null;
        }
    }
}
