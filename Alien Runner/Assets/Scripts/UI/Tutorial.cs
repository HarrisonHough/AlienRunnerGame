using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: Tutorial Class
*/


namespace AlienRunner
{

    public class Tutorial : MonoBehaviour
    {

        [SerializeField]
        private GameObject tutorialPanel;

        [SerializeField]
        private GameObject[] tutorialPages;

        private int index = 0;

        // Use this for initialization
        void Start()
        {
            tutorialPanel.SetActive(false);
        }

        public void StartTutorial() {
            HideAllPages();
            tutorialPanel.SetActive(true);            
            tutorialPages[index].SetActive(true);
        }


        private void HideAllPages() {
            for (int i = 0; i < tutorialPages.Length; i++)
            {
                tutorialPages[i].SetActive(false);
            }
        }


        public void NextPage()
        {
            tutorialPages[index].SetActive(false);
            if(index< tutorialPages.Length)
                index++;
            tutorialPages[index].SetActive(true);
        }

        public void StartGame() {
            index = 0;
            GameManager.instance.PlayGame();
            tutorialPanel.SetActive(false);
        }

        public void ShowOnStart(Toggle tog)
        {
            /*if (storeSetting)
            {
                PlayerPrefs.SetInt("Alien_hideHelp", 1);
            }*/

            if (!tog.isOn)
            {
                //is not on
                //disable showOnStart saved value
                PlayerPrefs.SetInt("Alien_hideHelp", 1);
                GameManager.instance.CheckHideHelp();
            }

        }

        public void ToggleShowOnStart()
        {

        }

    }
}
