using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button[] TowerButtons; // 設定按鍵

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach (Button item in TowerButtons) //遊戲一開始，先將所有的按鍵停止作用
        {
            item.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Money >= 10) // 如果錢還有10塊錢以上，就可以新增一個單管砲塔
        {
            TowerButtons[0].interactable = true;
        }
        else
        {
            TowerButtons[0].interactable = false;
        }

        if (gameManager.Money >= 50) // 如果錢還有50塊錢以上，就可以新增一個單管砲塔
        {
            TowerButtons[1].interactable = true;
        }
        else
        {
            TowerButtons[1].interactable = false;
        }

        if (gameManager.Money >= 30) // 如果錢還有30塊錢以上，就可以新增一個飛彈砲塔
        {
            TowerButtons[2].interactable = true;
        }
        else
        {
            TowerButtons[2].interactable = false;
        }
    }

    public void SelectOneGun() // 設定目前選擇是單管砲塔
    {
        TowerButtons[0].Select();
    }

    public void SelectTwoGun() // 設定目前選擇是單管砲塔
    {
        TowerButtons[1].Select();
    }

    public void SelectMissile() // 設定目前選擇是飛彈砲塔
    {
        TowerButtons[2].Select();
    }
}
