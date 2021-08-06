using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject scene;
    void Update()
    {
        Debug.Log(Input.acceleration);
        scene.transform.rotation = Quaternion.Euler(Input.acceleration.y * 90, 0, Input.acceleration.x * -90);
    }
}
