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
        
        for (int i = 0; i < 500; i++)
        {
            Feature originFeature;
            if (AllFeatures.Count == 1)
            {
                originFeature = AllFeatures[0];
            }
            else
            {
                originFeature = AllFeatures[Random.Range(0, AllFeatures.Count-1)];
            }

            Wall wall = ChoseWall(originFeature);
            
            if(wall==null) continue;
            
            FeatureType type;

            if (originFeature.type == FeatureType.Room)
            {
                type = FeatureType.Corridor;
            }
            else
            {
                if (Random.Range(0, 100) < 90)
                {
                    type = FeatureType.Room;
                }
                else
                {
                    type = FeatureType.Corridor;
                }
            }
            GenerateFeature(type,wall);
            if (_countFeatures >= _maxFeatures) break;
        }
        DrawMap(isAscii);
    }

    private void GenerateFeature(FeatureType type, Wall startWall, bool isFirst = false)
    {

        Feature room = new Feature();
        room.positions = new List<Vector2Int>();

        int roomWidth = 0;
        int roomHeight = 0;

        if (type == FeatureType.Room)
        {
             roomWidth = Random.Range(_widthMinRoom, _widthMaxRoom);
             roomHeight = Random.Range(_heightMinRoom, _heightMaxRoom);
        }else if (type == FeatureType.Corridor)
        {
            switch (startWall.direction)
            {
                case Directions.South:
                    roomWidth = _corridorWidth;
                    roomHeight = Random.Range(_minCorridorLenght, _maxCorridorLenght);
                    break;
                case Directions.North:
                    roomWidth = _corridorWidth;
                    roomHeight = Random.Range(_minCorridorLenght, _maxCorridorLenght);
                    break;
                case Directions.East:
                    roomWidth = Random.Range(_minCorridorLenght, _maxCorridorLenght);
                    roomHeight = _corridorWidth;
                    break;
                case Directions.West:
                    roomWidth = Random.Range(_minCorridorLenght, _maxCorridorLenght);
                    roomHeight = _corridorWidth;
                    break;
                    
            }
        }


        int xStartingPoint;
        int yStartingPoint;
        
        if (isFirst) {
            xStartingPoint = _mapWidth / 2;
            yStartingPoint = _mapHeight / 2;
        }
        else {
            int id;
            if (startWall.positions.Count == _corridorWidth) id = 1;
            else id = Random.Range(1, startWall.positions.Count - 2);

            xStartingPoint = startWall.positions[id].x;
            yStartingPoint = startWall.positions[id].y;
        }

        Vector2Int lastWallPosition = new Vector2Int(xStartingPoint, yStartingPoint);

        if (isFirst) {
            xStartingPoint -= Random.Range(1, roomWidth);
            yStartingPoint -= Random.Range(1, roomHeight);
        }
        else {
            switch (startWall.direction) {
                case Directions.South:
                    if (type == FeatureType.Room) xStartingPoint -= Random.Range(1, roomWidth - 2);
                    else xStartingPoint--;
                    yStartingPoint -= Random.Range(1, roomHeight - 2);
                    break;
                case Directions.North:
                    if (type == FeatureType.Room) xStartingPoint -= Random.Range(1, roomWidth - 2);
                    else xStartingPoint--;
                    yStartingPoint ++;
                    break;
                case Directions.West:
                    xStartingPoint -= roomWidth;
                    if (type == FeatureType.Room) yStartingPoint -= Random.Range(1, roomHeight - 2);
                    else yStartingPoint--;
                    break;
                case Directions.East:
                    xStartingPoint++;
                    if (type == FeatureType.Room) yStartingPoint -= Random.Range(1, roomHeight - 2);
                    else yStartingPoint--;
                    break;
            }
        }

        if (!CheckIfHasSpace(new Vector2Int(xStartingPoint, yStartingPoint), 
            new Vector2Int(xStartingPoint + roomWidth - 1, yStartingPoint + roomHeight - 1))) {
            return;
        }
        room.walls = new Wall[4];

        for (int i = 0; i < room.walls.Length; i++)
        {
            room.walls[i] = new Wall();
            room.walls[i].positions = new List<Vector2Int>();
            room.walls[i].lenght = 0;

            switch (i)
            {
                case 0:
                    room.walls[i].direction = Directions.South;
                    break;
                case 1:
                    room.walls[i].direction = Directions.North;
                    break;
                case 2:
                    room.walls[i].direction = Directions.West;
                    break;
                case 3:
                    room.walls[i].direction = Directions.East;
                    break;
            }
        }

        for (int y = 0; y < roomHeight; y++) {
            for (int x = 0; x < roomWidth; x++) {
                Vector2Int position = new Vector2Int();
                position.x = xStartingPoint + x;
                position.y = yStartingPoint + y;

                room.positions.Add(position);

                MapManager.map[position.x, position.y] = new Tile();
                MapManager.map[position.x, position.y].xPosition = position.x;
                MapManager.map[position.x, position.y].yPosition = position.y;

                if (y == 0) {
                    room.walls[0].positions.Add(position);
                    room.walls[0].lenght++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                if (y == (roomHeight - 1)) {
                    room.walls[1].positions.Add(position);
                    room.walls[1].lenght++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                if (x == 0) {
                    room.walls[2].positions.Add(position);
                    room.walls[2].lenght++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                if (x == (roomWidth - 1)) {
                    room.walls[3].positions.Add(position);
                    room.walls[3].lenght++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                if (MapManager.map[position.x, position.y].type != "Wall") {
                    MapManager.map[position.x, position.y].type = "Floor";
                }
            }
        }

        if (!isFirst)
        {
            MapManager.map[lastWallPosition.x, lastWallPosition.y].type = "Floor";
            switch (startWall.direction)
            {
                case Directions.South:
                    MapManager.map[lastWallPosition.x, lastWallPosition.y - 1].type = "Floor";
                    break;
                case Directions.North:
                    MapManager.map[lastWallPosition.x, lastWallPosition.y + 1].type = "Floor";
                    break;
                case Directions.West:
                    MapManager.map[lastWallPosition.x-1, lastWallPosition.y].type = "Floor";
                    break;
                case Directions.East:
                    MapManager.map[lastWallPosition.x+1, lastWallPosition.y].type = "Floor";
                    break;
            }
        }

        room.width = roomWidth;
        room.height = room.height;
        room.type = type;
        
        AllFeatures.Add(room);
        _countFeatures++;

    }

    bool CheckIfHasSpace(Vector2Int start, Vector2Int end) 
    {

        for (int y = start.y; y <= end.y; y++) {
            for (int x = start.x; x <= end.x; x++) {
                if (x < 0 || y < 0 || x >= _mapWidth || y >= _mapHeight) return false;
                
                
                if (MapManager.map[x,y] != null) return false;
            }
        }

        return true;
    }

    private Wall ChoseWall(Feature feature)
    {
        for (int i = 0; i < 10; i++)
        {
            int id = Random.Range(0, 100) / 25;
            if (!feature.walls[id].hasFeature)
            {
                return feature.walls[id];
            }
        }

        return null;
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
                        switch (MapManager.map[x,y].type)
                        {
                            case"Wall":
                                asciiMap += "#";
                                break;
                            case "Floor":
                                asciiMap += ".";
                                break;
                                
                        }
                    }
                    else
                    {
                        asciiMap += " ";
                    }

                    if (x == (_mapWidth - 1))
                    {
                        asciiMap += "\n";
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
                        switch (MapManager.map[x,y].type)
                        {
                            case"Wall":
                                var wall=Instantiate(_wallGameObject,_root.transform);
                                wall.transform.position = new Vector2((x-30f)*2, (y-30f)*2);
                                break;
                            case "Floor":
                                var floor=Instantiate(_floors[Random.Range(0,_floors.Count-1)],_root.transform);
                                floor.transform.position = new Vector2((x-30f)*2, (y-30f)*2);
                                break;
                                
                        }
                    }
                    else
                    {
                        
                    }

                    if (x == (_mapWidth - 1))
                    {
                        
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
