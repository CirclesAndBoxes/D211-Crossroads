using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_script : MonoBehaviour
{   
     public Color newColor;
    public int ColorId = 0;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        // Choose color based on ID
        if (ColorId == 1)
        {
            newColor = Color.red;
        }

        rend.color = newColor;
    }

    void Update()
    {
        // Empty for now
    }
}
