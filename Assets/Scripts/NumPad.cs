using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPad : MonoBehaviour
{
    public GameObject Door;
    public GameObject Trigger;

    public int pad_num;
    private int[] answer;
    private int[] input;
    int currentIndex;

    bool IsInput;

    public void StartInput()
    {
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
        GameManager.Instance.UIManager.NumPadUI.SetActive(true);
        currentIndex = 0;
        IsInput = true;
        if (pad_num == 1 || pad_num == 3)
        {
            input = new int[4];
            GameManager.Instance.UIManager.SetNumPadUI(4);
        }
        else if (pad_num == 2)
        {
            input = new int[3];
            GameManager.Instance.UIManager.SetNumPadUI(3);
        }
    }

    void EndInput()
    {
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = true;
        GameManager.Instance.UIManager.NumPadUI.SetActive(false);
        if(Check()) //정답일 경우
        {
            if(pad_num == 3)
            {
                Door.GetComponent<first_Door>().IsRecog = true;
                GameManager.Instance.UIManager.TimeAttackUI.SetActive(false);
            }
            else
            {
                if(pad_num == 2)
                    GameManager.Instance.UIManager.TimeAttackUI.SetActive(false);

                Door.GetComponent<Door>().StartOpenDoor();
            }
        }
        else
        {
            if (pad_num == 1)
            {
                GameManager.Instance.UIManager.StartTextUI("비밀번호가 틀린 것 같다");
            }
            else if (pad_num == 2)
            {
                GameManager.Instance.UIManager.TimeAttackUI.SetActive(false);
                Door.GetComponent<Door>().StartOpenDoor();
                Trigger.GetComponent<Trigger>().IsTrap = true;
            }
        }
    }

    bool Check()
    {
        for(int i = 0; i < answer.Length; i++)
        {
            if (answer[i] != input[i])
                return false;
        }
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(pad_num == 1)
        {
            answer = new int[4];
            answer[0] = 0;
            answer[1] = 9;
            answer[2] = 1;
            answer[3] = 8;
        }
        else if (pad_num == 2)
        {
            answer = new int[3];
            answer[0] = 5;
            answer[1] = 0;
            answer[2] = 5;
        }
        else if (pad_num == 3)
        {
            answer = new int[4];
            answer[0] = 1;
            answer[1] = 1;
            answer[2] = 7;
            answer[3] = 2;
        }
    }
    void Update()
    {
        if(IsInput)
        {
            if(currentIndex >= answer.Length)
            {
                EndInput();
                IsInput = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                GameManager.Instance.UIManager.InputNumPadUI(0, currentIndex);
                input[currentIndex++] = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                GameManager.Instance.UIManager.InputNumPadUI(1, currentIndex);
                input[currentIndex++] = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
{
                GameManager.Instance.UIManager.InputNumPadUI(2, currentIndex);
                input[currentIndex++] = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                GameManager.Instance.UIManager.InputNumPadUI(3, currentIndex);
                input[currentIndex++] = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                GameManager.Instance.UIManager.InputNumPadUI(4, currentIndex);
                input[currentIndex++] = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                GameManager.Instance.UIManager.InputNumPadUI(5, currentIndex);
                input[currentIndex++] = 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                GameManager.Instance.UIManager.InputNumPadUI(6, currentIndex);
                input[currentIndex++] = 6;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                GameManager.Instance.UIManager.InputNumPadUI(7, currentIndex);
                input[currentIndex++] = 7;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                GameManager.Instance.UIManager.InputNumPadUI(8, currentIndex);
                input[currentIndex++] = 8;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                GameManager.Instance.UIManager.InputNumPadUI(9, currentIndex);
                input[currentIndex++] = 9;
            }
        }
    }
}
