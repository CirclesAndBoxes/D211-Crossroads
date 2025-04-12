using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float[] colorDistances = {1000000, 1000000, 1000000, 1000000, 1000000};
    public List<Node> connectedNodes;
    public Node nodePrefab;
    public Road roadPrefab;

    public bool[] distanceSet = {false, false, false, false, false};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets own distance based on distance of nearby
    // return: Whether any of the distances were updated
    public bool UpdateDistances()
    {
        float[] originalDistances = (float[]) colorDistances.Clone();

        for (int i = 0; i < colorDistances.Length; i++) {
            colorDistances[i] = connectedNodes[i].colorDistances.Min() + 1;
        }
        return !colorDistances.Equals(originalDistances);
    }

    // Updates self and others if self updates
    public void RecursiveUpdate(int updatesLeft = 500) {
        if(UpdateDistances() && updatesLeft > 0) {
            foreach (Node n in connectedNodes) {
                n.RecursiveUpdate(updatesLeft - 1);
            }
        }
    }

    // Creates a new node and connects them
    public void NewNode() {
        Node other = Instantiate(nodePrefab, new Vector2(1, 2), Quaternion.identity);
        other.gameObject.transform.position = Random.insideUnitCircle * 9;

        connectedNodes.Add(other);
        other.connectedNodes.Add(this);

        Road road = Instantiate(roadPrefab, new Vector2(1, 2), Quaternion.identity);
        road.startNode = this;
        road.endNode = other;
    } 

    // Connects one node with another
    public void ConnectNode(Node other) {
        if (!connectedNodes.Contains(other)) {
            // Add roads to each other
            connectedNodes.Add(other);
            other.connectedNodes.Add(this);

            // Add a road between roads
            Road road = Instantiate(roadPrefab, new Vector2(1, 2), Quaternion.identity);
            road.startNode = this;
            road.endNode = other;

            // update distances 
            RecursiveUpdate();
        }
    }
}
