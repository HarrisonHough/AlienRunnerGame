using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AlienRunner
{
    [System.Serializable]
    public class GameMusicLoops
    {
        public AudioClip gameMusic;
        public int musicLoops;
    }


    public class AudioManager : MonoBehaviour
    {
        //public variables assigned in scene
        public AudioMixer mixer;
        public AudioSource musicAudio;
        public AudioSource SFXAudio;
        
        public AudioClip playerJump;
        public AudioClip UIClick;
        public AudioClip gameOverWin;
        public AudioClip gameOverLose;
        public AudioClip menuMusic;

        public GameMusicLoops[] gameMusicArray;
        public float[] loopLengths;


        private int _loopIndex;
        private bool _playMusicLoop;
        private bool _playerKilled = false;

        float timer = 0;
        // Use this for initialization

        void Start()
        {
            loopLengths = new float[gameMusicArray.Length];
            for (int i = 0; i < loopLengths.Length; i++)
                loopLengths[i] = gameMusicArray[i].gameMusic.length * gameMusicArray[i].musicLoops;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartGame()
        {
            _playMusicLoop = true;
            timer = 0;
            _loopIndex = 0;
            musicAudio.clip = gameMusicArray[_loopIndex].gameMusic;
            //musicAudio.Play();
            StartCoroutine(PlayMusicLoop());
        }

        IEnumerator PlayMusicLoop()
        {
            _playMusicLoop = true;

            _loopIndex = 0;
            musicAudio.loop = true;
            float time = 0;
            PlayNewClip();
            while (_playMusicLoop)
            {
                time += Time.deltaTime;

                if (time >= GetMusicLoopLength(_loopIndex))
                {
                    if (_loopIndex < gameMusicArray.Length)
                    {
                        Debug.Log("time passed now increment loop index");
                        _loopIndex++;
                        Debug.Log("Loop index now = " + _loopIndex);
                        GameManager.instance.IncreaseGameSpeed();
                        PlayNewClip();
                    }
                    time = 0;
                }
                yield return null;
            }
        }

        private float GetMusicLoopLength(int index)
        {
            return gameMusicArray[_loopIndex].gameMusic.length * gameMusicArray[_loopIndex].musicLoops;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /*IEnumerator PlayMusicLoop2()
        {
            _playMusicLoop = true;

            int _loopIndex = 0;
            musicAudio.loop = true;
            while (_loopIndex < gameMusicArray.Length)
            {
                PlayNewClip(_loopIndex);
                yield return new WaitForSeconds(gameMusicArray[_loopIndex].gameMusic.length * gameMusicArray[_loopIndex].musicLoops);

                if (_playerKilled)
                {
                    _loopIndex = gameMusicArray.Length;
                    _playerKilled = false;
                    yield return null;
                }

                //increment loop index
                _loopIndex++;
                Debug.Log("Play next sound loop");
                //increase speed
                GameManager.instance.IncreaseGameSpeed();
                //GameManager.gameSpeedMultiplier += GameManager.instance.speedIncreaseAmount;
                Debug.Log("Increase game speed Multiplier to " + GameManager.gameSpeedMultiplier);
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loopIndex"></param>
        private void PlayNewClip()
        {
            musicAudio.clip = gameMusicArray[_loopIndex].gameMusic;
            musicAudio.Play();
            Debug.Log("playing music loop #" + _loopIndex + " waiting for" + gameMusicArray[_loopIndex].gameMusic.length * gameMusicArray[_loopIndex].musicLoops + " seconds");
        }

        /// <summary>
        /// 
        /// </summary>
        public void GameOver()
        {
            if (_playMusicLoop)
            {
                _playMusicLoop = false;                
                musicAudio.Stop();
                Debug.Log("Game Over Stopping music loop");                
                GameManager.instance.ResetGameSpeed();
                StopCoroutine(PlayMusicLoop());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayJumpSound()
        {
            if (playerJump != null)
                SFXAudio.PlayOneShot(playerJump);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayWinSound()
        {
            if (gameOverWin != null)
                SFXAudio.PlayOneShot(gameOverWin);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayLoseSound()
        {
            if (gameOverLose != null)
                SFXAudio.PlayOneShot(gameOverLose);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayClickSound()
        {
            if (UIClick != null)
                SFXAudio.PlayOneShot(UIClick);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayMenuMusic()
        {
            if (menuMusic != null)
            {
                musicAudio.clip = menuMusic;
                musicAudio.Play();
            }
        }

        public void SetMusicVolume(float value)
        {
            //TODO convert from normalized value ( EG 0 - 1)
            mixer.SetFloat("musicVol", value.Remap(0, 1, -40, 0));
        }

        public void SetSFXVolume(float value)
        {
            
            //TODO convert From normalized value ( EG 0 - 1)
            mixer.SetFloat("SFXVol", value.Remap(0, 1, -40, 0));
        }

        public float GetMusicVolume()
        {
            float volume;
            mixer.GetFloat("musicVol", out volume);


            //TODO convert to normalized value ( EG 0 - 1)
            return volume.Remap(-40, 0, 0, 1);
        }

        public float GetSFXVolume()
        {
            float volume;
            mixer.GetFloat("SFXVol", out volume);

            //TODO convert to normalized value ( EG 0 - 1)
            return volume.Remap(-40,0, 0,1);
        }
    }

    

}