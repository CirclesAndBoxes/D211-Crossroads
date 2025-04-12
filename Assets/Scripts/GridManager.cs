// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GridManager : MonoBehaviour
// {
//     public float cellSize = 1f;
//     public int gridWidth = 20;
//     public int gridHeight = 20;
//     public Color gridColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
    
//     // Visual grid lines
//     private GameObject gridVisualization;
//     // Optional 2D array to store grid cell data
//     private GridCell[,] grid;

//     // Start is called before the first frame update
//     void Start()
//     {
//         grid = new GridCell[gridWidth, gridHeight]

//         for (int x = 0; x < gridWidth; x++)
//         {
//             for (int y = 0; y < gridHeight; y++)
//             {
//                 grid[x, y] = new GridCell();

//             }
//         }

//         CreateGridVisualization();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


// public class GridCell
// {
//     public bool isOccupied;
//     public GameObject occupyingObject;
//     public Vector3 worldPosition;
// }