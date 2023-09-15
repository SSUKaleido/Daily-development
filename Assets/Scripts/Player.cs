using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Wrench;
    public int maxHeart;
    private int mHeart;

    public bool IsSmash;
    bool smashCool;
    public int Heart
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Awake()
    {
        Heart = maxHeart;
        smashCool = true;
    }
    public void CheckIsSmash()
    {
        if (!IsSmash)
        {
            Wrench.SetActive(false);
        }
        else
            Wrench.SetActive(true);
    }

    IEnumerator SmashCoolTime()
    {
        smashCool = false;
        yield return new WaitForSeconds(1f);
        smashCool = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsSmash)
        {
            if(Input.GetMouseButton(0) && smashCool)
            {
                StartCoroutine(SmashCoolTime());
                Wrench.GetComponent<Animator>().SetTrigger("DoSmash");
            }
        }
    }
}
