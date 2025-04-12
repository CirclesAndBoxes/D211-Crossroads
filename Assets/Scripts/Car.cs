using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    public int colorIndex = 0;

    // Probably nodes per second
    public float speed;
    

    // 0 is deciding, 1 is moving
    int motionState = 0;

    public Node toNode;
    public Node fromNode;

    // the proportion of distance the car has traveled
    float singleDistance = 0;

    //float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        if (colorIndex == 0) {
            render.color = Color.gray;
        } else if (colorIndex == 1) {
            render.color = Color.blue;
        } else if (colorIndex == 2) {
            render.color = Color.red;
        } else if (colorIndex == 3) {
            render.color = Color.yellow;
        } else if (colorIndex == 4) {
            render.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (motionState == 1) {
            Vector3 diff = toNode.transform.position - fromNode.transform.position;
            
            // Increases the proportion of distance that the thing has traveled
            singleDistance += Time.deltaTime * speed;

            if (singleDistance >= 1) {
                transform.position = toNode.transform.position;
                singleDistance = 0;
                fromNode = toNode;
                motionState = 0;
            } else {
                transform.position += speed * Time.deltaTime * diff;
            }
        } else {
            toNode = DecideDirection(fromNode);
            transform.position = fromNode.transform.position;
            motionState = 1;
        }
    }

    // Ran when a car is on a node, decides the node it travels to
    public Node DecideDirection(Node currentNode) {
        List<Node> nodeList = currentNode.connectedNodes;

        if (currentNode.colorDistances[colorIndex] <= 0.01) {
            Destroy(gameObject);
            return currentNode;
        }

        float minimum = nodeList[0].colorDistances[colorIndex];
        int minimumIndex = 0;
        for (int i = 0; i < nodeList.Capacity; i++) {
            if (minimum > nodeList[i].colorDistances[colorIndex]) {
                minimumIndex = i;
                minimum = nodeList[i].colorDistances[colorIndex];
            }
        }
        
        return nodeList[minimumIndex];
    }
}
