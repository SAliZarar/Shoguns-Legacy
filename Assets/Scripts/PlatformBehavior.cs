using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    PlaySceneController controller;
    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Controller");
        controller = obj.GetComponent<PlaySceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < Camera.main.transform.position.y - 4)
        {
            controller.AddPlatform();
            Destroy(gameObject);
        }
    }
}
