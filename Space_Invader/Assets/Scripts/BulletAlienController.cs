using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAlienController : MonoBehaviour
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
        gameObject.transform.position += new Vector3(0, -0.1f, 0); //子彈會不斷往下移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Bullet")
        //如果碰撞的標籤是Player或Bullet
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
