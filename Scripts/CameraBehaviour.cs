using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Camera _camera;
    
    void Start()
    {
        _camera = GetComponent<Camera>();
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        _camera.transform.position = new Vector3(_player.transform.position.x,_player.transform.position.y,-10);
    }
}
