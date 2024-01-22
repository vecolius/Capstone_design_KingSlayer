using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MapArray
{
    public GameObject[] Map;
}

public class MapManager : MonoBehaviour
{
    public static MapManager instance = null;


    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public MapArray[] MapPrefabs; // 2중 배열을 쓰기위해 사용
    public List<GameObject> Prefablist = new List<GameObject>();
    public GameObject[] PrefabObject;
    private FadeEffect fadeEffect;
    public List<int> seed = new List<int>();
    public int[] seedarray;
    public bool check = false;

    public Coordinate[] coordinates =
    {
        new Coordinate(10,0),
        new Coordinate(10,30),
        new Coordinate(10,60),
        new Coordinate(10,90),
        new Coordinate(10,120),
        new Coordinate(10,150),
        new Coordinate(10,180),
        new Coordinate(10,210),
        new Coordinate(10,240),
        new Coordinate(10,270)
    };

    void Start()
    {
        fadeEffect = FadeEffect.instance.GetComponent<FadeEffect>();
        fadeEffect.Fadeset();
        
        if(DateManager.instance.nowPlayer.Clear_Check)//게임을 클리어시 처음부터 실행
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                seed.Add(Random.Range(0, MapPrefabs[i].Map.Length));
                GameObject mapPrefab = MapPrefabs[i].Map[seed[i]];
                Vector2 mapPosition = coordinates[i].GetPosition();
                SpawnMap(mapPrefab, mapPosition);
            }
            PrefabObject = Prefablist.ToArray();
        }

        else if (Slotdata.instance.savefiles[Slotdata.instance.numbers]&&DateManager.instance.nowPlayer.seed.Count!=0)// 슬롯데이터에 데이터가 있을때 실행
        {
            seed = DateManager.instance.nowPlayer.seed;

            for (int i = 0; i < coordinates.Length; i++)
            {
                GameObject mapPrefab = MapPrefabs[i].Map[seed[i]];
                Vector2 mapPosition = coordinates[i].GetPosition();
                SpawnMap(mapPrefab, mapPosition);
            }
            PrefabObject = Prefablist.ToArray();
        }
        else// 슬롯데이터에 데이터가 없을때 실행
        {
            for (int i = 0; i < coordinates.Length; i++)
            {
                seed.Add(Random.Range(0, MapPrefabs[i].Map.Length));
                GameObject mapPrefab = MapPrefabs[i].Map[seed[i]];
                Vector2 mapPosition = coordinates[i].GetPosition();
                SpawnMap(mapPrefab, mapPosition);
            }
            PrefabObject = Prefablist.ToArray();
        }
        DateManager.instance.nowPlayer.seed = seed;
        DateManager.instance.SaveData();
    }

    public void SpawnMap(GameObject mapPrefabs, Vector2 _position)
    {
        GameObject map = Instantiate(mapPrefabs);
        map.transform.position = _position;
        Prefablist.Add(map);
    }

    public struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2 GetPosition()
        {
            return new Vector2(x, y);
        }
    }
}

/*seed = DateManager.instance.nowPlayer.seed;*/