using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject[] playerPrefab;
    
    void Awake()
    {
        GameObject gameObject = Instantiate(playerPrefab[SaveManager.Instance.GetSkin()] as GameObject);
       
    }

}
