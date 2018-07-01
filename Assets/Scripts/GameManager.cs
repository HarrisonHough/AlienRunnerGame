using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Game Manager Class (Singleton)
*/
namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance = null;
        public static float gameSpeedMultiplier = 1.0f;
        public float speedIncreaseAmount = 0.5f;
        public GameObject playerPrefab;
        public Text scoreText;
        public bool _beatBestTime;
        public GameObject menu;

        private AudioManager _audioManager;
        private float _timeElapsed;
        private float _bestTime;
        private bool _gameStarted;
        private GameObject _player;
        private TimeManager _timeManager;
        private GameObject _floor;
        private Spawner _spawner;
        private int hideHelp;                           //0 = not hidden  1 = hidden
        public bool HideHelp { get { return hideHelp == 0 ? false: true; } }

        [SerializeField]
        private UIControl _uiControl;


        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            EnforceSingleton();
            GetMemberVariables();
        }

        /// <summary>
        /// 
        /// </summary>
        private void EnforceSingleton()
        {
            if (instance == null)
                instance = this;
            else if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetMemberVariables()
        {
            _floor = GameObject.Find("Foreground");
            _spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
            _timeManager = GetComponent<TimeManager>();
            _audioManager = GetComponent<AudioManager>();
            _uiControl = FindObjectOfType<UIControl>();
        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            PlayerPrefs.DeleteAll(); //to delete stored high scores
            
            //create player
            _player = _spawner.SpawnObject(playerPrefab);
            //disable player before starting game
            _player.GetComponent<IRecyclable>().Shutdown();
            SetFloorPosition();

            //disable spawner
            _spawner.active = false;

            //Time.timeScale = 0;
            gameSpeedMultiplier = 0;
            //load best time
            _bestTime = PlayerPrefs.GetFloat("Alien_bestTime");

            CheckHideHelp();
        }

        public void CheckHideHelp()
        {
            //check for show help
            hideHelp = PlayerPrefs.GetInt("Alien_hideHelp");
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetFloorPosition()
        {
            var floorHeight = _floor.transform.localScale.y;

            var pos = _floor.transform.position;
            pos.x = 0;
            pos.y = -((Screen.height / PixelPerfectCamera.pixelToUnits) / 2) + (floorHeight / 2);
            _floor.transform.position = pos;

        }

        public void ToggleHelp(bool storeSetting)
        {
            if (storeSetting)
            {
                PlayerPrefs.SetInt("Alien_hideHelp", 1);
            }

            //
            hideHelp = 0;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

            //DisplayMenu();
            DisplayScoreText();
            CheckForQuit();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckForQuit()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                QuitGame();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DisplayMenu()
        {
            if (!_gameStarted && gameSpeedMultiplier == 0)
            {
                menu.SetActive(true);
            }
        }

        public void GameOver() {
            menu.SetActive(true);
        }
        /// <summary>
        /// 
        /// </summary>
        private void DisplayScoreText()
        {

            if (!_gameStarted)
            {
                
                var textColor = _beatBestTime ? "#FF0" : "#FFF";

                scoreText.text = "TIME: " + Utilities.FormatTime(_timeElapsed) + "\n<color=" + textColor + ">BEST: " + Utilities.FormatTime(_bestTime) + "</color>";
            }
            else
            {
                _timeElapsed += Time.deltaTime;
                scoreText.text = "TIME: " + Utilities.FormatTime(_timeElapsed);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncreaseGameSpeed()
        {
            gameSpeedMultiplier += speedIncreaseAmount;
            _uiControl.IncreaseSpeedText();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetGameSpeed()
        {
            gameSpeedMultiplier = 1.0f;
            _uiControl.ResetSpeedText();
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnPlayerKilled()
        {

            _audioManager.GameOver();
            _spawner.active = false;

            //TODO remove
            //var playerDestroyScript = _player.GetComponent<DestroyOffscreen>();
            // playerDestroyScript.DestroyCallback -= OnPlayerKilled; //unlinks connection to delegate
            _player.GetComponent<IRecyclable>().Shutdown();

            ResetPlayerVelocity();
            //stop time over duration   

            //_timeManager.ManipulateTime(0, 5.5f);
            _timeManager.ManipulateSpeed(0, 2.0f);

            //game stopped
            _gameStarted = false;
            //continueText.text = "PRESS ANY BUTTON TO RESTART";
            CheckHighScore();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckHighScore()
        {

            if (_timeElapsed > _bestTime)
            {
                _bestTime = _timeElapsed;
                PlayerPrefs.SetFloat("Alien_bestTime", _bestTime);
                _beatBestTime = true;
                PlaySound("Win");
            }
            else
            {
                PlaySound("Lose");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetPlayerVelocity()
        {
            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayGame()
        {

            gameSpeedMultiplier = 1f;
            menu.SetActive(false);

            //_timeManager.ManipulateTime(1, 1f);
            _timeManager.ManipulateSpeed(1f,1f);
            ResetGame();
            _audioManager.StartGame();
            //PlaySound("Click");
        }

        public void CheckGameSettings() {

            Debug.Log("Game speed multiplier = " + gameSpeedMultiplier);
            Debug.Log("Time Manager time scale = " + _timeManager.GetTimeScale());

        }

        /// <summary>
        /// 
        /// </summary>
        void ResetGame()
        {
            _spawner.DisableAllObstacles();
            _spawner.active = true;
            

            Debug.Log("Player vector set to " + new Vector3(0, (Screen.height / PixelPerfectCamera.pixelToUnits) /2 + 100, 0));

            _player.transform.position = new Vector3(0, (Screen.height / PixelPerfectCamera.pixelToUnits) / 2 + 100, 0);
            _player.SetActive(true);
            _player.GetComponent<IRecyclable>().Restart();


            _gameStarted = true;

            _timeElapsed = 0;
            _beatBestTime = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /*string FormatTime(float value)
        {
            TimeSpan t = TimeSpan.FromSeconds(value);
            return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sound"></param>
        public void PlaySound(string sound)
        {

            switch (sound)
            {
                case "Jump":
                    _audioManager.PlayJumpSound();
                    break;
                case "Click":
                    _audioManager.PlayClickSound();
                    break;
                case "Win":
                    _audioManager.PlayWinSound();
                    break;
                case "Lose":
                    _audioManager.PlayLoseSound();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        }
    }
}
