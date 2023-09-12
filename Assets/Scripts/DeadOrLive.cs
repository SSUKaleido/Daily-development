using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadOrLive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UIManager.SetAttackUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.playerObject.GetComponent<Player>().Wrench.GetComponent<Animator>().SetTrigger("DoSmash");
            transform.parent.GetComponent<Trigger>().SceneEnd("3");
            gameObject.SetActive(false);
        }
    }
}
