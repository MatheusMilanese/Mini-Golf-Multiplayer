using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour{
    
    private Vector2Int sizeMap;

    private int[,] map;
    private Vector2Int _startPosition;
    private Vector2Int holePosition;

    public Vector2Int startPosition { get => _startPosition; }

    private List<Vector2Int> path;
    private Dictionary<Vector2Int, Vector2Int> pais;

    private int[,] mov = new int[4,2]{{1,0}, {-1, 0}, {0, 1}, {0, -1}};

    private void RandomFillMap(){
        map = new int[sizeMap.x, sizeMap.y];
        for(int x = 0; x < sizeMap.x; x++){
            for(int y = 0; y < sizeMap.y; y++){
                map[x,y] = Random.Range(1, 1000);
            }
        }
    }

    private void setStartPosition(){
        _startPosition = new Vector2Int(Random.Range(0, sizeMap.x-1), Random.Range(0, sizeMap.y-1));
    }

    private void setHolePosition(){
        holePosition = new Vector2Int(Random.Range(0, sizeMap.x-1), Random.Range(0, sizeMap.y-1));
        while(Vector2Int.Distance(_startPosition, holePosition) < 2){
            holePosition = new Vector2Int(Random.Range(0, sizeMap.x-1), Random.Range(0, sizeMap.y-1));
        }
    }

    private void setPai(Vector2Int no, Vector2Int pai){
        if(pais.ContainsKey(no)){
            pais[no] = pai;
        }
        else{
            pais.Add(no, pai);
        }
    }

    public List<Vector2Int> GeneratePath(Vector2Int _sizeMap){
        sizeMap = _sizeMap;
        path = new();
        pais = new();
        int[,] custo = new int[sizeMap.x, sizeMap.y];

        RandomFillMap();
        setHolePosition();
        setStartPosition();

        for(int x = 0; x < sizeMap.x; x++){
            for(int y = 0; y < sizeMap.y; y++){
                custo[x,y] = 99999999;
            }
        }
        custo[_startPosition.x, _startPosition.y] = 0;
        setPai(_startPosition, _startPosition);
        
        Queue<Vector2Int> q = new();

        q.Enqueue(_startPosition);
        while(q.Count > 0){
            Vector2Int u = q.Dequeue();
            for(int i = 0; i < 4; i++){
                Vector2Int w = new Vector2Int(u.x+mov[i,0], u.y+mov[i,1]);
                if(w.x < 0 || w.x >= sizeMap.x || w.y < 0 || w.y >= sizeMap.y) continue;
                if(custo[u.x, u.y] + map[w.x, w.y] < custo[w.x, w.y]){
                    custo[w.x, w.y] = custo[u.x, u.y] + map[w.x, w.y];
                    q.Enqueue(w);
                    setPai(w, u);
                }
            }
        }
        Vector2Int currentVertex = holePosition;
        while(pais[currentVertex] != currentVertex){
            path.Add(currentVertex);
            currentVertex = pais[currentVertex];
        }
        path.Add(currentVertex);
        path.Reverse();
        return path;
    }
}
