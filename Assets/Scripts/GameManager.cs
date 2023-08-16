using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
    private static GameManager _instance;

    public SoundManager SoundManager;
    public CMManager CMManager;
    public UIManager UIManager;

    public GameObject playerObject;
    public Camera mainCamera;
    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
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