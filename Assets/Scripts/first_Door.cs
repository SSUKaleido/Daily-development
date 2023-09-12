using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_Door : MonoBehaviour
{
    Animator anim;
    Transform player;
    Transform door;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameManager.Instance.playerObject.transform;
        door = transform;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, door.position);
        
        if (distance <= 5)
        {
            anim.SetBool("Near", true);

        }
        else
        {
            anim.SetBool("Near", false);
        }
    }
}
