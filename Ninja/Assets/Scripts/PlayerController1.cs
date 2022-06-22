using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        this.MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-3, 0, 0);
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(3, 0, 0);
            GetComponent<AudioSource>().Play();
        }        

        if (transform.position.x > 9)
        {
            transform.Translate(-3, 0, 0);
        }

        if (transform.position.x < -9)
        {
            transform.Translate(3, 0, 0);
        }
    }

    public void LButtonDown()
    {
        transform.Translate(-3, 0, 0);
        GetComponent<AudioSource>().Play();
    }

    public void RButtonDown()
    {
        transform.Translate(3, 0, 0);
        GetComponent<AudioSource>().Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MainCamera.GetComponent<CameraShake>().isShake = true;
    }
}
