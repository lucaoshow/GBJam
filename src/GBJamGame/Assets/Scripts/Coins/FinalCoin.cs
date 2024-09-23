using Root.GameManagement;
using Root.General.Utils.Scenes;
using UnityEngine;

namespace Root.Coins
{
    public class FinalCoin : MonoBehaviour
    {
        [SerializeField] private Coin finalCoin;

        void Update()
        {
            if (!this.finalCoin.CanInteract())
            {
                GameManager.Instance.Fade();
            }

            if (GameManager.Instance.fadeInHalf)
            {
                SceneHelper.LoadScene("StartScene");
            }
        }
    }
}
