using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Obstacle Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class Obstacle : MonoBehaviour, IRecyclable
    {

        public Sprite[] sprites;

        public Vector2 colliderOffset = Vector2.zero;

        public void OnEnable()
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Restart()
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.sprite = sprites[Random.Range(0, sprites.Length)];

            var collider = GetComponent<BoxCollider2D>();
            var size = renderer.bounds.size;
            size.y += colliderOffset.y;

            collider.size = size;
            collider.offset = new Vector2(-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Shutdown()
        {
            Debug.Log("Obstacle shut down, disable trigger");
            GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.SetActive(false);

        }

    }
}
