using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipType { crabClip,shopClip}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]private AudioSource AudioSource;
    public AudioClip grabClip, shopClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    public void PlayAudio(AudioClipType audioClipType)
    {
        if (AudioSource!=null)
        {
            AudioClip audioclip = null;

            if (audioClipType== AudioClipType.crabClip)
            {
                audioclip = grabClip;

            }
            else if(audioClipType == AudioClipType.shopClip)
            {
                audioclip = shopClip;
            }
            AudioSource.PlayOneShot(audioclip, .5f);
        }
    }

    public void StopBackGroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
}
