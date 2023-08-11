using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject FadePannel;

    public void FadeInStart()
    {
        StartCoroutine(FadeIn());
    }
    
    public void FadeOutStart()
    {
        StartCoroutine(FadeOut());
    }
    //페이드 아웃
    IEnumerator FadeIn()
    {
        FadePannel.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FadePannel.SetActive(false);
    }

    //페이드 인
    IEnumerator FadeOut()
    {
        FadePannel.SetActive(true);
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = FadePannel.GetComponent<Image>().color;
            c.a = f;
            FadePannel.GetComponent<Image>().color = c;
            yield return null;
        }
    }
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
