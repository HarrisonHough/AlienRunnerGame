using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Exit Game Class
*/

/// <summary>
/// 
/// </summary>
public class ExitGame : MonoBehaviour {

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {

        if (Input.GetKey("escape")) {
            Application.Quit();
        }	
	}
}
