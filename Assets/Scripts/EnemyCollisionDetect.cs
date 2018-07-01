using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Enemy Collision Detect Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class EnemyCollisionDetect : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collided with something");
            if (other.tag == "Enemy")
            {
                GameManager.instance.OnPlayerKilled();

                //TODO  
                //GameObjectUtil.Destroy(gameObject);
                //Debug.Log("Collided with enemy");

            }
        }

    }
}
