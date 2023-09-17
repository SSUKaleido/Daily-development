using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Robot")
            other.GetComponent<Robot>().Stun();
    }
}
