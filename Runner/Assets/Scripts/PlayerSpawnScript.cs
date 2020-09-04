using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    public Player player;
   public PlayerSkin[] skins;
    
    //need to rethink this, no need for 2 lists - 1 here and one in shop
    void Awake()
    {
        if (SaveManager.Instance != null) {
            int skinIndex = SaveManager.Instance.GetSkin();
            
                player.playerSkin = skins[skinIndex];
            Debug.Log("Player skin should change in : " + player.playerSkin);
        }
    }

}
