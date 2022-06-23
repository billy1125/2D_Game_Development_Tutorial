using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 1.0f; //移動速度
    public string DestoryTag;
    public string CheckPoints;
    public GameObject ExplosionPrefab; //爆炸預置物件
    public int Score = 10;
    public int Money = 1;
    public int MaxLife = 1;

    private GameObject[] checkPoints; //簽到點
    private GameManager gameManager; //遊戲導演程式
    private GameObject tempLocation;
    private int i = 0;
    private int life;

    // 要修改用Awake，因為如果這個程式會被別的物件參考或引用，一定要先在這邊做完所有的事情
    // 當這個EnemyController的類別被別的程式參考時，這裡的Awake函式會被立刻呼叫並且執行
    void Awake()
    {
        checkPoints = GameObject.Find(CheckPoints).GetComponent<CheckPoint>().CheckPoints;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        life = MaxLife;
    }

    // 敵人的標準移動方式，只要參考這個EnemyController的類別，都有會依照你設定給他的簽到點移動
    public void Move()
    {
        if (i == checkPoints.Length) //到達最後一個簽到點，就會刪除自己
        {
            Destroy(gameObject);
        }
        else
        {
            tempLocation = checkPoints[i]; //取得下一個目標簽到點
            //取得自己與目標點的位置座標
            Vector2 p1 = tempLocation.transform.position;
            Vector2 p2 = this.transform.position;
            // Vector3.MoveTowards可以讓自己逐漸靠近目標點
            transform.position = Vector2.MoveTowards(p2, p1, Speed * Time.deltaTime);
            //transform.Translate(0, Speed * Time.deltaTime, 0);

            float d = Vector2.Distance(p2, p1); //計算目前自己與目標點的距離有多少？

            if (d <= 0.3f) //距離低於1.0f，就會開始轉向
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, tempLocation.transform.rotation, Time.deltaTime * 2);
            }

            if (d <= 0.1f) //距離低於0.1f就會直接將自己的旋轉設定跟目標點一樣
            {
                transform.rotation = tempLocation.transform.rotation;
                i += 1; //i加1，代表自己要朝向下一個簽到點前進
            }
        }
    }

    // 發射武器的虛擬方法(virtual method)，所謂虛擬方法可以視為敵人的預設行為，你可以不寫東西，也可以像OnTriggerEnter2D有一個預設的動作
    // 如果你要設計一種新敵人，就可以使用預設的攻擊方法，也可以重新設計
    public virtual void FireWeapon()
    {
        //這裡沒有設計任何的攻擊方法
    }

    // 當碰撞發生時的虛擬方法，這裡就有事先設計，當然你可以「覆寫」它，重新設計另一種碰撞事件處理的方式
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == DestoryTag) //如果發生碰撞的物件標籤是Bullet，就產生爆炸，並且刪除自己
        {
            life -= 1;
            if (ExplosionPrefab != null && life == 0)
            {
                gameManager.UpdateScore(Score); //更新分數（加分）
                gameManager.UpdateMoney(Money); //更新金錢（加一塊錢）
                Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
                Destroy(collision.gameObject); //刪除砲彈
                Destroy(gameObject);
            }
        }
    }
}
