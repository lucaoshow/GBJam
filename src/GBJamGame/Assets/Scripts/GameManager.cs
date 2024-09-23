using Cinemachine;
using UnityEngine;

namespace Root.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Animator fadeAnimator;
        
        [HideInInspector] public bool fading;
        [HideInInspector] public bool fadeInHalf;
        private float fadeTime;
        private readonly float fadeDuration = 3f;

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

        private void Update()
        {
            if (this.fadeTime >= this.fadeDuration)
            {
                this.fading = false;
            }

            if (this.fading)
            {
                this.fadeTime += Time.deltaTime;
            }

            this.fadeInHalf = this.fading && this.fadeTime >= this.fadeDuration / 2;
        }

        public void TransportPlayerTo(Vector3 destination)
        {
            this.player.transform.position = destination;
            this.virtualCamera.transform.position = destination;    
        }

        public void Fade()
        {
            this.fadeAnimator.SetTrigger("start");
        }
    }
}
