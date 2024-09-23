using Root.Interactions;
using UnityEngine;

namespace Root.Monster
{
    public class WakeUpMonster : MonoBehaviour
    {
        [SerializeField] private Monster monster;
        [SerializeField] private Interactable interactable;

        private void Update()
        {
            if (!this.interactable.CanInteract())
            {
                this.monster.StartFollow();
                Destroy(this);
            }
        }
    }
}
