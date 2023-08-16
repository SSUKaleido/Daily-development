using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemName;
    public bool HighLightItem;
    Light highlight;

    public void GetItem()
    {
        //아이템 습득 코드
        Destroy(gameObject);
    }
    private IEnumerator Highlight()
    {
        while (true)
        {
            if (HighLightItem)
            {
                highlight.enabled = true;
            }
            else
            {
                highlight.enabled = false;
            }
            HighLightItem = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        ItemName = gameObject.name;
        highlight = transform.GetComponentInChildren<Light>();
        StartCoroutine("Highlight");
    }
}
