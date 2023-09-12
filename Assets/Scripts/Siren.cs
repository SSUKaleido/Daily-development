using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour
{
    public Light[] sirenLight;

    private IEnumerator PlaySiren()
    {
        foreach(var s in sirenLight)
        {
            if (s.GetComponent<Light>().enabled)
                s.GetComponent<Light>().enabled = false;
            else
                s.GetComponent<Light>().enabled = true;
        }
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(PlaySiren());
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySiren());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
