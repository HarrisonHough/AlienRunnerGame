using UnityEngine;
using System.Collections;


/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Pixel Perfect Camera Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class PixelPerfectCamera : MonoBehaviour
    {

        public static float pixelToUnits = 1f;
        public static float scale = 1f;

        [SerializeField]
        private Vector2 nativeResolution = new Vector2(480, 320);

        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            SetResolution();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetResolution()
        {
            var camera = GetComponent<Camera>();

            if (camera.orthographic)
            {
                scale = Screen.height / nativeResolution.y;
                pixelToUnits = 1 * scale;
                camera.orthographicSize = (Screen.height / 2.0f) / pixelToUnits;

                //Debug.Log("Scale = " + scale);
                //Debug.Log("Pixel to units = " + pixelToUnits);
                //Debug.Log("Screen height = " + Screen.height);
                //Debug.Log("Camera orth size = " + camera.orthographicSize);
            }
        }
    }
}
