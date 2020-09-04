
using UnityEngine;
using UnityEditor;

public class CreatePlayerSkinObject
{
    [MenuItem("Assets/Create/Player Skin")]
    public static void Create()
    {
        PlayerSkin asset = ScriptableObject.CreateInstance<PlayerSkin>();
        AssetDatabase.CreateAsset(asset, "Assets/PlayerSkins/NewPlayerSkin.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
