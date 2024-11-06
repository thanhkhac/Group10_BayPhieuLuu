using System;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void PlayCollisionSound()
    {
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }

        // Tắt renderer và collider để làm cho item "biến mất" sau va chạm
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Hủy đối tượng sau khi âm thanh phát xong
        Destroy(gameObject, collisionSound.length);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Tag trigger"+ other.tag);
        if (other.CompareTag("Player"))
        {
            PlayCollisionSound();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Tag collision" + other.collider.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            PlayCollisionSound();
        }
    }

}