using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFX : MonoBehaviour
{
    private AudioSource soundFX;
    [SerializeField]
    private AudioClip attackSound_1, attackSound_2, dieSound;

    void Awake()
    {
        soundFX = GetComponent<AudioSource>();
    }

    public void Attack1()
    {
        soundFX.clip = attackSound_1;
        soundFX.Play();
    }

    public void Attack2()
    {
        soundFX.clip = attackSound_2;
        soundFX.Play();
    }

    public void Die()
    {
        soundFX.clip = dieSound;
        soundFX.Play();
    }
}
