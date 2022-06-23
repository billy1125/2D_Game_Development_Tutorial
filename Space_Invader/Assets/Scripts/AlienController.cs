using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienController : MonoBehaviour
{
    public float AlienMoveSpeed = 0.1f; //外星怪物的速度設定值

    public GameObject BulletPrefab; //怪物子彈預置物件
    GameObject GameManager; //遊戲導演程式

    public float span = 1.0f;
    public float delta = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10); //設定10秒後外星怪物物件被刪除
        GameManager = GameObject.Find("GameManager"); //找到場景中的遊戲導演程式
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            Vector3 pos = gameObject.transform.position + new Vector3(0, -1.0f, 0);
            //子彈生成的位置根據怪物的位置，再往下減1.0f
            Instantiate(BulletPrefab, pos, gameObject.transform.rotation); //依據上述的pos位置，生成子彈
        }
    }

    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, -1 * AlienMoveSpeed, 0); //外星怪物會不斷往下移動
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet") //如果碰撞的標籤是Bullet
        {
            GameManager.GetComponent<GameManager>().IncreaseScore(10); //如果打到外星人，就加10分
            Destroy(gameObject); //刪除外星怪物物件
        }
    }
}
