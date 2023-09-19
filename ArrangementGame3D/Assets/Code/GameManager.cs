using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
   
    public SpawnManager _spawnManager;
    private GameData gameData;
    
    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private float gameTimer;
    private bool isGameOver;

    


    void Start()
    {
        
        //LoadGameData();
        isGameOver = _spawnManager.isGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameTimer -= Time.deltaTime;
            textTimer.text = Mathf.Round(gameTimer).ToString(CultureInfo.InvariantCulture);
            
            if (isGameOver == false || gameTimer <= 0)
            {
               Debug.Log("GAME OVER"); 
               GameOver();
            }
            
        }
       
    }

    private void GameOver()
    {
        //
    }
    
    private void LevelUp()
    {
        
    }

    

    public void SaveGameData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + "/data1.dat");
        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }

    public void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/data1.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/data1.dat", FileMode.Open);
            gameData = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }
        else
        {
            // Yeni bir oyun verisi oluştur
            gameData = new GameData();
            gameData.levelGame = 1; // İlk seviyeyi ayarla
        }
    }

   
    
    
}

[System.Serializable]
public class GameData
{
    public float timerGame;
    public int goldGame;
    public int levelGame = 1;

}

