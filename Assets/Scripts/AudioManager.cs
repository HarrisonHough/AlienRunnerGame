using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Audio Manager Class
*/

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

        [SerializeField]
        public GameMusicLoops[] gameMusicArray;

        //public variables assigned in scene
        [SerializeField]
        private AudioMixer _mixer;
        [SerializeField]
        private AudioSource _musicAudio;
        [SerializeField]
        private AudioSource _SFXAudio;
        
        [SerializeField]
        private AudioClip _playerJump;
        [SerializeField]
        private AudioClip _UIClick;
        [SerializeField]
        private AudioClip _gameOverWin;
        [SerializeField]
        private AudioClip _gameOverLose;
        [SerializeField]
        private AudioClip _menuMusic;        

        private float[] _loopLengths;
        private int _loopIndex;
        private bool _playMusicLoop;
        private bool _playerKilled = false;
        private float _timer = 0;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            _loopLengths = new float[gameMusicArray.Length];
            for (int i = 0; i < _loopLengths.Length; i++)
                _loopLengths[i] = gameMusicArray[i].gameMusic.length * gameMusicArray[i].musicLoops;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartGame()
        {
            _playMusicLoop = true;
            _timer = 0;
            _loopIndex = 0;
            _musicAudio.clip = gameMusicArray[_loopIndex].gameMusic;
            //musicAudio.Play();
            StartCoroutine(PlayMusicLoop());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayMusicLoop()
        {
            _playMusicLoop = true;

            _loopIndex = 0;
            _musicAudio.loop = true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private float GetMusicLoopLength(int index)
        {
            return gameMusicArray[_loopIndex].gameMusic.length * gameMusicArray[_loopIndex].musicLoops;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loopIndex"></param>
        private void PlayNewClip()
        {
            _musicAudio.clip = gameMusicArray[_loopIndex].gameMusic;
            _musicAudio.Play();
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
                _musicAudio.Stop();
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
            if (_playerJump != null)
                _SFXAudio.PlayOneShot(_playerJump);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayWinSound()
        {
            if (_gameOverWin != null)
                _SFXAudio.PlayOneShot(_gameOverWin);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayLoseSound()
        {
            if (_gameOverLose != null)
                _SFXAudio.PlayOneShot(_gameOverLose);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayClickSound()
        {
            if (_UIClick != null)
                _SFXAudio.PlayOneShot(_UIClick);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayMenuMusic()
        {
            if (_menuMusic != null)
            {
                _musicAudio.clip = _menuMusic;
                _musicAudio.Play();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetMusicVolume(float value)
        {
            //TODO convert from normalized value ( EG 0 - 1)
            _mixer.SetFloat("musicVol", value.Remap(0, 1, -40, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetSFXVolume(float value)
        {            
            //TODO convert From normalized value ( EG 0 - 1)
            _mixer.SetFloat("SFXVol", value.Remap(0, 1, -40, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float GetMusicVolume()
        {
            float volume;
            _mixer.GetFloat("musicVol", out volume);

            //TODO convert to normalized value ( EG 0 - 1)
            return volume.Remap(-40, 0, 0, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float GetSFXVolume()
        {
            float volume;
            _mixer.GetFloat("SFXVol", out volume);

            //TODO convert to normalized value ( EG 0 - 1)
            return volume.Remap(-40,0, 0,1);
        }
    }

    

}