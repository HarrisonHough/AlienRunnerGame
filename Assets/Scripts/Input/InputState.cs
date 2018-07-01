using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Input Class
*/

/// <summary>
/// 
/// </summary>
public class InputState : MonoBehaviour {

    public bool actionButton;
    public float absVelX = 0f;
    public float absVelY = 0f;
    public bool standing;
    public float standingThreshold = 1;


    private Rigidbody2D _rb2d;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
        actionButton = Input.anyKeyDown;
	}

    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate() {
        absVelX = System.Math.Abs(_rb2d.velocity.x);
        absVelY = System.Math.Abs(_rb2d.velocity.y);

        standing = absVelY <= standingThreshold;
    }
}
