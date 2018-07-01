using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Jump Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class Jump : MonoBehaviour
    {

        public float jumpSpeed = 240f;
        public float forwardSpeed = 20f;

        private Rigidbody2D _rb2d;
        private InputState _inputState;

        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _inputState = GetComponent<InputState>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            if (_inputState.standing)
            {
                if (_inputState.actionButton)
                {

                    _rb2d.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
                    GameManager.instance.PlaySound("Jump");
                }
            }
        }
    }
}
