using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private List<Transform> players;

    private void Start() {
        mapGenerator.GenerateMap();
        SetPositionPlayers();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            mapGenerator.GenerateMap();
            SetPositionPlayers();
        }
    }

    void SetPositionPlayers(){
        for(int i = 0; i < players.Count; i++){
            players[i].position = mapGenerator.getStartPosition();
            players[i].gameObject.GetComponent<BallController>().StopBall();
        }
    }
}
