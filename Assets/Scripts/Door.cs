using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string DoorName;
    // Start is called before the first frame update
    public void OpenDoor()
    {
        switch(DoorName)
        {
            case "°¨¿Á¹®":
                if (GameManager.Instance.InventoryManager.GetItem("°¨¿Á ¿­¼è"))
                {
                    transform.tag = "Untagged";
                    GetComponent<AudioSource>().Play();
                    StartCoroutine(OpenJailDoor());
                }
                else
                    GameManager.Instance.UIManager.StartTextUI("¿­¸®Áö ¾Ê´Â´Ù");
                break;
        }
    }

    private IEnumerator OpenJailDoor()
    {
        Transform pivot = transform.parent;
        for (float y = 0; y <= 130; y += 0.5f)
        {
            pivot.localRotation = Quaternion.Euler(pivot.rotation.x, y, pivot.rotation.z);
            yield return new WaitForFixedUpdate();
        }
        pivot.localRotation = Quaternion.Euler(pivot.rotation.x, 130, pivot.rotation.z);
    }

    public void StartCloseDoor()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor()
    {
        for (float y = transform.position.y; y >= 0; y -= 0.1f)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return null;
        }
        Invoke("StompSound", Random.Range(8, 10));
    }

    void StompSound()
    {
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        Invoke("StompSound", Random.Range(6, 10));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
