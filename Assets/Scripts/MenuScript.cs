using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI = null;
    [SerializeField] private GameObject pauseMenuUI = null;
    [SerializeField] private Text currenScoreText = null;
    [SerializeField] private PlayerController player = null;


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }    

    public void PauseGame()
    {
        Time.timeScale = 0f;
        currenScoreText.text = player.Score.ToString();

        inGameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        inGameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
