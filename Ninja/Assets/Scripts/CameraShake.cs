using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 相機震動
/// </summary>
public class CameraShake : MonoBehaviour
{
    float shakeTime = 0.5f;//震動時間
    float currentTime = 0.0f;
    private List<Vector3> gameobjpons = new List<Vector3>();
    //public Camera camera;//要求震動的相機
    public bool isShake = false;

    void Update()
    {
        //if (Input.GetKeyDown("a"))
        //{
        //    currentTime = shakeTime;
        //}
        if (isShake)
            currentTime = shakeTime;
    }

    void LateUpdate() {
        UpdateShake(); 
    }
    void UpdateShake()
    {
        if (currentTime > 0.0f)
        {
            isShake = false;
            currentTime -= Time.deltaTime;
            GetComponent<Camera>().rect = new Rect(0.04f * (-1.0f + 2.0f * Random.value) * Mathf.Pow(currentTime, 2), 
                                                   0.04f * (-1.0f + 2.0f * Random.value) * Mathf.Pow(currentTime, 2), 
                                                   1.0f, 1.0f);
        }
        else
        {           
            currentTime = 0.0f;            
        }
    }
}