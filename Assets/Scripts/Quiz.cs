using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    public int quiz_num;

    public void ShowQuiz()
    {
        GameManager.Instance.UIManager.SetQuizUI(quiz_num);
    }
}
