using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject AlienPrefab; //外星怪物預置物件
    public GameObject StarFighterPrefab; //戰機預置物件
    public GameObject StartPoint; //戰機產生的起點

    GameObject StarFighter;
    GameManager Manager;

    ObjectStatus _StarFighterStatus;
    public ObjectStatus StarFighterStatus
    {
        get { return _StarFighterStatus; }
    }
    public enum ObjectStatus
    {
        Alive, //物件存活中
        Destroyed, //物件已消滅
        Initialing //物件初始化中
    }

    public float span = 1.0f;
    public float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStarFighter(); 
        Manager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.Status != GameManager.GameStatus.GameOver)
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.span && Manager.Status == GameManager.GameStatus.Gaming) //如果遊戲正常執行中，產生怪物
            {
                this.delta = 0;
                GameObject Alien = Instantiate(AlienPrefab) as GameObject;
                float px = Random.Range(-2, 2) + 0.1f * Random.Range(0, 10);
                Alien.transform.position = new Vector3(px, 7, 0);
            }

            if (StarFighter == null && _StarFighterStatus == ObjectStatus.Alive) //戰機被消滅時
            {
                _StarFighterStatus = ObjectStatus.Destroyed;
            }

            //戰機被消滅後，也確認目前遊戲正在重啟中的狀態，開始重新產生戰機
            if (_StarFighterStatus == ObjectStatus.Destroyed && Manager.Status == GameManager.GameStatus.Restarting)
            {
                _StarFighterStatus = ObjectStatus.Initialing;
                StartCoroutine(InitialStarFighter());
            }
        }
    }

    public void GenerateStarFighter()
    {
        StarFighter = Instantiate(StarFighterPrefab, StartPoint.transform.position, StartPoint.transform.rotation);
        _StarFighterStatus = ObjectStatus.Alive;
    }

    IEnumerator InitialStarFighter()
    {
        if (StarFighterPrefab == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(5); 
        GenerateStarFighter(); 
    }
}
