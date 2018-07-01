using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Tiled Background Class
*/


namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class TiledBackground : MonoBehaviour
    {
        [SerializeField]
        private int _textureSize = 32;
        [SerializeField]
        private bool _scaleHorizontally = true;
        [SerializeField]
        private bool _scaleVertically = true;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            SetHeightAndWidth(GetWidth(), GetHeight());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        private void SetHeightAndWidth(float newWidth, float newHeight)
        {
            transform.localScale = new Vector3(newWidth * _textureSize, newHeight * _textureSize, 1);
            GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private float GetHeight()
        {
            return !_scaleVertically ? 1 : Mathf.Ceil(Screen.height / (_textureSize * PixelPerfectCamera.scale));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private float GetWidth()
        {
            return !_scaleHorizontally ? 1 : Mathf.Ceil(Screen.width / (_textureSize * PixelPerfectCamera.scale));
        }
    }
}
