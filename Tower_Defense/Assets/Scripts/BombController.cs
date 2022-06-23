using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    bool enabledBomb = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        StartCoroutine(ColliderEnable());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enabledBomb == true)
        {
            // 做線性插值，當炸彈爆炸時，碰撞體的半徑會逐漸擴大
            this.GetComponent<CircleCollider2D>().radius = Mathf.Lerp(1, 3, 0.9f);
        }
    }

    IEnumerator ColliderEnable()
    {
        yield return new WaitForSeconds(2);
        //兩秒後，播放爆炸音效，並且讓碰撞體開啟，讓炸彈碰撞體能對砲塔產生效果
        enabledBomb = true;
        this.GetComponent<AudioSource>().Play();
    }
}
