using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using FirstGearGames.SmoothCameraShaker;
using TMPro;
using System.Text;

public enum UI_TYPE { TALK, ITEM, DOOR, GETITEM, DIALOG, TEXT, ATTACK, PAD, INVESTIGATE }

public class UIManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject Tutorial;
    public GameObject FadePannel;
    public GameObject DamageUI;
    public GameObject NumPadUI;
    public GameObject QuizUI;
    public GameObject TimeAttackUI;
    public GameObject[] UI;
    public ShakeData shakedata;

    GameObject playerObject;
    Volume globalVolume;
    VolumeProfile volumeProfile;

    public float vignette_OriginIntensity = 0.5f;
    public float filmGrain_OriginIntensity = 0.5f;
    public float cameraShake_OriginMagnitudeNoise = 0.05f;
    public float cameraShake_OriginRoughness = 1;
    float originFov;

    public bool IsTalkUI;
    public bool IsItemUI;
    public bool IsDoorUI;
    public bool IsPadUI;
    public bool IsInvestigateUI;

    bool IsOnce;
    bool IsScript;
    bool IsQuiz;

    int quizNum = -1;

    NPC dialogNPC;
    string[] dialogScript;
    int dialogIndex;

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
    private IEnumerator SetTalkUI()
    {
        while (true)
        {
            if (IsTalkUI)
            {
                UI[(int)UI_TYPE.TALK].SetActive(true);
            }
            else
            {
                UI[(int)UI_TYPE.TALK].SetActive(false);
            }
            IsTalkUI = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator SetItemUI()
    {
        while (true)
        {
            if (IsItemUI)
            {
                UI[(int)UI_TYPE.ITEM].SetActive(true);
            }
            else
            {
                UI[(int)UI_TYPE.ITEM].SetActive(false);
            }
            IsItemUI = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SetDoorUI()
    {
        while (true)
        {
            if (IsDoorUI)
            {
                UI[(int)UI_TYPE.DOOR].SetActive(true);
            }
            else
            {
                UI[(int)UI_TYPE.DOOR].SetActive(false);
            }
            IsDoorUI = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SetPadUI()
    {
        while(true)
        {
            if(IsPadUI)
            {
                UI[(int)UI_TYPE.PAD].SetActive(true);
            }
            else
            {
                UI[(int)UI_TYPE.PAD].SetActive(false);
            }
            IsPadUI = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SetInvestigateUI()
    {
        while (true)
        {
            if (IsInvestigateUI)
            {
                UI[(int)UI_TYPE.INVESTIGATE].SetActive(true);
            }
            else
            {
                UI[(int)UI_TYPE.INVESTIGATE].SetActive(false);
            }
            IsInvestigateUI = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SetGetItemUI(string ItemName)
    {
        UI[(int)UI_TYPE.GETITEM].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ItemName + " 획득";
        UI[(int)UI_TYPE.GETITEM].SetActive(true);
        yield return new WaitForSeconds(3f);
        UI[(int)UI_TYPE.GETITEM].SetActive(false);
    }

    public void StartGetItemUI(string ItemName)
    {
        //UIReset();
        StartCoroutine(SetGetItemUI(ItemName));
    }

    public void SetInventoryUI()
    {

    }
    public void SetDialogUI(NPC npc, string[] script)
    {
        dialogNPC = npc;
        dialogScript = script;
        dialogIndex = 0;
        IsScript = true;
        UI[(int)UI_TYPE.DIALOG].SetActive(true);
        NextDialog();
    }

    public void StartTextUI(string text)
    {
        StartCoroutine(SetTextUI(text));
    }

    public void StartTutorialUI()
    {
        StartCoroutine(SetTutorialUI());
    }

    private IEnumerator SetTutorialUI()
    {
        Tutorial.SetActive(true);
        yield return new WaitForSeconds(5f);
        Tutorial.SetActive(false);
    }

    public void SetAttackUI()
    {
        UI[(int)UI_TYPE.ATTACK].SetActive(true);
    }

    public void SetNumPadUI(int num)
    {
        NumPadUI.SetActive(true);
        NumPadUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
        for (int i = 0; i < num; i++)
        {
            NumPadUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += "*";
        }
    }

    public void InputNumPadUI(int num, int index)
    {
        string tmp = NumPadUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
        StringBuilder sb = new StringBuilder(tmp);
        sb[index] = (char)(num + '0');
        NumPadUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = sb.ToString();
    }

    private IEnumerator SetTextUI(string text)
    {
        UI[(int)UI_TYPE.TEXT].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        UI[(int)UI_TYPE.TEXT].SetActive(true);
        yield return new WaitForSeconds(2f);
        UI[(int)UI_TYPE.TEXT].SetActive(false);
    }

    public void StartGetDamaged()
    {
        StartCoroutine(BloodFade());
    }

    public void SetQuizUI(int num)
    {
        QuizUI.transform.GetChild(num).gameObject.SetActive(true);
        quizNum = num;
        IsQuiz = true;
    }

    public void SetTimeAttackUI()
    {
        TimeAttackUI.SetActive(true);
    }
    IEnumerator BloodFade()
    {
        int index = Random.Range(0, 4);
        Image image = DamageUI.transform.GetChild(index).GetComponent<Image>();
        image.enabled = true;
        Color c = image.color;
        c.a = 0;
        for (float f = 0f; f < 1; f += 0.02f)
        {
            c.a = f;
            image.color = c;
            yield return null;
        }

        c.a = 1;
        yield return new WaitForSeconds(3f);

        for (float f = 1f; f > 0; f -= 0.02f)
        {
            c.a = f;
            image.color = c;
            yield return null;
        }
        c.a = 0;
        image.enabled = false;
    }

    void NextDialog()
    {
        if(dialogIndex == dialogScript.Length)
        {
            IsScript = false;
            UI[(int)UI_TYPE.DIALOG].SetActive(false);
            dialogNPC.TalkEnd();
            return;
        }
        UI[(int)UI_TYPE.DIALOG].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogScript[dialogIndex++];
    }
    public void HorrorChase(Transform origin)
    {
        float distance = Vector3.Distance(origin.position, playerObject.transform.position);
        float intensityWeight = Mathf.Clamp((vignette_OriginIntensity + 1 / distance), 0, 1);

        playerObject.GetComponent<FirstPersonController>().fov = originFov - 10;
        if (volumeProfile.TryGet<Vignette>(out Vignette vignette))
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
            GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.ENCOUNTER, false, false);
            IsOnce = true;
        }
        //여기서 수정한 값들은 다 초기화 해줘야함
    }

    void UIReset()
    {
        foreach(var ui in UI)
        {
            ui.SetActive(false);
        }
    }
    public void HorrorReset()
    {
        playerObject.GetComponent<FirstPersonController>().fov = originFov;
        PPReset();
        GameManager.Instance.SoundManager.PlayAudio((int)SOUND.BGM, (int)GameManager.Instance.SoundManager.currentBGM, true, true);
        GameManager.Instance.playerObject.GetComponent<Player>().Heart = GameManager.Instance.playerObject.GetComponent<Player>().maxHeart;
        IsOnce = false;
    }

    void PPReset()
    {
        if (volumeProfile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.rounded.Override(false);
            vignette.intensity.Override(vignette_OriginIntensity);
        }

        if (volumeProfile.TryGet<FilmGrain>(out FilmGrain filmGrain))
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
        if(UI.Length > 0)
        {
            if (UI[(int)UI_TYPE.TALK] != null)
                StartCoroutine(SetTalkUI());
            if (UI[(int)UI_TYPE.ITEM] != null)
                StartCoroutine(SetItemUI());
            if (UI[(int)UI_TYPE.DOOR] != null)
                StartCoroutine(SetDoorUI());
            if (UI[(int)UI_TYPE.PAD] != null)
                StartCoroutine(SetPadUI());
            if (UI[(int)UI_TYPE.INVESTIGATE] != null)
                StartCoroutine(SetInvestigateUI());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsScript)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
                NextDialog();
        }

        if(IsQuiz)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                QuizUI.transform.GetChild(quizNum).gameObject.SetActive(false);
                quizNum = -1;
                IsQuiz = false;
            }
        }
    }
}
