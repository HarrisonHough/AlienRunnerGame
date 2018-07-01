using UnityEngine;
using System.Collections;
using AlienRunner;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Animated Texture Class
*/


namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class AnimatedTexture : MonoBehaviour
    {

        [SerializeField]
        private Vector2 speed = Vector2.zero;

        private Vector2 _offset = Vector2.zero;
        private Material _material;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            _material = GetComponent<Renderer>().material;

            _offset = _material.GetTextureOffset("_MainTex");
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            _offset += speed * Time.deltaTime * GameManager.gameSpeedMultiplier;

            _material.SetTextureOffset("_MainTex", _offset);
        }
    }
}
