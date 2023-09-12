using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public new void GetItem()
    {
        switch (ItemName)
        {
            case "°¨¿Á ¿­¼è":
                if (GameManager.Instance.InventoryManager.GetItem("·»Ä¡"))
                {
                    base.GetItem();
                }
                else
                    GameManager.Instance.UIManager.StartTextUI("´êÁö ¾Ê´Â´Ù");
                break;
            case null:
                base.GetItem();
                break;
        }
    }
}
