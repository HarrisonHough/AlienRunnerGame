using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Player Animation Manager Class
*/

namespace AlienRunner
{

    /// <summary>
    /// 
    /// </summary>
    public class PlayerAnimationManager : MonoBehaviour
    {

        private Animator animator;
        private InputState inputState;

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            animator = GetComponent<Animator>();
            inputState = GetComponent<InputState>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            CheckForRunning();

        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckForRunning()
        {
            var running = true;

            if (inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold)
                running = false;

            animator.SetBool("Running", running);
        }
    }
}