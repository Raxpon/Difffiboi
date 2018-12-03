using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    private Vector3 offset;

    // Use this for initialization
    void Start ()
    {
        transform.Translate(5.85f, 2f ,-6f);
    }

    // Update is called once per frame
    void Update()
    {
       /* if (GameObject.Find("player").GetComponent<movement>().camerapos == 1)
        {
            transform.Translate(-5.85f,8.05f,-16.89f);
        }
        if(GameObject.Find("player").GetComponent<movement>().camerapos == 0)
        {
            transform.Translate(0f, 8.05f, -5.89f);
        }*/
    }
}
