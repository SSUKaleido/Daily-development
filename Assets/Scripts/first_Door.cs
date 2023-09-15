using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_Door : MonoBehaviour
{
    Animator anim;
    Transform player;
    Transform door;

    public bool IsFinal;
    public bool IsMiddle;
    public bool IsRecog;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameManager.Instance.playerObject.transform;
        door = transform;

        if (IsFinal || IsMiddle)
            IsRecog = false;
        else
            IsRecog = true;
    }
    // Update is called once per frame
    void Update()
    {       
        if(IsMiddle)
        {
            if(GameManager.Instance.InventoryManager.GetItem("Item1") && GameManager.Instance.InventoryManager.GetItem("Item2"))
            {
                IsRecog = true;
                IsMiddle = false;
            }           
        }

        if(IsRecog)
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
}
