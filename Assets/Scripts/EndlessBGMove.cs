using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBGMove : MonoBehaviour
{
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject l4;
    void Update()
    {
        if (l1.transform.position.y < Camera.main.transform.position.y - 10)
        {
            l1.transform.position = new Vector3(l1.transform.position.x, l1.transform.position.y + 47, l1.transform.position.z);
            l2.transform.position = new Vector3(l2.transform.position.x, l2.transform.position.y + 47, l2.transform.position.z);
            l3.transform.position = new Vector3(l3.transform.position.x, l3.transform.position.y + 47, l3.transform.position.z);
            l4.transform.position = new Vector3(l4.transform.position.x, l4.transform.position.y + 47, l4.transform.position.z);
        }
    }
}
