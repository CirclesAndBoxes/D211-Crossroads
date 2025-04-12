using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Color buildingColor = Color.red;
    public BuildingType buildingType = BuildingType.CarGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateBuilding(Vector3.zero);
    }

    void CreateBuilding(Vector3 position)
    {
        GameObject newBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
        Building building = newBuilding.GetComponent<Building>();
        if (building != null)
        {
            building.Initialize(buildingType, buildingColor);
        }
    }
}
