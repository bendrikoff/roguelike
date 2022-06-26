using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public static Tile[,] map;
}
public class Tile
{
    public int xPosition;
    public int yPosition;
    public GameObject baseObject;
    public string type;
}


public class Wall
{
    public List<Vector2Int> positions;
    public Directions direction;
    public int lenght;
    public bool hasFeature = false;
    
}

public class Feature
{
    public List<Vector2Int> positions;
    public Wall[] walls;
    public FeatureType type;
    public int width;
    public int height;
}
