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
        playerObject = GameObject.Find("Player");
        mainCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        UIManager.FadeInStart();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}