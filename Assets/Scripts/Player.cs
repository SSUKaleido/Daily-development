using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHeart;
    private int mHeart;
    public int Heart
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        Heart = maxHeart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
