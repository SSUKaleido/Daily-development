using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.Instance.playerObject)
        {
            GameManager.Instance.UIManager.FadeOutStart(sceneName);
        }
    }
}
