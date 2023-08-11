using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SOUND { MASTER, BGM, SFX, }
public enum BGM_NAME { AMB1, }
public enum SFX_NAME { JUMPSCARE, WALK, }

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource[] audioSource;
    public AudioClip[] BGM;
    public AudioClip[] SFX;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public void PlayAudio(int soundType, int clipNum, bool loop)
    {
        switch(soundType)
        {
            case 0:
                audioSource[soundType].clip = SFX[clipNum];
                break;
            case 1:
                audioSource[soundType].clip = BGM[clipNum];
                break;
            case 2:
                audioSource[soundType].clip = SFX[clipNum];
                break;
        }
        audioSource[soundType].loop = loop;
        audioSource[soundType].Play();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
