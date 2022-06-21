using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_GO : MonoBehaviour
{
    public float rotateSpeed = 0;
    public InputField textObject;
    public GameObject rotateObject;
    private bool isGo = false;

    private void Start()
    {
        textObject.text = "0.0";
    }

    void Update()
    {
        if (isGo)
        {
            rotateObject.transform.Rotate(0, 0, rotateSpeed);
            textObject.text = rotateSpeed.ToString();

            if (rotateSpeed < 0.5f)
            {
                isGo = false;
                rotateSpeed = 0;
                textObject.text = "0.0";
            }
            else
            {
                rotateSpeed *= 0.999f;
            }
        }
       
    }

    public void Go()
    {
        rotateSpeed = float.Parse(textObject.text);
        isGo = true;
    }
}
