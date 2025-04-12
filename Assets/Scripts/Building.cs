using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    CarGenerator,
    Destination
}

public class Building : MonoBehaviour
{
    public BuildingType Type;
    public Color Color;
    public Transform EntryPoint;

    public void Initialize(BuildingType type, Color color) {
        Type = type;
        Color = color;

        GetComponent<SpriteRenderer>().color = color;
    }
}
