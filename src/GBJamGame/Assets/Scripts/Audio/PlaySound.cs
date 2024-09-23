using UnityEngine;

namespace Root.Audio
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private Soundtracks soundtrack;
        public void Play()
        {
            AudioManager.Instance.PlaySoundtrack(this.soundtrack);
        }

    }
}
