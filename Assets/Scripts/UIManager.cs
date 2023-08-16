using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using FirstGearGames.SmoothCameraShaker;

public class UIManager : MonoBehaviour
{
    public GameObject FadePannel;
    public ShakeData shakedata;
    GameObject playerObject;
    Volume globalVolume;
    VolumeProfile volumeProfile;

    public float vignette_OriginIntensity = 0.5f;
    public float filmGrain_OriginIntensity = 0.5f;
    public float cameraShake_OriginMagnitudeNoise = 0.05f;
    public float cameraShake_OriginRoughness = 1;
    float originFov;
    bool IsOnce;

    public void FadeInStart()
    {
        StartCoroutine(FadeIn());
    }
    
    public void FadeOutStart()
    {
        StartCoroutine(FadeOut());
    }
    public void FadeOutStart(string name)
    {
        StartCoroutine(FadeOut(name));
    }
    //페이드 아웃
    IEnumerator FadeIn()
    {
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = 1;
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        //yield return new WaitForSeconds(1);
    }

    //페이드 인
    IEnumerator FadeOut()
    {
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = 0;
        for (float f = 0f; f < 1; f += 0.02f)
        {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            Debug.Log(c);
            yield return null;
        }
        GameManager.Instance.Restart();
    }
    IEnumerator FadeOut(string name)
    {
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = 0;
        for (float f = 0f; f < 1; f += 0.01f)
        {
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        GameManager.Instance.SceneMove(name);
    }
    public void HorrorChase(Transform origin)
    {
        float distance = Vector3.Distance(origin.position, playerObject.transform.position);
        float intensityWeight = Mathf.Clamp((vignette_OriginIntensity + 1 / distance), 0, 1);

        playerObject.GetComponent<FirstPersonController>().fov = originFov - 10;
        if(volumeProfile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.rounded.Override(true);
            vignette.intensity.Override(intensityWeight);
        }
        if (volumeProfile.TryGet<FilmGrain>(out FilmGrain filmGrain))
        {
            filmGrain.intensity.Override(intensityWeight);
        }

        shakedata.MagnitudeNoise = Mathf.Clamp((cameraShake_OriginMagnitudeNoise + 1 / distance), 0, 1);
        shakedata.Roughness = cameraShake_OriginRoughness + (1 / distance);

        //GameManager.Instance.SoundManager.SetVolume((int)SOUND.SFX, -((1 / distance) * 40));
        if (!IsOnce)
        {
            //GameManager.Instance.playerObject.GetComponent<CameraShake>().Shake();
            GameManager.Instance.SoundManager.PlayAudio((int)SOUND.BGM, (int)BGM_NAME.CHASE1, true, true);
            IsOnce = true;
        }

        //여기서 수정한 값들은 다 초기화 해줘야함
    }

    public void HorrorReset()
    {
        playerObject.GetComponent<FirstPersonController>().fov = originFov;
        PPReset();
        GameManager.Instance.SoundManager.PlayAudio((int)SOUND.BGM, (int)BGM_NAME.AMB1, true, true);
        IsOnce = false;
    }

    void PPReset()
    {
        if (volumeProfile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.rounded.Override(false);
            vignette.intensity.Override(vignette_OriginIntensity);
        }

        if(volumeProfile.TryGet<FilmGrain>(out FilmGrain filmGrain))
        {
            filmGrain.intensity.Override(filmGrain_OriginIntensity);
        }

        shakedata.MagnitudeNoise = cameraShake_OriginMagnitudeNoise;
        shakedata.Roughness = cameraShake_OriginRoughness;
    }

    void Awake()
    {
        
    }

    void Start()
    {
        playerObject = GameManager.Instance.playerObject;
        originFov = playerObject.GetComponent<FirstPersonController>().fov;
        globalVolume = GameObject.Find("Global Volume").GetComponent<Volume>();
        volumeProfile = globalVolume.sharedProfile;
        PPReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
