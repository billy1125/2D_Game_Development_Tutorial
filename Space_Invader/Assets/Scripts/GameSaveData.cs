using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class GameSaveData
{
    [SerializeField]
    public string HighestScore;
    public string Name;

    public string[] ReadData()
    {
        string[] data = new string[2];

        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("save.json"));
        string stringJson = fileReader.ReadToEnd();
        fileReader.Close();

        GameSaveData gameSaveData = JsonUtility.FromJson<GameSaveData>(stringJson);
        //HighestScore.text = "Highest Score: " + gameSaveData.HighestScore;

        data[0] = gameSaveData.HighestScore;
        data[1] = gameSaveData.Name;

        return data;
    }

    public void SaveData(int _score, string _name)
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.HighestScore = _score.ToString();
        gameSaveData.Name = _name;

        string saveString = JsonUtility.ToJson(gameSaveData);

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("save.json"));

        try
        {
            file.Write(saveString);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            throw;
        }
        finally
        {
            file.Close();
        }
    }
}