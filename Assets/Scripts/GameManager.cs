using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int[] moves;
    public int currentLevel { get; private set; }

    public bool areMovesAvailable => moves[currentLevel - 1] > 0;

    private void Start()
    {
        DI.di.SetGameManager(this);
        moves = new int[] { 4, 6, 8 };
        currentLevel = PlayerPrefs.GetInt(PPF_KEYS.PPF_LEVEL_KEY, 1);
        StartCoroutine(LoadLevel());
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
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene($"Level-{currentLevel}");
    }

    private void OnLevelCompleted()
    {
        Debug.Log("Level Completed");
        ++currentLevel;
        PlayerPrefs.SetInt(PPF_KEYS.PPF_LEVEL_KEY, currentLevel);
    }

    private void RetryLevel()
    {
        Debug.Log("Retry Level");
        moves = new int[] { 4, 6, 8 };
        StartCoroutine(LoadLevel());
    }

    private void OnDisable()
    {
        EventsModel.LEVEL_COMPLETED -= OnLevelCompleted;
        EventsModel.LOAD_NEXT_LEVEL -= LoadNextLevel;
        EventsModel.RETRY_LEVEL -= RetryLevel;
    }
}
