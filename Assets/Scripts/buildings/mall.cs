using UnityEngine;

public class Mall : MonoBehaviour
{
    [Header("Mall Settings")]
    public Transform entryPoint;
    public float consumptionInterval = 8f; // Time in seconds between consuming resources
    public int resourcesNeeded = 0;
    public int maxResourcesNeeded = 3;
    
    // Consumption timer
    private float consumptionTimer = 0f;
    
    void Start()
    {
        // If no entry point is defined, use the transform position
        if (entryPoint == null)
            entryPoint = transform;
            
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