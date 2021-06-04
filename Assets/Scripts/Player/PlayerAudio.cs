using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private PlayerSources sources;
    [SerializeField] private PlayerClips clips;

    public void PlayFootstep()
    {
        AudioClip newClip = clips.footsteps[Random.Range(0, clips.footsteps.Count)];
        PlayRandomClip.PlayClip(sources.footsteps, newClip, true, 0.8f, 1.2f);
    }

    public void PlayJump()
    {
        AudioClip newClip = clips.jump[Random.Range(0, clips.jump.Count)];
        PlayRandomClip.PlayClip(sources.jump, newClip);
    }

    public void PlayGrab()
    {
        AudioClip newClip = clips.grab[Random.Range(0, clips.grab.Count)];
        PlayRandomClip.PlayClip(sources.grab, newClip);
    }

    public void PlayClimb()
    {
        AudioClip newClip = clips.climb[Random.Range(0, clips.climb.Count)];
        PlayRandomClip.PlayClip(sources.climb, newClip);
    }

    public void PlayTongue()
    {
        AudioClip newClip = clips.tongue[Random.Range(0, clips.tongue.Count)];
        PlayRandomClip.PlayClip(sources.tongue, newClip);
    }
}

[System.Serializable]
public class PlayerSources
{
    public AudioSource footsteps, jump, grab, climb, tongue;
}

[System.Serializable]
public class PlayerClips
{
    public List<AudioClip> footsteps = new List<AudioClip>();
    public List<AudioClip> jump = new List<AudioClip>();
    public List<AudioClip> grab = new List<AudioClip>();
    public List<AudioClip> climb = new List<AudioClip>();
    public List<AudioClip> tongue = new List<AudioClip>();
}
