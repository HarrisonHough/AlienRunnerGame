using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Recycle Off Screen Class
*/

/// <summary>
/// 
/// </summary>
public class RecycleOnCollision : MonoBehaviour {

    IRecyclable recycle;

    private void Start()
    {
        recycle = GetComponent<IRecyclable>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destroy")
        {            
                recycle.Shutdown();
        }
    }
}
