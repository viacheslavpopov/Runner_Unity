using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefab;
    
    void Awake()
    {
        if (SaveManager.Instance != null)
        {
            GameObject gameObject = Instantiate(playerPrefab[SaveManager.Instance.GetSkin()] as GameObject);
            //gameObject.transform.position = new Vector3(0, 1f, 0);
        }
        else
        {
            GameObject gameObject = Instantiate(playerPrefab[0] as GameObject);
        }
    }

}
