using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;

    public SoundManager SoundManager;
    public CMManager CMManager;
    public UIManager UIManager;
    public InventoryManager InventoryManager;

    public GameObject playerObject;
    public Camera mainCamera;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SceneMove(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        CMManager = GameObject.Find("CMManager").GetComponent<CMManager>();
        UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        UIManager.FadeInStart();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Stage_One":
                SoundManager.currentBGM = BGM_NAME.AMB1;
                UIManager.StartTutorialUI();
                break;
            case "Stage_Two":
                SoundManager.currentBGM = BGM_NAME.AMB2;
                playerObject.GetComponent<Player>().Wrench.SetActive(true);
                break;
        }

        if (SceneManager.GetActiveScene().name != "Stage_Start")
        {
            playerObject = GameObject.Find("Player");
            mainCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
            SoundManager.PlayAudio((int)SOUND.BGM, (int)SoundManager.currentBGM, true, false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Tab))
            UIManager.SetInventoryUI();
    }
}