using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public Text HighestScore;

    void Start()
    {      
        GameSaveData gameSaveData = new GameSaveData();

        string[] data = gameSaveData.ReadData();

        HighestScore.text = "Highest Score: " + data[0] + "\r\nName: " + data[1];
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
