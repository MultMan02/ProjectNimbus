using UnityEngine;

namespace TopDown.Audio
{
    public class StepSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip[] stepSounds;
        
        public void PlayStepSound()
        {
            int randomStepSoundIndex = Random.Range(0, stepSounds.Length);
            SoundManager.Instance?.PlaySound(stepSounds[randomStepSoundIndex]);
        }
    }
}
