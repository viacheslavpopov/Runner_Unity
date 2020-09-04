using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSkinContainer: MonoBehaviour
{
    public PlayerSkin playerSkin;


    private const string SkinNameText = "Skin name: ";
    private const string CostText = "Cost: ";
    private void Awake()
    {
        TextMeshProUGUI[] atributes = GetComponentsInChildren<TextMeshProUGUI>();
        atributes[0].text = SkinNameText + playerSkin.skinName;
        atributes[1].text = CostText + playerSkin.cost;

    }
}
