using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_Ver01 : MonoBehaviour
{
    private float carSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.carSpeed = 0.2f;
        }

        transform.Translate(this.carSpeed, 0, 0);

        this.carSpeed *= 0.98f;
    }
}
