using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingPlacer: MonoBehaviour
{
    public GameObject buildingPrefab;
    public Color buildingColor = Color.red;
    public BuildingType buildingType = BuildingType.CarGenerator;

    public GridManager gridManager;

    private GameObject previewObject;
    private SpriteRenderer previewRenderer;
    private bool canPlace = true;
    
    void Start()
    {
        previewObject = Instantiate(buildingPrefab, Vector3.zero, Quaternion.identity);
        previewObject.name = "BuildingPreview";

        // Disable any non-visual components
        foreach (var component in previewObject.GetComponents<Component>())
        {
            if (!(component is Transform) && !(component is SpriteRenderer)) // So can't tranform or move or is even a sprite
            {
                Destroy(component);
            }
        }
        
        previewRenderer = previewObject.GetComponent<SpriteRenderer>();
        
        // Make it semi-transparent
        Color previewColor = buildingColor;
        previewColor.a = 0.5f;
        previewRenderer.color = previewColor;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // So get center of closest grid cell
        Vector3 snappedPosition = gridManager.SnapToGrid(mousePosition);
        previewObject.transform.position = snappedPosition;

        // Check if position is valid for placement
        Vector2Int gridPos = gridManager.WorldToGridPosition(snappedPosition);
        canPlace = gridManager.IsCellFree(gridPos.x, gridPos.y);

        // Update preview color
        Color previewColor = buildingColor;
        previewColor.a = canPlace ? 0.7f : 0.3f;
        previewRenderer.color = previewColor;
        
        float time = Time.time;
        if (Input.GetMouseButton(0) && canPlace)
        {
            CreateBuilding(snappedPosition);
            
            // Mark the grid cell as occupied
            gridManager.OccupyCell(gridPos.x, gridPos.y);
        }
    }

    void CreateBuilding(Vector3 position)
    {
        // Create the building
        GameObject newBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
        
        // Get the Building component
        Building building = newBuilding.GetComponent<Building>();
        
        // Initialize it
        if (building != null)
        {
            building.Initialize(buildingType, buildingColor);
        }
    }
}