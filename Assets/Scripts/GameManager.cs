using UnityEngine;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text textScore_GameOver;
    public TMP_Text textTopScore_GameOver;

    public TMP_Text textScore;

    public BoxSpawner boxSpawner;

    public GameObject panelGameplay;
    public GameObject panelGameOver;

    public bool isGameActive;
    [SerializeField]
    private int score;

    public bool hasCollidedWithGround;

    public Sound gameOverSound;
    public int Score
    {
        get{ return score; }
    }
    private void Awake()
    {
        Instance = this;

        isGameActive = true;
    }

    public void SetScore()
    {
        AddScore();
        UpdateScore();
        CheckGameState();
    }
    private void CheckGameState()
    {
        if (isGameActive)
        {
            StartCoroutine(SpawnBoxCoroutine());
        }
    }
    private void AddScore()
    {
        score++;
    }

    private void UpdateScore()
    {
        textScore.text = score.ToString();
    }

    public void GameOver()
    {
        if (isGameActive)
        {
            isGameActive = false;
            AudioManager.Instance.PlaySound(gameOverSound);
            CameraManager.Instance.ResetCameraPosition();
            CheckHighScore();
            GameOverPanelChange();
        }
        
    }

    private void GameOverPanelChange()
    {
        panelGameplay.SetActive(false);
        textScore_GameOver.text = score.ToString();
        textTopScore_GameOver.text = PlayerPrefs.GetInt("TopScore").ToString();
        panelGameOver.SetActive(true);
    }

    private void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore",score);
            PlayFabManager.Instance.SendLeaderboard(score);
        }
    }
    
    private IEnumerator SpawnBoxCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        boxSpawner.SpawnBox();
    }
}
