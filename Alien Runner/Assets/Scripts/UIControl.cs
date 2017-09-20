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
        public GameObject settingsPanel;
        public GameObject menuPanel;

        [SerializeField]
        private Slider SFXSlider;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private Text speedText;
        private int speedValue = 1;

        private GameObject[] settingsElements;
        private GameObject[] menuElements;
        private AudioManager audio;
        private Tutorial tutorial;

        private void Awake()
        {
            audio = FindObjectOfType<AudioManager>();
            tutorial = GetComponent<Tutorial>();
            ResetSpeedText();
        }

        // Use this for initialization
        void Start()
        {
            InitializeElements();
            SetSettingsPanelVisibility(false);

            SFXSlider.onValueChanged.AddListener(delegate { AdjustSFXSlider(); });
            musicSlider.onValueChanged.AddListener(delegate { AdjustMusicSlider(); });
            
        }

        private void InitializeElements()
        {
            settingsElements = new GameObject[settingsPanel.transform.childCount];

            for (int i = 0; i < settingsElements.Length; i++)
            {
                settingsElements[i] = settingsPanel.transform.GetChild(i).gameObject;
            }

            menuElements = new GameObject[menuPanel.transform.childCount];

            for (int i = 0; i < menuElements.Length; i++)
            {
                menuElements[i] = menuPanel.transform.GetChild(i).gameObject;
            }
        }

        public void SetSettingsPanelVisibility(bool isVisible)
        {
            for (int i = 0; i < settingsElements.Length; i++)
            {
                settingsElements[i].SetActive(isVisible);
            }
            UpdateSoundSliders();

        }

        public void SetMenusPanelVisibility(bool isVisible)
        {
            for (int i = 0; i < menuElements.Length; i++)
            {
                menuElements[i].SetActive(isVisible);
            }
        }

        public void PlayGame() {

            if (GameManager.instance.HideHelp)
            {
                GameManager.instance.PlayGame();
            }
            else {
                tutorial.StartTutorial();
                Debug.Log("Setting " + menuPanel.name +  " to false");
                menuPanel.SetActive(false);
                GameManager.instance.CheckGameSettings();
            }

        }

        public void ExitGame()
        {
            // TODO add exit game logic
            //SceneManager.LoadScene(0);
        }

        private void UpdateSoundSliders()
        {
            SFXSlider.value = audio.GetSFXVolume();
            musicSlider.value = audio.GetMusicVolume();
        }

        public void AdjustSFXSlider()
        {
            audio.SetSFXVolume(SFXSlider.value);
        }

        public void AdjustMusicSlider()
        {
            audio.SetMusicVolume(musicSlider.value);
        }

        public void IncreaseSpeedText() {

            speedValue++;
            UpdateSpeedText();
        }

        private void UpdateSpeedText() {
            
            speedText.text = "SPEED\nX " + speedValue;
        }

        public void ResetSpeedText() {

            speedValue = 1;
            UpdateSpeedText();
        }
    }

}
