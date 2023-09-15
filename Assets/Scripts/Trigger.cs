using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Trigger : MonoBehaviour
{
    public string triggerName;

    public bool IsTrap;

    public GameObject TrapDoor;
    public GameObject robots;
    public GameObject Robot;
    public GameObject door;
    public GameObject VFX;

    void SceneTwo()
    {
        Instantiate(Robot, robots.transform.position, Quaternion.Euler(0, 180, 0));
        robots.SetActive(false);
    }

    public void SceneEnd(string TN)
    {
        switch (TN)
        {
            case "1":
                GameManager.Instance.playerObject.GetComponent<FirstPersonController>().crosshair = true;
                GameManager.Instance.playerObject.GetComponent<FirstPersonController>().useSprintBar = true;
                GameManager.Instance.playerObject.GetComponent<FirstPersonController>().playerCanMove = true;
                GameManager.Instance.playerObject.GetComponent<FirstPersonController>().Start();
                robots.SetActive(true);
                GameManager.Instance.playerObject.transform.rotation = Quaternion.Euler(GameManager.Instance.playerObject.transform.rotation.x, 270f, GameManager.Instance.playerObject.transform.rotation.z);
                GameManager.Instance.mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                GameManager.Instance.SoundManager.PlayAudio((int)SOUND.BGM, (int)BGM_NAME.SIREN, true, false);
                break;
            case "3":
                GameManager.Instance.playerObject.transform.rotation = Quaternion.Euler(GameManager.Instance.playerObject.transform.rotation.x, 0, GameManager.Instance.playerObject.transform.rotation.z);
                GameManager.Instance.mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = true;
                GameManager.Instance.playerObject.GetComponent<Player>().IsSmash = true;
                transform.GetChild(0).gameObject.SetActive(false);
                GameManager.Instance.UIManager.UI[(int)UI_TYPE.ATTACK].SetActive(false);
                VFX.SetActive(true);
                Invoke("SceneTwo", 4f);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.Instance.playerObject)
        {
            switch(triggerName)
            {
                case "1":
                    GameManager.Instance.playerObject.GetComponent<FirstPersonController>().crosshair = false;
                    GameManager.Instance.playerObject.GetComponent<FirstPersonController>().useSprintBar = false;
                    GameManager.Instance.playerObject.GetComponent<FirstPersonController>().playerCanMove = false;
                    GameManager.Instance.playerObject.GetComponent<FirstPersonController>().Start();
                    //GameManager.Instance.CMManager.PlayScene(2);
                    transform.GetChild(0).gameObject.GetComponent<PlayableDirector>().Play();
                    break;
                case "2":
                    door.GetComponent<Door>().StartCloseDoor();
                    GameManager.Instance.SoundManager.currentBGM = BGM_NAME.SAFE1;
                    GameManager.Instance.UIManager.HorrorReset();
                    GameManager.Instance.UIManager.SetTimeAttackUI();
                    break;
                case "3":
                    GameManager.Instance.playerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
                    transform.GetChild(0).gameObject.GetComponent<PlayableDirector>().Play();
                    break;
                case "4":
                    if (IsTrap)
                        TrapDoor.SetActive(false);
                    break;
                case "5":
                    GetComponent<AudioSource>().Play();
                    break;
                case "6":
                    door.transform.GetComponent<Animator>().SetBool("Near", false); //¹® ´Ý±â
                    door.transform.GetComponent<first_Door>().IsRecog = false;
                    GameManager.Instance.UIManager.SetTimeAttackUI();
                    break;
                case "ending":
                    GameManager.Instance.UIManager.FadeOutStart("Stage_End");
                    break;
            }
        }
        GetComponent<BoxCollider>().enabled = false;
    }
}
