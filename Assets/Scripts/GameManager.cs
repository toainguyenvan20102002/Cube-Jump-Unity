using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score;

    float countScore = 0;

    int maxSpeed;

    bool isEndGame = false;
    bool isPauseGame = false;

    [SerializeField] PlayerController playerController;
    [SerializeField] float timeToNewLevel;

    [Header("UI")]
    [SerializeField] Text textScore;
    [SerializeField] GameObject panelEndGame;
    [SerializeField] GameObject panelStartGame;
    [SerializeField] Text textHighScore;
    [SerializeField] Text textEndGameScore;
    [SerializeField] Button pauseGame;
    [SerializeField] Button resumeGame;

    float countTime;

    private void Start()
    {
        if(instance == null) instance = this;

        countTime = 0;
        score = 0;
        maxSpeed = 25;
        isEndGame = true;
        Time.timeScale = 0;
    }

    private void Update()
    {
 
        if (isEndGame || isPauseGame) return;
        countScore += Time.deltaTime;
        countTime += Time.deltaTime;
        if(countTime >= timeToNewLevel && LaneManager.GetSpeed() < maxSpeed)
        {
            LaneManager.AddSpeed(1);
            countTime = 0;
            GetComponent<LaneManager>().SetSpeed();
        }

        score = (int)countScore;
        textScore.text = score.ToString();
    }

    public void AddScore(int add)
    {
        score += add;
    }

    public void EndGame()
    {
        if (isEndGame || isPauseGame) return;
        Time.timeScale = 0.5f;
        isEndGame = true;
        // Invoke
        Invoke("TurnOnPanel", 1.0f);
    }

    public void PauseGame()
    { 
        isPauseGame = true;
        Time.timeScale = 0;
        pauseGame.gameObject.SetActive(false);
        resumeGame.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        isPauseGame =false;
        Time.timeScale = 1;

        pauseGame.gameObject.SetActive(true);
        resumeGame.gameObject.SetActive(false);
    }

    private void TurnOnPanel()
    {
        panelEndGame.SetActive(true);
        textScore.gameObject.SetActive(false);
        Time.timeScale = 0;

        

        int highScore = GetHighScore();

        if(highScore < score)
        {
            highScore = score;
        }

        textHighScore.text = "High Score: " + highScore.ToString();
        textEndGameScore.text = "Your Score: " + score.ToString();

        SaveHighScore(highScore);
    }

    int GetHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", -1);
        return highScore;
    }

    void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void StartGame()
    {
        panelStartGame.SetActive(false);
        isEndGame = false;
        Time.timeScale = 1;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
