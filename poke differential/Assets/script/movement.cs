using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public float speed = 6f;
    public float pickups;
    public float camerapos = 0;
    void Awake ()
    {
        
    }
	void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-moveHorizontal*Time.deltaTime, 0.0f, -moveVertical*Time.deltaTime);
        if (pickups>=1)
        {
            GameObject.FindGameObjectWithTag("Pick Up").SetActive(false);
        }

        transform.Translate(movement*speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            SceneManager.LoadSceneAsync("LøsOpgave");
        }
        if (other.gameObject.CompareTag("Camera change"))
        {
            if(camerapos==0)
            {
                camerapos = 1;
            }
            else
            {
                camerapos = 0;
            }
        }
        if(other.gameObject.CompareTag("NPC"))
        {
            SceneManager.LoadSceneAsync("MacDonalskopi");
        }
    }
}
