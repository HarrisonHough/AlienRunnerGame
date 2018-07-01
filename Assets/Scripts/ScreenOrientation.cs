using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Screen Orientation Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenOrientation : MonoBehaviour
    {

        public enum Orientation { Portrait, LandscapeLeft, LandscapeRight, Landscape }
        public Orientation screenOrientation;

        // Use this for initialization
        void Start()
        {

            SetOrientation();
        }

        void SetOrientation()
        {

            switch (screenOrientation)
            {
                case Orientation.Portrait:
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;

                    if (Screen.width > Screen.height)
                    {
                        SetResolution();
                    }
                    break;
                case Orientation.LandscapeLeft:
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                    if (Screen.width < Screen.height)
                    {
                        SetResolution();
                    }
                    break;
                case Orientation.LandscapeRight:
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeRight;
                    if (Screen.width < Screen.height)
                    {
                        SetResolution();
                    }
                    break;

                default:
                    Debug.Log("Something wen wrong in switch");
                    break;

            }

            
        }

        void SetResolution()
        {
            int width = Screen.width;
            int height = Screen.height;
            Screen.SetResolution(height, width, false);
            Debug.Log("Setting resolution");
        }


    }
}
