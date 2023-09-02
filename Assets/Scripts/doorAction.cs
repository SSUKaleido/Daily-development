using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAction : MonoBehaviour
{
    bool flag;

    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == true) //¹®¿­±â
        {
            if (Door.transform.position.x >= 1.326f)
            {
                Door.transform.Translate(3+0.05f, 0, 7);
            }
        }
        if(flag == false)
        {
            if (Door.transform.position.x <= 2.122f)
            {   
                Door.transform.Translate(3-0.05f, 0, 7);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        flag = true;
    }
    private void OnTriggerExit(Collider other)
    {
        flag = false;
    }


}
