using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Recycle GameObject Class
*/
namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class RecycleGameObject : MonoBehaviour, IRecyclable
    {
        
        /// <summary>
        /// 
        /// </summary>
        public void Restart()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Shutdown()
        {
            gameObject.SetActive(false);
        }
    }
}
