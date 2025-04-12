using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int colorIndex = 0;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.x += 0.1f * Time.deltaTime * speed;
        transform.position = position;
        
    }

    // Ran when a car is on a node, decides the node it travels to
    public Node DecideDirection(Node currentNode) {
        List<Node> nodeList = currentNode.connectedNodes;

        float minimum = currentNode.colorDistances[colorIndex];
        //Int makes a variable that is an integer value. String makes a
        int minimumIndex = -1;
        for (int i = 0; i < nodeList.Capacity; i++) {
            if (minimum > nodeList[i].colorDistances[colorIndex]) {
                minimumIndex = i;
                minimum = nodeList[i].colorDistances[colorIndex];
            }
        }
        

        return nodeList[minimumIndex];
    }
}
