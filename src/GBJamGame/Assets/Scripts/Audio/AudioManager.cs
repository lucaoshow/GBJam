using UnityEngine;

namespace Root.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource soundtrackSource;
        [SerializeField] private AudioSource soundEffectsSource;
        [SerializeField] private AudioClip[] soundtracks;
        [SerializeField] private AudioClip[] soundEffects;

        private static AudioManager instance;
        public static AudioManager Instance
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

        public void PlaySoundtrack(Soundtracks soundtrackIndex)
        {
            this.soundtrackSource.clip = this.soundtracks[(int) soundtrackIndex];
            this.soundtrackSource.Play();
        }

        public void PlaySoundEffect(SoundEffects soundEffectIndex)
        {
            if (this.soundEffectsSource.isPlaying)
            {
                return;
            }

            if (this.soundEffectsSource.clip != this.soundEffects[(int) soundEffectIndex])
            {
                this.soundEffectsSource.clip = this.soundEffects[(int) soundEffectIndex];
            }

            this.soundEffectsSource.Play();
        }
    }
}
