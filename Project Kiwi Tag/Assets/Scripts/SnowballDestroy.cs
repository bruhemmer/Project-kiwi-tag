using System.Collections;
using UnityEngine;

public class SnowballDestroy : MonoBehaviour
{
    [Header("Credits: TheCoder")]

    [Header("Tags to not break")]
    public string[] tagsToIgnore;

    [Header("Particle Prefab")]
    public GameObject particleEffect;

    [Header("Audio Stuff")]
    public AudioClip destructionSound;
    public AudioSource audioSource;

    [Header("Don't Touch")]
    public bool canDestroy = false;

    void OnTriggerEnter(Collider other)
    {
        if (canDestroy)
        {
            if (!ShouldIgnoreTag(other.gameObject.tag))
            {
                PlayEffects();
                Destroy(gameObject, 0.2f);
            }
        }
    }

    bool ShouldIgnoreTag(string tag)
    {
        if (tagsToIgnore != null)
        {
            foreach (string ignoredTag in tagsToIgnore)
            {
                if (tag == ignoredTag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void PlayEffects()
    {
        if (particleEffect != null)
        {
            Vector3 spawnPosition = transform.position;
            
            Instantiate(particleEffect, spawnPosition, Quaternion.identity);
        }

        if (destructionSound != null)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(destructionSound);
            }
            else
            {
                AudioSource.PlayClipAtPoint(destructionSound, transform.position);
            }
        }
    }
}
