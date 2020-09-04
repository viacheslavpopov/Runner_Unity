using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        //to do string tags - someday ..
        //public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    private void Awake()
    {

        for (int index = 0; index < pools.Count; index++)
        {

            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pools[index].size; i++)
            {
                GameObject gameObject = Instantiate(pools[index].prefab);
                gameObject.SetActive(false);
                gameObject.transform.SetParent(transform);
                objectPool.Enqueue(gameObject);
            }
            poolDictionary.Add(index, objectPool);

        }
        Debug.Log("Number of different pools: " + poolDictionary.Count);
    }

    public GameObject SpawnFromPool(int tag, Vector3 position, Quaternion rotation)

    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");

        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(transform);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public void ReturnToPool(int poolToReturnTo = 0)
    {
        Debug.Log("Pool to return to: " + poolToReturnTo);
        GameObject objectToDespawn = poolDictionary[poolToReturnTo].Dequeue();
        objectToDespawn.SetActive(false);
        poolDictionary[poolToReturnTo].Enqueue(objectToDespawn);

    }
    public int DistinctPrefabCount()
    {
        return pools.Count;
    }
}
