using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Collision Switch Class
*/


namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class CollisionSwitch : MonoBehaviour
    {

        private Rigidbody2D _parentRB;
        private Collider2D _parentCollider;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        private void Start()
        {
            _parentRB = transform.parent.GetComponent<Rigidbody2D>();
            _parentCollider = transform.parent.GetComponent<Collider2D>();
            //Debug.Log("Parents name is " + transform.parent.gameObject.name);
            _parentCollider.isTrigger = false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log("Switch trigger entered");
            if (other.tag == "Player")
            {
                //Debug.Log("Turn on trigger");
                //turn off RB to allow player to pass through
                //_parentRB.simulated = false;
                _parentCollider.isTrigger = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit2D(Collider2D other)
        {
            //Debug.Log("Switch trigger exited");
            if (other.tag == "Player")
            {
                //Debug.Log("Turn off trigger");
                //turn off RB to allow player to pass through
                //_parentRB.simulated = false;
                //_parentCollider.isTrigger = false;
            }
        }
    }
}
