using UnityEngine;

namespace TopDown.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        private AudioSource soundSource;

        private void Awake()
        {
            //If singleton not null destroy it, otherwise create it
            if (Instance != null) Destroy(gameObject);

            Instance = this;
            
            soundSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            soundSource.PlayOneShot(sound);
        }
    }
}
