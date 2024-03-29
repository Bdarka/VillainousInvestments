using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource currentSong;
    public int songCount;
    public bool isFocused;

    public List<AudioClip> songs = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        currentSong = GetComponent<AudioSource>();
        songCount = 0;
        currentSong.clip = songs[songCount];
    }

    // turns out there's a quirk with Untiy where if you don't check for if its the current user selected window
    // then it just plays a new track each time a user alt + tab. Not cool 
    // Source: https://discussions.unity.com/t/how-can-i-play-one-music-right-after-the-other-ends/210980/4
    private void OnApplicationFocus(bool focus)
    {
        isFocused = focus;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFocused == true && currentSong.isPlaying == false)
        {
            PlayNextMusic();
        }
    }

    public void PlayNextMusic()
    {
        if(songCount > songs.Count)
        {
            songCount = 0;
        }
        currentSong.clip = songs[songCount];
        currentSong.Play();
        songCount++;
    }
}
