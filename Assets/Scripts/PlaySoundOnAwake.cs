using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlaySoundOnAwake : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        private void OnEnable()
        {
            audioSource.Play();
        }
    }
}