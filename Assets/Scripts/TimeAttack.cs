using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeAttack : MonoBehaviour
{
    public GameObject Robot;

    public bool IsSceneTwo;
    public bool IsSceneFinal;
    public int time_minutes;
    TextMeshProUGUI minutesText;
    TextMeshProUGUI secondsText;

    private IEnumerator TimePass()
    { 
        int m = time_minutes;
        int s = 0;
        while(m >= 0)
        {
            minutesText.text = m.ToString();
            secondsText.text = s.ToString("00");
            if (s > 0)
                s--;
            else
            {
                s = 59;
                m--;
            }
            yield return new WaitForSeconds(1f);
        }
        EndTimer();
    }

    void EndTimer()
    {
        if(IsSceneTwo)
        {
            //문 열고 로봇 소환
            Destroy(GameObject.Find("Door"));
            GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.EXPLOSION, false, false);
            Instantiate(Robot, GameObject.Find("TimeOver").transform.position, Quaternion.identity);
        }
        else if (IsSceneFinal)
        {
            //문 열고 로봇 소환
            Destroy(GameObject.Find("middle_door"));
            GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.EXPLOSION, false, false);
            Instantiate(Robot, GameObject.Find("TimeOver").transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void Awake()
    {
        minutesText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        secondsText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimePass());
    }
}
