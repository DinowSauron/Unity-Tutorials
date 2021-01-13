using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    public float intensity;

    void Start()
    {
        
    }

    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * 3;
        float horizontal = -Input.GetAxis("Mouse X");
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, Quaternion.Euler(vertical, horizontal, 0), intensity * Time.deltaTime);
        

    }
    public void fired()
    {
        float x = Random.Range(-5, 5);
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, Quaternion.Euler(-10, x, 0), intensity * 10 * Time.deltaTime);
    }
}
