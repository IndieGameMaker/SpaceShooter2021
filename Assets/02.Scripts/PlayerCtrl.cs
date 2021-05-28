using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCtrl : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");  // -1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical");    // -1.0f ~ 0.0f ~ +1.0f

        Debug.Log("h=" + h);
        Debug.Log("v=" + v);
    }
}