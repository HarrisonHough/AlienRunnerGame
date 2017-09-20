using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Menu Class
*/

/// <summary>
/// 
/// </summary>
public class Menu : MonoBehaviour {


    public string levelToLoad;

    /// <summary>
    /// 
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene(levelToLoad);

    }

    /// <summary>
    /// 
    /// </summary>
    public void ShowScores() {
        //show high scores leader board
    }
}
