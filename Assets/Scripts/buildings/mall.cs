using Unity.Mathematics;
using UnityEngine;

public class Mall : MonoBehaviour
{
    [Header("Mall Settings")]
    public Transform entryPoint;
    public float consumptionInterval = 8f; // Time in seconds between consuming resources
    public int resourcesNeeded = 0;
    public int maxResourcesNeeded = 3;
    public Node entrance;
    public int colorIndex;

    // Consumption timer
    private float consumptionTimer = 0f;
    
    void Start()
    {
        // If no entry point is defined, use the transform position
        if (entryPoint == null)
            entryPoint = transform;
        
        // Set the node that a Mall is connected to as 0
        entrance.distanceSet[colorIndex] = true;
        entrance.colorDistances[colorIndex] = 0;

        // Start with some needs
        resourcesNeeded = 1;
    }
    
    void Update()
    {
        // Update mall consumption timer
        consumptionTimer += Time.deltaTime;
        
        // Generate a need for resources at regular intervals
        if (consumptionTimer >= consumptionInterval && resourcesNeeded < maxResourcesNeeded)
        {
            resourcesNeeded++;
            consumptionTimer = 0f;
            
            Debug.Log($"Mall needs a resource. Resources needed: {resourcesNeeded}/{maxResourcesNeeded}");
        }
    }
    
    // Deliver a resource to this mall
    public bool DeliverResource()
    {
        // Only accept if we need resources
        if (resourcesNeeded <= 0)
            return false;
            
        // Consume a resource
        resourcesNeeded--;
        Debug.Log($"Resource delivered to mall. Resources still needed: {resourcesNeeded}");
        
        return true;
    }
    
    // Show the entry point in the editor
    private void OnDrawGizmos()
    {
        if (entryPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(entryPoint.position, 0.3f);
        }
    }
}