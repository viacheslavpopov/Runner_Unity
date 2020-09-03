using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    [SerializeField] GameObject mainFloorTile;
    [SerializeField] int tilesOnScreen = 7;

    private Transform playerTransform;
    private float zSpawn;
    private float tileLength;

    private int lastPrefabIndex;
    private Queue<GameObject> tileQueue;

    private ObjectPool pool;
    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }
    void Start()
    {
        
        tileQueue = new Queue<GameObject>();
        const int emptyTilesToBeSpawnedFirst = 5;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        tileLength = mainFloorTile.GetComponent<MeshRenderer>().bounds.size.x;
       
        Debug.Log("tile length " + tileLength);

        zSpawn = - tileLength;

        for (int i = 0; i< tilesOnScreen; i++)
        {
            if (i < emptyTilesToBeSpawnedFirst)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //2 * tileLength -- safe zone
        if (playerTransform.position.z - tileLength > (zSpawn - tileLength * tilesOnScreen))
        {
            SpawnTile();
            GameObject tileToDespawn = tileQueue.Dequeue();
            tileToDespawn.SetActive(false);
        }
    }

    private void SpawnTile(int index = -1)
    {

        GameObject item;
        int tag = PickRandomTileIndex();
        if (index == 0)
        {
            item = pool.SpawnFromPool(0, Vector3.forward * zSpawn, Quaternion.identity);
        }
        else
        {
            item = pool.SpawnFromPool(tag, Vector3.forward * zSpawn, Quaternion.identity);
        }
        tileQueue.Enqueue(item);

        zSpawn += tileLength;

    }

    private int PickRandomTileIndex()
    {
        int range = pool.pools.Count;
        if(range < 2)
        {
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, range);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
 