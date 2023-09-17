using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    public GameObject Tutorial;

    bool IsPause;
    bool IsTutorial;

    public void ResumeButton()
    {
        Resume();
    }

    public void TutorialButton()
    {
        IsTutorial = true;
        Tutorial.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    void Pause()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
        IsPause = true;
    }

    void Resume()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = true;
        IsPause = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTutorial)
        {
            if(Input.anyKeyDown)
            {
                Tutorial.SetActive(false);
                IsTutorial = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsPause)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }
    }
}
