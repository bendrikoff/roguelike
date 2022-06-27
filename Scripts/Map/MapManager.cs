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
    public Vector2Int position;
}


public class Wall:Tile
{
    public Directions direction;
}

public class Floor : Tile
{
    
}

public class Feature
{
    public FeatureType type;
    public int width;
    public int height;
    public List<Tile> tiles;
}
