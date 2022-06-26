using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DungeonGenerator _dungeonGenerator;

    private void Start()
    {
        _dungeonGenerator = GetComponent<DungeonGenerator>();
        _dungeonGenerator.Init();
        _dungeonGenerator.GenerateDungeon();
    }
    
}
