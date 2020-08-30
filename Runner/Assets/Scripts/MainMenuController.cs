using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] Transform shopPanel;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject shopCanvas;

    private void Start()
    {
        shopCanvas.SetActive(false);
        InitializeShop();
    }
    public void ToShop()
    {
        menuCanvas.SetActive(false);
        shopCanvas.SetActive(true);
       // SceneManager.LoadScene("Shop");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void HideShop()
    {
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(true);

    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    void InitializeShop()
    {
        if(shopPanel == null)
        {
            Debug.Log("Not Initialized shop");
        }

        int index = 0;
        foreach(Transform transform in shopPanel)
        {
            int currentIndex = index;
            Button button = transform.GetComponent<Button>();
            button.onClick.AddListener(() => OnContainerSelect(currentIndex));
            index++;
        }
    }

    private void OnContainerSelect(int index)
    {
        Debug.Log("Current index of the container: " + index);
        SaveManager.Instance.SelectSkin(index);
    }
}
