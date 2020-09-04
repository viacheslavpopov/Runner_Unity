using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkin : ScriptableObject
{
    public string skinName = "Skin name here";
    public int cost = 50;
    public Material bodyMaterial;
    public Material headMaterial;

}
