using Cinemachine;
using Root.Audio;
using UnityEngine;

namespace Root.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

        public void TransportPlayerTo(Vector3 destination)
        {
            this.player.transform.position = destination;
            this.virtualCamera.transform.position = destination;    
        }

        public void AddCoin()
        {
            AudioManager.Instance.PlaySoundEffect(SoundEffects.Coin);
        }
    }
}
