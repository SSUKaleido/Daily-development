using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public new void GetItem()
    {
        switch (ItemName)
        {
            case "���� ����":
                if (GameManager.Instance.InventoryManager.GetItem("��ġ"))
                {
                    base.GetItem();
                }
                else
                    GameManager.Instance.UIManager.StartTextUI("���� �ʴ´�");
                break;
            case null:
                base.GetItem();
                break;
        }
    }
}
