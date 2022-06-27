using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    
    
    [SerializeField] private int _widthMinRoom;
    [SerializeField] private int _widthMaxRoom;
    [SerializeField] private int _heightMinRoom;
    [SerializeField] private int _heightMaxRoom;

    [SerializeField] private int _corridorWidth;
    [SerializeField] private int _minCorridorLenght;
    [SerializeField] private int _maxCorridorLenght;
    [SerializeField] private int _maxFeatures;

    [Header("Ascii")]
    [SerializeField] private Text _screen;

    [SerializeField] private bool isAscii;

    [Header("Not Ascii")] 
    [SerializeField] private GameObject _root;

    [SerializeField] private List<GameObject> _bottomWalls;
    [SerializeField] private List<GameObject> _rightLeftWalls;
    [SerializeField] private List<GameObject> _upperWalls;

    [SerializeField] private List<GameObject> _floors;
    
    [SerializeField] private GameObject _wallGameObject;
    
    [SerializeField] private GameObject _floorGameObject;

    public List<Feature> AllFeatures;
    private int _countFeatures;

    public void Init()
    {
        MapManager.map = new Tile[_mapHeight, _mapWidth];
        AllFeatures = new List<Feature>();
    }

    public void GenerateDungeon()
    {
        GenerateFeature(FeatureType.Room,new Wall(),true);

        GenerateFeature(FeatureType.Room,new Wall());
        DrawMap(isAscii);
    }

    private void GenerateFeature(FeatureType type, Wall startWall, bool isFirst = false)
    {
        int roomHeight = Random.Range(_heightMinRoom, _heightMaxRoom);
        int roomWidth = Random.Range(_widthMinRoom, _widthMaxRoom);

        int startPosX = 0;
        int startPosY = 0;

        

        if (!isFirst)
        {
            Feature prevFeature = AllFeatures[Random.Range(0, AllFeatures.Count-1)];
            Tile wall= prevFeature.tiles.Find(x => x is Wall);
            Debug.Log(prevFeature.tiles.IndexOf(wall));
            //Из первой попавшейся стены делаем дырку
            MapManager.map[wall.position.x, wall.position.y] = new Floor();
        }

        if(!isFirst) return;
        
        
        Feature feature = new Feature();
        feature.tiles = new List<Tile>();
        for (int y = 0; y < roomHeight; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                Tile tile;
                if (y == 0)
                {
                    tile = new Wall();
                }else if (x == 0)
                {
                    tile = new Wall();
                }else if (y == roomHeight-1)
                {
                    tile = new Wall();
                }else if (x == roomWidth-1)
                {
                    tile = new Wall();
                }
                else
                {
                    tile = new Floor();
                }

                tile.position = new Vector2Int(x, y);
                feature.tiles.Add(tile);
                MapManager.map[x,y] = tile;
            }
        }

        AllFeatures.Add(feature);

    }


    

    private void DrawMap(bool isAscii)
    {
        if (isAscii)
        {
            string asciiMap = "";

            for (int y = _mapHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    if (MapManager.map[x, y] != null)
                    {
                        if (MapManager.map[x, y] is Wall)
                        {
                            asciiMap += "#";

                        }else if (MapManager.map[x, y] is Floor)
                        {
                            asciiMap += ".";

                        }
                    }
                }
            }

            _screen.text = asciiMap;
        }
        else
        {
            for (int y = _mapHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    if (MapManager.map[x, y] != null)
                    {
                        if (MapManager.map[x, y] is Wall)
                        {
                            var wall=Instantiate(_wallGameObject,_root.transform);
                            wall.transform.position = new Vector2(x*2, y*2);
                        }else if (MapManager.map[x, y] is Floor)
                        {
                            var floor=Instantiate(_floors[Random.Range(0,_floors.Count-1)],_root.transform);
                            floor.transform.position = new Vector2(x*2, y*2);
                        }
                    }
                    
                }
            }

        }
    }

    private void PutTile(Vector2 pos, string type)
    {
        switch (type)
        {
            case"Wall":
                                
                var wall=Instantiate(_wallGameObject,_root.transform);
                wall.transform.position = new Vector2((pos.x-30f)*2, (pos.y-30f)*2);
                break;
            case "Floor":
                var floor=Instantiate(_floors[Random.Range(0,_floors.Count-1)],_root.transform);
                floor.transform.position = new Vector2((pos.x-30f)*2, (pos.y-30f)*2);
                break;
                                
        }
    }
    
}
