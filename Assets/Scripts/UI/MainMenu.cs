using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject pnlInstruction;
        
        public void OnStartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OnInstructions()
        {
            pnlInstruction.SetActive(true);
        }

        public void OnInstructionsDone()
        {
            pnlInstruction.SetActive(false);
        }

        public void OnQuit()
        {
            Application.Quit();
        }
    }
}
