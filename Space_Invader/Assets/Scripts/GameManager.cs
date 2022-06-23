using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
//using System.IO;

public class GameManager : Singleton<GameManager>
{
    ObjectGenerator Generator;
    GameSaveData gameSaveData = new GameSaveData();

    GameStatus _Status;
    public GameStatus Status {
		get { return _Status; }
	}
    public enum GameStatus {
		Gaming, //遊戲中
        GameOver, //遊戲結束
		Restarting //重啟遊戲中
	}

    int intScore = 0;
    int LifeAmount = 2;
    string HighestScore;

    public Text ScoreText;
    public Text BestScoreText;
    public InputField NameInputField;
    public GameObject[] LifeImage; 
    public GameObject GameUI;
    public GameObject Input;
    public Image LifeBar;

    // Start is called before the first frame update
    void Start()
    {        
        LifeBar.fillAmount = 1;
        _Status = GameStatus.Gaming;
        Generator = GetComponent<ObjectGenerator>();
        ScoreText.text = "Score: " + intScore;
        HighestScore = gameSaveData.ReadData()[0];
    }

    // Update is called once per frame
    void Update()
    {
        // 如果確認戰機已經被摧毀
        if (Generator.StarFighterStatus == ObjectGenerator.ObjectStatus.Destroyed && 
            Status == GameStatus.Gaming &&
            LifeAmount >= 0) 
        {
            _Status = GameStatus.Restarting; //設定為重啟遊戲
            LifeImage[LifeAmount].SetActive(false); 
            LifeAmount -= 1; 
        }

        // 如果確認戰機已經重新生成
        if (Generator.StarFighterStatus == ObjectGenerator.ObjectStatus.Alive && Status == GameStatus.Restarting){
            _Status = GameStatus.Gaming;
            LifeBar.fillAmount = 1;
        }

        if (LifeAmount == -1)
        {
            SaveGameData();
            _Status = GameStatus.GameOver;
            GameUI.SetActive(true);
        }
    }

    public void IncreaseScore(int _score)
    {
        intScore += _score;
        ScoreText.text = "Score: " + intScore;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void UpdateLifeBar(float _amount)
    {
        LifeBar.fillAmount = _amount;
    }

    void SaveGameData()
    {
        int BestScore = Convert.ToInt32(HighestScore);

        if (intScore > BestScore)
        {
            Input.SetActive(true);
            BestScoreText.text = "Score: " + intScore.ToString();

                        
            gameSaveData.SaveData(intScore, NameInputField.text);
        }
    }
}
