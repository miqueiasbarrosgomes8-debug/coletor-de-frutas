using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pontuação")]
    private int score;
    private int highScore;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI finalScoreText;

    [Header("Painéis")]
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;

    [Header("Botões")]
    public GameObject restartButton;
    public GameObject menuButton;

    [Header("Spawner")]
    public Spawners spawner;

    private bool gameRunning = false;
    private bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        score = 0;

        scoreText.text = "Pontos: 0";

        highScoreText.text = "Recorde: " + highScore;

        mainMenuPanel.SetActive(true);

        gameOverPanel.SetActive(false);

        gameOverText.gameObject.SetActive(false);

        finalScoreText.gameObject.SetActive(false);

        restartButton.SetActive(false);

        menuButton.SetActive(false);
    }

    public void StartGame()
    {
        score = 0;

        gameEnded = false;

        gameRunning = true;

        scoreText.text = "Pontos: 0";

        mainMenuPanel.SetActive(false);

        gameOverPanel.SetActive(false);

        gameOverText.gameObject.SetActive(false);

        finalScoreText.gameObject.SetActive(false);

        restartButton.SetActive(false);

        menuButton.SetActive(false);

        spawner.StartSpawn();
    }
public void AddScore(int points)
{
    Debug.Log("AddScore chamado");

    if(gameEnded)
        return;

    score += points;

    scoreText.text = "Pontos: " + score;
}
    public void GameOver()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        gameRunning = false;

        spawner.StopSpawn();

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);

            PlayerPrefs.Save();
        }

        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        gameOverPanel.SetActive(true);

        gameOverText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        finalScoreText.text =
            "Pontuação: " + score +
            "\n\nRecorde: " + highScore;

        finalScoreText.gameObject.SetActive(true);

        restartButton.SetActive(true);

        menuButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}