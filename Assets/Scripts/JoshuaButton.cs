using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoshuaButton : MonoBehaviour
{
    public int newNodes = 5;
    public float timer = 5;


    public void Update()
    {
        
        if (newNodes > 0) {
            timer -= Time.deltaTime;
            if (timer < 0) {
                timer = 5;
                newNodes -= 1;
                
                Node[] allTargets = FindObjectsOfType<Node>();

                // Create a list with distances
                List<Node> sortedByDistance = allTargets
                    .OrderBy(obj => Vector2.Distance(transform.position, obj.transform.position))
                    .ToList();
                Node node1 = sortedByDistance[0];

                node1.ConnectNode(sortedByDistance[1]);
            }
            
        }    
    }

    public void doOneThing () {
            Debug.Log("Button Pressed");
        }

}
