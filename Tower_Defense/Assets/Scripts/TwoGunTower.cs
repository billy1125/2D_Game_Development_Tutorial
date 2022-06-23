using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoGunTower : TowerController
{
    public GameObject[] FirePoints;

    public override void FireWeapon()
    {
        foreach (var item in FirePoints) //我們希望第二種坦克一次發射兩顆砲彈，所以我們透過覆寫重新設計
        {
            Instantiate(BulletPrefab, item.transform.position, item.transform.rotation);
        }
    }
}
