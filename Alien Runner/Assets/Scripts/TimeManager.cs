using UnityEngine;
using System.Collections;
using AlienRunner;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: time Manager Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeManager : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newSpeed"></param>
        /// <param name="duration"></param>
        public void ManipulateSpeed(float newSpeed, float duration)
        {
            StartCoroutine(FadeSpeedTo(newSpeed, duration));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator FadeSpeedTo(float speed, float time)
        {
            float startSpeed = GameManager.gameSpeedMultiplier;
            float currentTime = 0f;
            while (GameManager.gameSpeedMultiplier != speed)
            {
                currentTime += Time.deltaTime;
                GameManager.gameSpeedMultiplier = Mathf.Lerp(startSpeed, speed, currentTime / time);
                yield return null;
            }
            if (GameManager.gameSpeedMultiplier == 0)
                GameManager.instance.GameOver();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTime"></param>
        /// <param name="duration"></param>
        public void ManipulateTime(float newTime, float duration)
        {

            if (Time.timeScale == 0)
                Time.timeScale = 0.1f;

            StartCoroutine(FadeTo(newTime, duration));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator FadeTo(float value, float time)
        {

            for (float t = 0f; t < 1; t += Time.deltaTime / time)
            {
                Time.timeScale = Mathf.Lerp(Time.timeScale, value, t);
                //if(Mathf.Abs(value - Time.timeScale) < .01f){
                if (Mathf.Abs(value - Time.timeScale) < 0.01f)
                {
                    Time.timeScale = value;
                    yield break;
                }

                yield return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float GetTimeScale()
        {
            return Time.timeScale;
        }
    }
}
