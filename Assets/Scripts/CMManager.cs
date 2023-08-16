using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameraList;

    public void PlayScene(int num)
    {
        cameraList[num].GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
