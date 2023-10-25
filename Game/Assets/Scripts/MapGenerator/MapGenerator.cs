using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int sizeMap;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject holeGroundPrefab;
    [SerializeField] private GridGenerator gridGenerator;

    List<Vector2Int> positionMap;
    Queue<GameObject> map = new Queue<GameObject>();

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("entrou");
            GenerateMap();
        }
    }

    void GenerateMap(){
        Vector3 position;
        GameObject ground;
        positionMap = gridGenerator.GeneratePath(sizeMap);
        CleanMap();
        for(int i = 0; i < positionMap.Count-1; i++){
            position = new Vector3(positionMap[i].x*10, 0, positionMap[i].y*10);
            ground = Instantiate(groundPrefab, position, Quaternion.identity);
            ground.name = $"Ground {positionMap[i].x} {positionMap[i].y}";
            map.Enqueue(ground);
        }
        position = new Vector3(positionMap[positionMap.Count-1].x*10, -1.766f, positionMap[positionMap.Count-1].y*10);
        ground = Instantiate(holeGroundPrefab, position, Quaternion.identity);
        ground.name = "Hole";
        map.Enqueue(ground);
    }

    void CleanMap(){
        while(map.Count > 0){
            GameObject gameObject = map.Dequeue();
            Destroy(gameObject);
        }
    }
}
