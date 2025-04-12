using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize = 1f;
    public int gridWidth = 20;
    public int gridHeight = 20;

    // Optional 2D array to store grid cell data
    private GridCell[,] grid;
    private GameObject gridVisualization;

    // Start is called before the first frame update
    void Start()
    {
        grid = new GridCell[Screen.width, Screen.height]; // Default is false
        CreateGridVisualization();
    }

    private void CreateGridVisualization()
    {
        gridVisualization = new GameObject("GridVisualization");

        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        // lineMaterial.color = gridColor;

        for (int y = 0; y <= gridHeight; y++)
        {
            GameObject line = new GameObject("HLine_" + y);
            line.transform.parent = gridVisualization.transform;

            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = 2;
            
            Vector3 startPos = new Vector3(-gridWidth * cellSize / 2, y * cellSize - gridHeight * cellSize / 2, 0);
            Vector3 endPos = new Vector3(gridWidth * cellSize / 2, y * cellSize - gridHeight * cellSize / 2, 0);
            
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }

        for (int x = 0; x <= gridWidth; x++)
        {
            GameObject line = new GameObject("VLine_" + x);
            line.transform.parent = gridVisualization.transform;
            
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = 2;
            
            Vector3 startPos = new Vector3(x * cellSize - gridWidth * cellSize / 2, -gridHeight * cellSize / 2, 0);
            Vector3 endPos = new Vector3(x * cellSize - gridWidth * cellSize / 2, gridHeight * cellSize / 2, 0);
            
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }
    }

    public void FillGrid(int x, int y, GameObject road, GameObject node)
    {
        // Check if the position is within grid bounds
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
        {
            return;
        }
        
        grid[x, y].isRoad = true;
        grid[x, y].road = road;
        grid[x, y].node = node;
    }

    public Vector3 GridToWorldPosition(int x, int y)
    {
        // gridWidth * cellSize / 2 basicaly first gridWidth * cellSize gives us size of the whole grid in pixels
        // then we divide it by 2 to get the center of the grid
        // We need this so a 10x10 grid is not in between -10 and 10
        float worldX = x * cellSize + cellSize / 2 - gridWidth * cellSize / 2;
        float worldY = y * cellSize + cellSize / 2 - gridHeight * cellSize / 2;
        
        return new Vector3(worldX, worldY, 0);
    }

        public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x + gridWidth * cellSize / 2) / cellSize);
        int y = Mathf.FloorToInt((worldPosition.y + gridHeight * cellSize / 2) / cellSize);
        
        // Clamp values to the grid bounds
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        y = Mathf.Clamp(y, 0, gridHeight - 1);
        
        return new Vector2Int(x, y);
    }

    public Vector3 SnapToGrid(Vector3 worldPosition)
    {
        Vector2Int gridPos = WorldToGridPosition(worldPosition);
        return GridToWorldPosition(gridPos.x, gridPos.y);
    }
    
    public bool IsCellFree(int x, int y)
    {
        // Check if the position is within grid bounds
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
        {
            return false;
        }
        
        return !grid[x, y].isRoad;
    }

    public GameObject GetNodeAt(int x, int y)
    {
        // Check if the position is within grid bounds
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
        {
            return null;
        }
        
        return grid[x, y].node;
    }
}