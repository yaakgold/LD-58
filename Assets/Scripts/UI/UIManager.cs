using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        public GameObject pauseMenu, endGameMenu;
        public TMP_Text endGameText;

        public bool gameOver;
        
        private bool _isPaused;
        
        public void PauseGameToggle()
        {
            _isPaused = !_isPaused;
            pauseMenu.SetActive(_isPaused);
            Time.timeScale = _isPaused ? 0 : 1;
        }

        public void OnPlayerDeath()
        {
            gameOver = true;
            endGameText.text = "YOU DIED";
            endGameText.color = Color.red;
            endGameMenu.SetActive(true);
            Time.timeScale = .1f;
        }

        public void OnPlayerWin()
        {
            gameOver = true;
            endGameText.text = "YOU DEFEATED THE COLLECTOR!";
            endGameText.color = Color.green;
            endGameMenu.SetActive(true);
            Time.timeScale = .5f;
        }
        
        public void QuitToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
