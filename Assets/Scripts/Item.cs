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
        GameManager.Instance.InventoryManager.AddItem(gameObject);
        GameManager.Instance.UIManager.StartGetItemUI(ItemName);
        GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.ITEM, false, false);
        gameObject.SetActive(false);
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
    void Start()
    {
        if(ItemName == null)
        {
            ItemName = gameObject.name;
        }
        /*highlight = transform.GetComponentInChildren<Light>();
        highlight.enabled = false;*/
        //StartCoroutine("Highlight");
    }
}
