using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{ 
    int rot = 0;
	// Use this for initialization
	void Start ()
    {
        GameObject player = GameObject.Find("player");
        movement playerscript = player.GetComponent<movement>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.Find("player").GetComponent<movement>().pickups != 0)
        {
            if (rot < 60)
            {
                transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime);
                rot++;
            }
        }
    }
}
