using Root.GameManagement;
using Root.Interactions;
using UnityEngine;

namespace Root.Coins
{
    public class Coin : Interactable
    {
        [SerializeField] private float rotationRate;
        private float rotation = 0;

        public override void Start()
        {
            base.Start();
            this.interactWhenCollide = true;
            this.interactionType = InteractionTypes.Coin;
            this.gameObject.SetActive(false);
        }

        public override void Interact()
        {
            GameManager.Instance.AddCoin();
            this.DisableInteraction();
            this.GetComponent<SpriteRenderer>().enabled = false;
        }

        private void Update()
        {
            this.rotation += Time.deltaTime * this.rotationRate;
            this.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }
}
