using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string[] script;
    public Transform LookPivot;
    Animator animator;
    bool IsLook;
    public void TalkStart()
    {
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().crosshair = false;
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
        GameManager.Instance.playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameManager.Instance.UIManager.UI[(int)UI_TYPE.TALK].SetActive(false);
        GameManager.Instance.UIManager.SetDialogUI(this, script);
        if(LookPivot != null)
            GameManager.Instance.CMManager.cameraList[1].LookAt = LookPivot;
        else
            GameManager.Instance.CMManager.cameraList[1].LookAt = transform;
        GameManager.Instance.CMManager.PlayScene(1);
        IsLook = true;
    }

    public void TalkEnd()
    {
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = true;
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().crosshair = true;
        GameManager.Instance.CMManager.StopScene(1);
        IsLook = false;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsLook)
        {
            Vector3 targetPosition = new Vector3(GameManager.Instance.playerObject.transform.position.x, transform.position.y, GameManager.Instance.playerObject.transform.position.z);
            transform.LookAt(targetPosition);
        }
    }
}
