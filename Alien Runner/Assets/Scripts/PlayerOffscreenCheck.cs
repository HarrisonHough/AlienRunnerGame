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
    public class PlayerOffscreenCheck : MonoBehaviour
    {
        [SerializeField]
        private float offset = 16f;

        private bool _offscreen;
        private float _offsceenX = 0;
        private Rigidbody2D _rb2d;
        private IRecyclable recycle;

        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            recycle = GetComponent<IRecyclable>();
        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            _offsceenX = (Screen.width / PixelPerfectCamera.pixelToUnits) / 2 + offset;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            CheckForOffScreen();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckForOffScreen()
        {
            var posX = transform.position.x;
            var dirX = _rb2d.velocity.x;
            if (Mathf.Abs(posX) > _offsceenX)
            {
                if (dirX < 0 && posX < -_offsceenX)
                {
                    _offscreen = true;
                }
                else if (dirX > 0 && posX > _offsceenX)
                {
                    _offscreen = true;
                }
                else
                    _offscreen = false;
                if (_offscreen)
                {
                    OnOutOfBounds();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnOutOfBounds()
        {
            _offscreen = false;
            GameManager.instance.OnPlayerKilled();
        }
    }
}
