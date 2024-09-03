using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    public GameObject L4;

    public void Move(float f)
    {
        L1.transform.Translate(new Vector3(0, f*0.3f, 0));
        L2.transform.Translate(new Vector3(0, 0, 0));
        L3.transform.Translate(new Vector3(0, f*0.2f, 0));
        L4.transform.Translate(new Vector3(0, f*0.1f, 0));
    }
}
