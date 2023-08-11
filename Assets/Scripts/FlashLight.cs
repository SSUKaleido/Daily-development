using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light flashlight;
    // Start is called before the first frame update
    void Awake()
    {
        flashlight = transform.GetChild(0).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.enabled)
                flashlight.enabled = false;
            else
                flashlight.enabled = true;
        }
    }
}
