using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Recycle Off Screen Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class RecycleOnCollision : MonoBehaviour
    {

        IRecyclable recycle;

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            recycle = GetComponent<IRecyclable>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Destroy")
            {
                recycle.Shutdown();
            }
        }
    }
}