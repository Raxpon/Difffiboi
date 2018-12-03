using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sniksnak : MonoBehaviour
{
    int textnumber = 0;
    private GameObject text1;
    private GameObject text2;
    private GameObject text3;
    private GameObject text4;

    // Use this for initialization
    void Start ()
    {
        text1 = GameObject.FindGameObjectWithTag("Text 1");
        text2 = GameObject.FindGameObjectWithTag("Text 2");
        text3 = GameObject.FindGameObjectWithTag("Text 3");
        text4 = GameObject.FindGameObjectWithTag("Text 4");
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }
    private void OnMouseDown()
    {
        switch (textnumber)
        {
            case 0:
                textnumber = 1;
                break;
            case 1:
                text1.SetActive(false);
                text2.SetActive(true);
                textnumber = 2;
                break;
            case 2:
                text2.SetActive(false);
                text3.SetActive(true);
                textnumber = 3;
                break;
            case 3:
                text3.SetActive(false);
                text4.SetActive(true);
                textnumber = 4;
                break;
            case 4:
                textnumber = 5;
                break;
            case 5:
                SceneManager.LoadSceneAsync("MacDonals");
                break;
            default:
                break;
        }
    }
}

