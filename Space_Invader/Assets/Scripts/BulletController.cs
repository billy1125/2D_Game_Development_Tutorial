using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2); //設定2秒後子彈物件被刪除
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, 0.2f, 0); //子彈會不斷往上移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Alien" || collision.tag == "AlienBullet")
        //如果碰撞的標籤是Alien或AlienBullet
        {
            Instantiate(ExplosionPrefab, transform.position, transform.rotation); //在子彈碰撞的位置產生爆炸
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall") //如果碰撞的標籤是Wall
        {
            Destroy(gameObject);
        }
    }
}
