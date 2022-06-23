using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public string TargetTagName;         //目標物標籤名稱
    public float DetectMinDistance = 10; //最短的偵測距離
    public float Speed = 5.0f;

    private GameObject Missiletarget; //飛彈的目標物件
    private bool launchMissile = false; //是否發射飛彈

    void FixedUpdate()
    {
        if (launchMissile) // 只有launchMissile是true的狀態，飛彈才會前進，也就是發射狀態
        {
            transform.Translate(0, Time.deltaTime * Speed, 0);
        }
        
        if (Missiletarget != null) //如果飛彈的目標被設定了，就會追蹤目標
        {
            //計算飛彈和目標的距離
            float distanceToEnemy = Vector3.Distance(this.transform.position, Missiletarget.transform.position);
            
            if (distanceToEnemy >= 3) // 距離大於等於3，追蹤目標
            {
                // 讓飛彈追蹤目標其實作法跟砲塔對著目標是一樣的方法
                Vector3 dir = Missiletarget.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.back);
                Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * 1).eulerAngles;
                this.transform.rotation = Quaternion.Euler(0, 0, rotation.z);
            }
            else if (distanceToEnemy < 3) // 距離小於3，不追蹤目標，但是速度加快
            {
                Speed = 15.0f;
            }
        }
    }

    // 設定飛彈發射的方法
    public void LaunchMissile(GameObject _target)
    {
        Missiletarget = _target; //設定飛彈目標
        launchMissile = true; //設定飛彈發射狀態
        this.transform.SetParent(null); //將飛彈的父物件取消，避免轉向還跟著砲塔轉
        Destroy(gameObject, 5); //設定飛彈消滅的時間
    }
}
