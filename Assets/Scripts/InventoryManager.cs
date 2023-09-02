using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM { }
public class InventoryManager : MonoBehaviour
{
    public List<GameObject> ItemList;
    // Start is called before the first frame update
    public void AddItem(GameObject item)
    {
        ItemList.Add(item);
    }

    public void DeleteItem(GameObject item)
    {
        foreach(var i in ItemList)
        {
            if (i == item)
                ItemList.Remove(i);
        }
    }
    public void DeleteItem(string item)
    {
        foreach (var i in ItemList)
        {
            if (i.GetComponent<Item>().ItemName == item)
                ItemList.Remove(i);
        }
    }

    public GameObject GetItem(string item)
    {
        foreach (var i in ItemList)
        {
            if (i.GetComponent<Item>().ItemName == item)
                return i;
        }
        return null;
    }

    void Start()
    {
        ItemList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
