using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    float rotSpeed = 0;  // 旋轉速度

    void Start()
    {

    }

    void Update()
    {
        // 若點擊滑鼠就要設定旋轉速度
        if (Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 10;
        }

        // 旋轉速度、讓輪盤旋轉
        transform.Rotate(0, 0, this.rotSpeed);

        // (增加)讓輪盤減速
        this.rotSpeed *= 0.999f;
    }
}