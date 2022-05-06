using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuControlls : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject nameSelectMenu;
    public GameObject[] nameFields;
    public Toggle hints;

    public TextMeshProUGUI[] inputNames;


    public void GoToMainMenu()
    {
        mainMenu.SetActive(true);
        nameSelectMenu.SetActive(false);
    }

    public void GoToNameSelect(int n)
    {
        GameManager.numberPlayers = n;
        GameManager.Names = new string[n + 1];

        mainMenu.SetActive(false);
        nameSelectMenu.SetActive(true);

        foreach(GameObject go in nameFields)
            go.SetActive(false);

        for(int i = 0; i < n; i++)
            nameFields[i].SetActive(true);
    }

    public void StartGame()
    {
        for(int i = 0; i < GameManager.numberPlayers; i++)
        {
            GameManager.Names[i] = inputNames[i].text;
        }
        GameManager.withHints = hints.isOn;
        LevelLoader.LoadLevel(3);
    }

    public void SetName(int i, string name)
    {
        GameManager.Names[i] = name;
    }
}
