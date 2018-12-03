using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Ligninger : MonoBehaviour
{
    Assignment Ligning = new Assignment(Assignment.Difficulty.EASY);
    public string Svar;
    public InputField input;
     

    // Use this for initialization
    void Start ()
    {
        GetComponent<TextMesh>().text = Ligning.Create();
        Svar = Ligning.correctAnswer;
        Debug.Log(Ligning.correctAnswer);
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadSceneAsync("Vundet");
            Debug.Log(Svar);
        }
    }
}
