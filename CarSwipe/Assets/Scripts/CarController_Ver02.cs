using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_Ver02 : MonoBehaviour
{
    public float carSpeed;
    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.startPosition = Input.mousePosition;
            //this.carSpeed = 0.2f;
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 endPoition = Input.mousePosition;
                float swipeLength = endPoition.x - startPosition.x;

                this.carSpeed = swipeLength / 500.0f;

                GetComponent<AudioSource>().Play();
            }
        }

        transform.Translate(this.carSpeed, 0, 0);

        this.carSpeed *= 0.98f;
    }
}
