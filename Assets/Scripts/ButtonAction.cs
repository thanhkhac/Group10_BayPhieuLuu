using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class ButtonAction : MonoBehaviour
    {
        // Hàm Reload lại Scene hiện tại
        public void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
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
            SceneManager.LoadScene("Level1");
            // GameManager.Instance.ResetGame(); // Đặt lại game nếu cần thiết
        }

        // Hàm Continue để tiếp tục game từ điểm lưu trước đó
        public void ContinueGame()
        {
            string lastScene = PlayerPrefs.GetString("LastSavedScene", "Home");
            SceneManager.LoadScene(lastScene);
            // GameManager.Instance.LoadGame(); // Tải trạng thái của người chơi nếu có
        }

        // Hàm Pause game
        public void PauseGame()
        {
            Time.timeScale = 0; // Dừng tất cả các hoạt động trong game
            Debug.Log("Game is paused.");
            // Hiển thị UI Pause nếu có (chẳng hạn kích hoạt một Canvas Pause)
        }

        // Hàm Resume game
        public void ResumeGame()
        {
            Time.timeScale = 1; // Tiếp tục tất cả các hoạt động trong game
            Debug.Log("Game is resumed.");
            // Ẩn UI Pause nếu có
        }
    }
}