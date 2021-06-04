using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomClip
{
    public static void PlayClip(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public static void PlayClip(AudioSource source, AudioClip clip, bool changePitch, float min, float max)
    {
        if (changePitch) source.pitch = Random.Range(min, max);
        source.clip = clip;
        source.Play();
    }
}
