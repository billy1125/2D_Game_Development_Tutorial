using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject Player;
    GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("player");
        this.gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
            gameManager.GetComponent<GameManager>().IncreasePonit();
        }

        Vector2 p1 = transform.position;
        Vector2 p2 = this.Player.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;
        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            gameManager.GetComponent<GameManager>().decreaseHP();

            Destroy(gameObject);
        }
    }
}
