using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank01 : EnemyController // 坦克01的控制程式從EnemyController「繼承」而來
{
    public string TargetTagName; //目標物標籤名稱
    public float DetectMinDistance = 10; //最短的偵測距離
    public GameObject FirePoint;
    public GameObject BulletPrefab;
    public GameObject TankGun; //坦克砲塔

    private Transform target;
    private float Span = 1.0f;
    private float Delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //另一種定時函數，你可以設定多少時間後，每一段時間重複做一件事
    }

    public void FixedUpdate()
    {
        Move();
        FireWeapon();
    }

    void UpdateTarget() //追蹤最近的目標物
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
        if (nearestEnemy != null && shortestDistance <= DetectMinDistance)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // 這裡重新「覆寫（override）」EnemyController的FireWeapon（發射武器）的方法
    // 所謂覆寫，意思就是重寫設計一個方法裡面的程式
    public override void FireWeapon()
    {
        if (target != null) //讓砲塔對準目標物
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.back);
            Vector3 rotation = Quaternion.Lerp(TankGun.transform.rotation, lookRotation, Time.deltaTime * 5).eulerAngles;
            TankGun.transform.rotation = Quaternion.Euler(0, 0, rotation.z);

            this.Delta += Time.deltaTime; //依照過去產生物件的方式，射擊砲彈
            if (this.Delta > this.Span)
            {
                Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
                this.Delta = 0;
            }
        }
    }

    // 假設你要重新設計碰撞事件處理？該怎麼做？
    //public override void OnTriggerEnter2D(Collider2D collision)    
}
