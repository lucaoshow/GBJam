using Root.GameManagement;
using Root.General.Utils.Scenes;
using UnityEngine;

namespace Root.Coins
{
    public class FinalCoin : MonoBehaviour
    {
        [SerializeField] private Coin finalCoin;
        private bool loaded = false;
        private bool faded = false;

        void Update()
        {
            if (!this.finalCoin.CanInteract() && !this.faded)
            {
                GameManager.Instance.Fade();
                this.faded = true;
            }

            if (GameManager.Instance.fadeInHalf && !this.loaded)
            {
                SceneHelper.LoadScene("StartScene");
                this.loaded = true;
            }
        }
    }
}
