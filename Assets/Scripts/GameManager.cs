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
    public InventoryManager InventoryManager;

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