using ThanhNK;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
    
        public Canvas PauseScene;
        
        public void ReloadScene()
        {
            ContinueGame();
        }

        // Hàm Load Scene Home
        public void LoadSceneHome()
        {
            SceneManager.LoadScene("Home");
        }

        // Hàm Load Scene Instruction
        public void LoadSceneInstruction()
        {
            SceneManager.LoadScene("Instruction");
        }

        // Hàm Load Scene About Us
        public void LoadSceneAboutUs()
        {
            SceneManager.LoadScene("AboutUs");
        }

        // Hàm New Game, có thể là khởi đầu lại từ đầu
        public void NewGame()
        {
            ContinueGame();
            PlayerData.PLayerHealth = 100;
            PlayerData.PLayerMana = 100;
            PlayerData.OldPLayerHealth = 100;
            PlayerData.OldPlayerMana = 100;
        }

        // Hàm Continue để tiếp tục game từ điểm lưu trước đó
        public void ContinueGame()
        {
            SceneManager.LoadScene("Level"+ PlayerData.CurrentLevel);
            // GameManager.Instance.LoadGame(); // Tải trạng thái của người chơi nếu có
        }

        // Hàm Pause game
        public void PauseGame()
        {
            Time.timeScale = 0; // Dừng tất cả các hoạt động trong game
            Debug.Log("Game is paused.");
            PauseScene.enabled = true;
        }

        // Hàm Resume game
        public void ResumeGame()
        {
            Time.timeScale = 1; 
            Debug.Log("Game is resumed.");
            PauseScene.enabled = false;
        }
    }
}