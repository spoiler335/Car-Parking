using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int[] moves;
    public int currentLevel { get; private set; }

    public bool areMovesAvailable => moves[currentLevel - 1] > 0;

    [SerializeField] private Slider progressSlider;

    [SerializeField] private GameObject gameProgressIndicator;

    private void Start()
    {
        DI.di.SetGameManager(this);
        moves = new int[] { 4, 6, 8 };
        currentLevel = PlayerPrefs.GetInt(PPF_KEYS.PPF_LEVEL_KEY, 1);
        StartCoroutine(LoadLevelInit());
    }

    private void OnEnable()
    {
        EventsModel.LEVEL_COMPLETED -= OnLevelCompleted;
        EventsModel.LEVEL_COMPLETED += OnLevelCompleted;

        EventsModel.LOAD_NEXT_LEVEL -= LoadNextLevel;
        EventsModel.LOAD_NEXT_LEVEL += LoadNextLevel;

        EventsModel.RETRY_LEVEL -= RetryLevel;
        EventsModel.RETRY_LEVEL += RetryLevel;
    }

    private void LoadNextLevel()
    {
        Debug.Log("Load Next Level");
        LoadLevel();
    }

    private void LoadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync($"Level-{currentLevel}");
    }

    private void OnLevelCompleted()
    {
        Debug.Log("GameManager :: Level Completed");
        ++currentLevel;
        PlayerPrefs.SetInt(PPF_KEYS.PPF_LEVEL_KEY, currentLevel);
    }

    private void RetryLevel()
    {
        Debug.Log("GameManager :: Retry Level");
        moves = new int[] { 4, 6, 8 };
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        LoadLevel();
    }

    private IEnumerator LoadLevelInit()
    {
        if (currentLevel >= 4)
        {
            gameProgressIndicator.SetActive(true);
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        progressSlider.value = 0;
        progressSlider.maxValue = 1;
        while (progressSlider.value < 1)
        {
            progressSlider.value += 0.001f;
            yield return null;
        }
        LoadLevel();
    }

    private void OnDisable()
    {
        EventsModel.LEVEL_COMPLETED -= OnLevelCompleted;
        EventsModel.LOAD_NEXT_LEVEL -= LoadNextLevel;
        EventsModel.RETRY_LEVEL -= RetryLevel;
    }
}
