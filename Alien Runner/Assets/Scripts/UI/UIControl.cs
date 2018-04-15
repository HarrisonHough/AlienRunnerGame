using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: UIControl Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class UIControl : MonoBehaviour
    {
        [SerializeField]
        private GameObject _settingsPanel;
        [SerializeField]
        private GameObject _menuPanel;

        [SerializeField]
        private Slider _SFXSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Text _speedText;
        private int _speedValue = 1;

        private GameObject[] _settingsElements;
        private GameObject[] _menuElements;
        private AudioManager _audio;
        private Tutorial _tutorial;

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _audio = FindObjectOfType<AudioManager>();
            _tutorial = GetComponent<Tutorial>();
            ResetSpeedText();
        }

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            InitializeElements();
            SetSettingsPanelVisibility(false);

            _SFXSlider.onValueChanged.AddListener(delegate { AdjustSFXSlider(); });
            _musicSlider.onValueChanged.AddListener(delegate { AdjustMusicSlider(); });
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeElements()
        {
            _settingsElements = new GameObject[_settingsPanel.transform.childCount];

            for (int i = 0; i < _settingsElements.Length; i++)
            {
                _settingsElements[i] = _settingsPanel.transform.GetChild(i).gameObject;
            }

            _menuElements = new GameObject[_menuPanel.transform.childCount];

            for (int i = 0; i < _menuElements.Length; i++)
            {
                _menuElements[i] = _menuPanel.transform.GetChild(i).gameObject;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetSettingsPanelVisibility(bool isVisible)
        {
            for (int i = 0; i < _settingsElements.Length; i++)
            {
                _settingsElements[i].SetActive(isVisible);
            }
            UpdateSoundSliders();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetMenusPanelVisibility(bool isVisible)
        {
            for (int i = 0; i < _menuElements.Length; i++)
            {
                _menuElements[i].SetActive(isVisible);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayGame() {

            if (GameManager.instance.HideHelp)
            {
                GameManager.instance.PlayGame();
            }
            else {
                _tutorial.StartTutorial();
                Debug.Log("Setting " + _menuPanel.name +  " to false");
                _menuPanel.SetActive(false);
                GameManager.instance.CheckGameSettings();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void ExitGame()
        {
            // TODO add exit game logic
            //SceneManager.LoadScene(0);
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateSoundSliders()
        {
            _SFXSlider.value = _audio.GetSFXVolume();
            _musicSlider.value = _audio.GetMusicVolume();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AdjustSFXSlider()
        {
            _audio.SetSFXVolume(_SFXSlider.value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AdjustMusicSlider()
        {
            _audio.SetMusicVolume(_musicSlider.value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncreaseSpeedText() {

            _speedValue++;
            UpdateSpeedText();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateSpeedText() {
            
            _speedText.text = "SPEED\nX " + _speedValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetSpeedText() {

            _speedValue = 1;
            UpdateSpeedText();
        }
    }

}
