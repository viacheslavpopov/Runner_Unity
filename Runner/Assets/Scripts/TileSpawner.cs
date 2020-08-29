using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] tilePrefabs;
    [SerializeField] GameObject mainFloorTile;
    [SerializeField] int tilesOnScreen = 7;

    private Transform playerTransform;
    private float zSpawn;
    private float tileLength;
    private List<GameObject> activeTiles;
    private int lastPrefabIndex;
    void Start()
    {
        const int emptyTilesToBeSpawnedFirst = 5;

        activeTiles = new List<GameObject>();
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
        if (playerTransform.position.z - 2 * tileLength > (zSpawn - tileLength * tilesOnScreen))
        {
            SpawnTile();
            RemoveTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)

    {
        //Debug.Log("Spawned tile " + Time.time);
        GameObject gameObject;
        if (prefabIndex == -1)
        {
            gameObject = Instantiate(tilePrefabs[PickRandomTileIndex()] as GameObject);
        }
        else
        {
            gameObject = Instantiate(tilePrefabs[prefabIndex] as GameObject);
        }
        gameObject.transform.SetParent(transform);
        gameObject.transform.position = Vector3.forward * zSpawn;
        zSpawn += tileLength;

        activeTiles.Add(gameObject);
    }
    private void RemoveTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int PickRandomTileIndex()
    {
        if(tilePrefabs.Length < 2)
        {
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
 