using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingPlacer: MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject nodePrefab;
    public Color roadColor = Color.gray;
    public GridManager gridManager;

    private GameObject previewObject;
    private SpriteRenderer previewRenderer;
    private bool canPlace = true;
    
    void Start()
    {
        previewObject = Instantiate(roadPrefab, Vector3.zero, Quaternion.identity);
        previewObject.name = "RoadPreview";

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
        Color previewColor = Color.white;
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
        Color previewColor = Color.white;
        previewColor.a = canPlace ? 0.7f : 0.3f;
        previewRenderer.color = previewColor;
        
        float time = Time.time;
        if (Input.GetMouseButton(0) && canPlace)
        {
            CreateRoad(snappedPosition);
        }
    }

    void CreateRoad(Vector3 position)
    {
        Vector2Int gridPos = gridManager.WorldToGridPosition(position);

        // Create the road
        GameObject newRoad = Instantiate(roadPrefab, position, Quaternion.identity);
        GameObject newNode = Instantiate(nodePrefab, position, Quaternion.identity);
        Node nodeComponent = newNode.GetComponent<Node>();


        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),   // North
            new Vector2Int(1, 1),   // North-East
            new Vector2Int(1, 0),   // East
            new Vector2Int(1, -1),  // South-East
            new Vector2Int(0, -1),  // South
            new Vector2Int(-1, -1), // South-West
            new Vector2Int(-1, 0),  // West
            new Vector2Int(-1, 1)   // North-West
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int neighborPos = gridPos + dir;

            if (!gridManager.IsCellFree(neighborPos.x, neighborPos.y))
            {
                // SO if node and road at this point
                GameObject neighborNode = gridManager.GetNodeAt(neighborPos.x, neighborPos.y);
                Node neighborComponent = neighborNode.GetComponent<Node>();
                neighborComponent.ConnectNode(nodeComponent);
            }
        }
        gridManager.FillGrid(gridPos.x, gridPos.y, newRoad, newNode);
    }
}