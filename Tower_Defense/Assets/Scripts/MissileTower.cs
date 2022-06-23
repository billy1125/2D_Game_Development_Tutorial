using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : TowerController
{
    public GameObject MissileOnPlatform; // 用來存放目前架上的飛彈

    private void Start()
    {
        InvokeRepeating("Recharge", 0.0f, 5.0f); //每五秒安裝飛彈一次
    }

    public override void FireWeapon()
    {
        if (target != null && MissileOnPlatform != null) // 如果砲塔有追蹤到目標，就會一直把目標設定到飛彈，然後飛彈就會發射
        {            
            MissileOnPlatform.gameObject.GetComponent<MissileController>().LaunchMissile(target);
            MissileOnPlatform = null;
        }
    }

    // 安裝飛彈的方法
    void Recharge()
    {
        if (MissileOnPlatform == null) // 如果架上沒有才會安裝
        {
            MissileOnPlatform = Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            MissileOnPlatform.transform.SetParent(this.gameObject.transform); //將飛彈的父物件設定是砲塔，這樣飛彈才會跟著砲塔轉
        }
    }
}
