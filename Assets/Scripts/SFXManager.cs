using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public List<AudioClip> SFXClips = new List<AudioClip>();

    public void PlaySound(string name)
    {
        foreach (AudioClip clip in SFXClips)
        {
            if(clip.name == name)
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }
    }

}
