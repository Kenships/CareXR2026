using System;
using Obvious.Soap;
using UnityEngine;

namespace _Project.Scripts
{
    public class TutorialCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject nextButton;
        [SerializeField] private GameObject[] tutorialPages;
        [SerializeField] private GameObject calibration;
        [SerializeField] private ScriptableEventNoParam completedTutorial;
        [SerializeField] private ScriptableEventNoParam starsComplete;

        [SerializeField] private GameObject stars;

        private int _currentTutorialPage;

        private void OnEnable()
        {
            calibration.SetActive(false);
            _currentTutorialPage = 0;
            tutorialPages[_currentTutorialPage].SetActive(true);
            _currentTutorialPage++;

            starsComplete.OnRaised += Next;
        }

        private void OnDisable()
        {
            starsComplete.OnRaised -= Next;
        }

        public void Next()
        {
            if (_currentTutorialPage == 1)
            {
                calibration.SetActive(true);
                nextButton.SetActive(false);
                tutorialPages[_currentTutorialPage - 1].SetActive(false);
                tutorialPages[_currentTutorialPage].SetActive(true);
                _currentTutorialPage++;
                return;
            }

            if (_currentTutorialPage == 2)
            {
                stars.SetActive(true);
                nextButton.SetActive(false);
                tutorialPages[_currentTutorialPage - 1].SetActive(false);
                tutorialPages[_currentTutorialPage].SetActive(true);
                _currentTutorialPage++;
                return;
            }
            nextButton.SetActive(true);

            tutorialPages[_currentTutorialPage - 1].SetActive(false);

            if (_currentTutorialPage < tutorialPages.Length)
            {
                tutorialPages[_currentTutorialPage].SetActive(true);
                _currentTutorialPage++;
            }
            else
            {
                nextButton.SetActive(false);
                completedTutorial.Raise();
            }
                
        }
    }
}
