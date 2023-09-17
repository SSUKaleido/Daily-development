using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SOUND { MASTER, BGM, SFX, }
public enum BGM_NAME { AMB1, CHASE1, SIREN, AMB2, AMB3, SAFE1 }
public enum SFX_NAME { JUMPSCARE, DOOR, ITEM, ENCOUNTER, EXPLOSION, WHIP }

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource[] audioSource;
    public AudioClip[] BGM;
    public AudioClip[] SFX;

    public BGM_NAME currentBGM;

    public float defaultVolume;
    // Start is called before the first frame update
    public void PlayAudio(int soundType, int clipNum, bool loop, bool fade)
    {
        StartCoroutine(PlayAudioCoroutine(soundType, clipNum, loop, fade));
    }

    IEnumerator PlayAudioCoroutine(int soundType, int clipNum, bool loop, bool fade)
    {
        if (fade)
        {
            SetVolume(soundType, 0);
            for (float v = 0f; v > -40; v -= 0.5f)
            {
                SetVolume(soundType, v);
                yield return null;
            }
            SetVolume(soundType, -40);
        }

        switch (soundType)
        {
            case (int)SOUND.MASTER:
                audioSource[soundType].clip = SFX[clipNum];
                break;
            case (int)SOUND.BGM:
                audioSource[soundType].clip = BGM[clipNum];
                break;
            case (int)SOUND.SFX:
                audioSource[soundType].clip = SFX[clipNum];
                break;
        }
        audioSource[soundType].loop = loop;
        audioSource[soundType].Play();

        if(fade)
        {
            for (float v = -40f; v < 0; v += 0.2f)
            {
                SetVolume(soundType, v);
                yield return null;
            }
            SetVolume(soundType, 0);
        }
    }

    public void SetVolume(int soundType, float volume)
    {
        switch(soundType)
        {
            case 0:
                audioMixer.SetFloat("Master", volume);
                break;
            case 1:
                audioMixer.SetFloat("BGM", volume);
                break;
            case 2:
                audioMixer.SetFloat("SFX", volume);
                break;
        }
    }
    void Awake()
    {
        audioMixer.SetFloat("Master", defaultVolume);
        audioMixer.SetFloat("BGM", defaultVolume);
        audioMixer.SetFloat("SFX", defaultVolume);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
