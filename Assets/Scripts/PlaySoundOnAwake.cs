using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlaySoundOnAwake : MonoBehaviour
    {
        private AudioSource audioSource;
        private bool IsFirst = true;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        private void OnEnable()
        {
            if (IsFirst)
            {
                IsFirst = false;
                return;
            }
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}