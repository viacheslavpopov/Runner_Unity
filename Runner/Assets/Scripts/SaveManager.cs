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
        //ResetStats();

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
    public void AttemptToSetHighscore(int newHighscore)
    {
        if (newHighscore > state.highscore)
        {
            state.highscore = newHighscore;
            Save();
        }
         
    }
    public void ResetStats()
    {
        state.currentMoney = 0;
        state.highscore = 0;
        Save();
    }
    public void AddMoney(int ammount)
    {
        Debug.Log("Add money: " + ammount);
       int temp = state.currentMoney;
        temp += ammount;
        state.currentMoney = temp;
        Save();
    }
    public int GetCurrentMoney()
    {
        return state.currentMoney;
    }
    public int GetHighscore()
    {
        return state.highscore;
    }
    public int GetSkin()
    {
        return state.skinNumber;
    }
}
