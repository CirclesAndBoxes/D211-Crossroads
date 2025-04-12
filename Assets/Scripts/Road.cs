using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Road : MonoBehaviour
{
    public Node startNode;
    public Node endNode;
    // Start is called before the first frame update

    void Start()
    {
        Vector2 pos1 = startNode.gameObject.transform.position;
        Vector2 pos2 = endNode.gameObject.transform.position;
        Vector2 displacement = pos1 - pos2;
        float dist = displacement.magnitude;
        float angle = Mathf.Atan2( displacement.y,displacement.x) * Mathf.Rad2Deg;

        transform.localScale = new Vector3(dist, 1, 1);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = (pos1 + pos2)/2;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
