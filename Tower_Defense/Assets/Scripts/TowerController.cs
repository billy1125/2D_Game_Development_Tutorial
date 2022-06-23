using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    public string TargetTagName;         //目標物標籤名稱
    public float DetectMinDistance = 10; //最短的偵測距離
    public GameObject FirePoint;         //射擊點設定
    public GameObject BulletPrefab;      //砲彈預置物件
    public GameObject ExplosionPrefab;   //爆炸預置物件
    public float MaxLife = 10.0f;        //生命最大值
    public Image LifeBar;                //血條物件

    private float Span = 1.0f;
    private float Delta = 0;
    private float life;
    protected GameObject target; 
    GameManager gameManager; //遊戲導演程式
    SetTower setTower;

    // 基本的啟始設定要寫在Awake裡面
    void Awake()
    {
        life = MaxLife;
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //另一種定時函數，你可以設定多少時間後，每一段時間重複做一件事
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        setTower = GameObject.Find("Towers").GetComponent<SetTower>();
    }

    void Update()
    {      
        if (life == 0)   // 若血為0，砲塔就毀滅
        {
            Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
            setTower.TowerIsDestroy(this.transform.position);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (target != null && gameManager.Life > 0) //讓砲塔對準目標物
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.back);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
            this.transform.rotation = Quaternion.Euler(0, 0, rotation.z);

            this.Delta += Time.deltaTime;
            if (this.Delta > this.Span)
            {
                FireWeapon(); // 射擊武器
                this.Delta = 0;
            }
        }
    }

    //追蹤最近的目標物
    public void UpdateTarget() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TargetTagName); //將場景中某一個特定標籤的物件全部找進來
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) //計算每一個物件與砲塔之間的距離，把最近的物件找出來
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        // 如果最近的物件距離是低於最短的偵測距離，才當成是目標物
        if (nearestEnemy != null && shortestDistance <= DetectMinDistance && gameManager.Life > 0)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    // 射擊武器的虛擬方法，你可以使用預設方法，或者重新寫一個...
    public virtual void FireWeapon()
    {
        Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到的物件標籤是BulletEnemy，才會扣血，並且為了避免重複中彈，這個砲彈會改為"Untagged"標籤
        if (collision.tag == "BulletEnemy" || collision.tag == "Missile")
        {
            collision.tag = "Untagged";
            life -= 1.0f;
            LifeBar.fillAmount = life / MaxLife;
        }
    }
}
