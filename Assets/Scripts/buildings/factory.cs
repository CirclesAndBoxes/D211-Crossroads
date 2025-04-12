using UnityEngine;

public class Factory : MonoBehaviour
{
    [Header("Factory Settings")]
    public Transform spawnPoint;
    public int colorIndex = 0;
    public float productionInterval = 5f; // Time in seconds between producing resources
    public int resourceCount = 0;
    public int maxResources = 5;
    
    // Production timer
    private float productionTimer = 0f;
    
    void Start()
    {
        // If no spawn point is defined, use the transform position
        if (spawnPoint == null)
            spawnPoint = transform;
            
        // Start with some resources
        resourceCount = 2;
    }
    
    void Update()
    {
        // Update factory production timer
        productionTimer += Time.deltaTime;
        
        // Produce a resource at regular intervals
        if (productionTimer >= productionInterval && resourceCount < maxResources)
        {
            resourceCount++;
            productionTimer = 0f;
            
            Debug.Log($"Factory produced a resource. Resources: {resourceCount}/{maxResources}");
        }
    }
    
    // Spawn a car from this factory
    public GameObject SpawnCar(GameObject carPrefab)
    {
        if (carPrefab == null || spawnPoint == null)
            return null;
            
        // Only spawn if we have resources
        if (resourceCount <= 0)
            return null;
            
        // Spawn the car
        GameObject car = Instantiate(carPrefab, spawnPoint.position, Quaternion.identity);
        
        // Set the car's properties
        Car carComponent = car.GetComponent<Car>();
        if (carComponent != null)
        {
            carComponent.colorIndex = this.colorIndex;
            carComponent.speed = 5f; // Default speed
        }
        
        // Reduce the resource count
        resourceCount--;
        
        return car;
    }
    
    // Show the spawn point in the editor
    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(spawnPoint.position, 0.3f);
        }
    }
}