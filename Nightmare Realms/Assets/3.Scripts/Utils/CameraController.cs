using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 playerPos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y + 1.5f, -10f);
        cam.transform.position = Vector3.Lerp(cam.transform.position, playerPos, 1f * Time.deltaTime);
    }
}
