using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject carObject;
    private GameObject flagObject;
    private GameObject distantText;
    private GameObject PointsText;

    // Start is called before the first frame update
    void Start()
    {
        this.carObject = GameObject.Find("car");
        this.flagObject = GameObject.Find("flag");
        this.distantText = GameObject.Find("Distance");
        this.PointsText = GameObject.Find("Points");
    }

    // Update is called once per frame
    void Update()
    {
        float length = this.flagObject.transform.position.x - this.carObject.transform.position.x;
        this.distantText.GetComponent<Text>().text = "距離目標還有：" + length.ToString("F2") + "m";
        if (length > 0) { 
            length = 100 / length;
            this.PointsText.GetComponent<Text>().text = "分數：" + length.ToString("F2");
        }
        else
        {
            this.PointsText.GetComponent<Text>().text = "分數：0";
        }

    }
}
