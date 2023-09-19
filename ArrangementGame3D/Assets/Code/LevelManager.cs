using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Button> levelPassButtons;

    private GameData gameData;
    private GameManager _gameManager;

    private void Start()
    {
        //levelPassButtons[1].interactable = true;
        SetupGame();
        _gameManager.SaveGameData();
        _gameManager.LoadGameData();
        
    }
    
    public void NextLevelPass()
    {
        //gameData.levelGame++;
        SceneManager.LoadScene("1");
    }

    private void SetupGame()
    {
        for (int i = 1; i < levelPassButtons.Count; i++)
        {
            levelPassButtons[i].interactable = false;
        }

        for (int i = 0; i < levelPassButtons.Count; i++)
        {
            if (i < gameData.levelGame)
            {
                levelPassButtons[i].interactable = true;
            }
        }
    }
    

}
