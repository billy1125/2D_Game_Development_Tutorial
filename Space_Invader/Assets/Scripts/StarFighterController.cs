using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFighterController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject ExplosionPrefab;
    public float FireRate = 0.2f;

    GameObject GameManager;

    float LifeAmount = 1;
    public bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        StartCoroutine(FireWeapons());
    }

    // Update is called once per frame
    void Update()
    {
        //增加子彈發射的功能
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }

        isFiring = InputManager.instance.steering.IsFiring;
    }

    void FixedUpdate()
    {
        var steeringInput = InputManager.instance.steering.delta;

        gameObject.transform.position += new Vector3(0.1f * steeringInput.x, 0, 0);

        //簡單的左右控制，這個範例與過去的貓咪移動都是類似的
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(ExplosionPrefab, transform.position, transform.rotation); //在碰撞的位置產生爆炸

        if (collision.tag == "Alien" || collision.tag == "AlienBullet") //如果碰撞的標籤是怪物或怪物子彈
        {
            LifeAmount -= 0.1f;
            GameManager.GetComponent<GameManager>().UpdateLifeBar(LifeAmount);
            if (LifeAmount <= 0)
            {
                Destroy(gameObject); //刪除戰機
            }            
        }
    }

    private void FireBullet()
    {
        Vector3 pos = gameObject.transform.position + new Vector3(0, 0.6f, 0); //子彈生成的位置根據戰機的位置，再往上加0.6f
        Instantiate(BulletPrefab, pos, gameObject.transform.rotation); //依據上述的pos位置，生成子彈
    }

    IEnumerator FireWeapons()
    {
        while (true)
        {            
            yield return new WaitForSeconds(FireRate);
            if (isFiring)
            {
                if (this.BulletPrefab != null)
                {
                    FireBullet();
                }
            }
        }
    }
}
