using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Instant Velocity Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class InstantVelocity : MonoBehaviour
    {

        public Vector2 velocity = Vector2.zero;

        private Rigidbody2D _rb2d;

        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        void FixedUpdate()
        {
            _rb2d.velocity = velocity * GameManager.gameSpeedMultiplier;
        }
    }
}