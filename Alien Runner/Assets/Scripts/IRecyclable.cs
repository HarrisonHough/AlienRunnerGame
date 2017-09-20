using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: IRecycle Interface
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecyclable
    {

        void Shutdown();

        void Restart();
    }
}
