using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowController : MonoBehaviour
{
    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }

        //Vector2 p1 = transform.position;
        //Vector2 p2 = this.Player.transform.position;

        //Vector2 dir = p1 - p2;

        //float d = dir.magnitude;
        //float r1 = 0.5f;
        //float r2 = 1.0f;

        //if (d < r1 + r2)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene("ClearScene");
        }
        
    }

}
