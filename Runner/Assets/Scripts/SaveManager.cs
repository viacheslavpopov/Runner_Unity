using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }
    public SaveState state;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();

    }

    public void Save()
    {
        
        PlayerPrefs.SetString("save", HelperScript.Serialize<SaveState>(state));
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = HelperScript.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
        }
    }
    public void SelectSkin(int index)
    {
        state.skinNumber = index;
        Save();
    }
    public int GetSkin()
    {
        return state.skinNumber;
    }
}
