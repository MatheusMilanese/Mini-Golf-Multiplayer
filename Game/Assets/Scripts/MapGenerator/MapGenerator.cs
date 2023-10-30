using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int sizeMap;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject holeGroundPrefab;
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private List<GameObject> wallsPrefab;

    List<Vector2Int> positionMap;
    Stack<GameObject> groundsMap = new Stack<GameObject>();
    Stack<GameObject> wallsMap = new Stack<GameObject>();

    Dictionary<Vector2Int, int> wallsConfigurationDictionary = new Dictionary<Vector2Int, int>();

    public void GenerateMap(){
        wallsConfigurationDictionary.Clear();
        positionMap = gridGenerator.GeneratePath(sizeMap);
        CleanMap();
        for(int i = 0; i < positionMap.Count-1; i++){
            CreateGround(i, false);
            SetGroundConections(i, i+1);
            if(i > 0) SetGroundConections(i, i-1);
        }
        CreateGround(positionMap.Count-1, true);
        SetGroundConections(positionMap.Count-1, positionMap.Count-2);
        
        for(int i = 0; i < positionMap.Count; i++){
            CreateWall(i);
        }
    }

    void CreateWall(int index){
        int configuration = wallsConfigurationDictionary[positionMap[index]];
        Debug.Log($"{index} {configuration}");
        Vector3 position = new Vector3(positionMap[index].x*10+5, 1.5f, positionMap[index].y*10-5);
        GameObject wall = Instantiate(wallsPrefab[configuration], position, Quaternion.identity);
        // wall.name = $"Wall {positionMap[index].x} {positionMap[index].y}";
        wallsMap.Push(wall);
    }

    void CreateGround(int index, bool isHole){
        GameObject ground;
        Vector3 position = new Vector3(positionMap[index].x*10, 0, positionMap[index].y*10);

        if(isHole){
            position.y = -1.766f;
            ground = Instantiate(holeGroundPrefab, position, Quaternion.identity);
        }
        else{
            ground = Instantiate(groundPrefab, position, Quaternion.identity);
        }
        ground.name = $"Ground {positionMap[index].x} {positionMap[index].y}";
        groundsMap.Push(ground);
    }

    void SetGroundConections(int indexA, int indexB){
        Vector2Int direction = positionMap[indexA] - positionMap[indexB];
        if(direction.x == 1) UpdateGroundConfiguration(positionMap[indexA], 8);
        else if(direction.x == -1) UpdateGroundConfiguration(positionMap[indexA], 2);
        else if(direction.y == 1) UpdateGroundConfiguration(positionMap[indexA], 1);
        else if(direction.y == -1) UpdateGroundConfiguration(positionMap[indexA], 4);
    }

    void UpdateGroundConfiguration(Vector2Int position, int value){
        if(wallsConfigurationDictionary.ContainsKey(position)){
            wallsConfigurationDictionary[position] += value;
        }
        else{
            wallsConfigurationDictionary.Add(position, value);
        }
    }

    void CleanMap(){
        while(groundsMap.Count > 0){
            GameObject gameObject = groundsMap.Pop();
            Destroy(gameObject);
        }
        while(wallsMap.Count > 0){
            GameObject gameObject = wallsMap.Pop();
            Destroy(gameObject);
        }
    }

    public Vector3 getStartPosition(){
        return new Vector3(gridGenerator.startPosition.x*10+5, 1.5f, gridGenerator.startPosition.y*10-5);
    }


}
