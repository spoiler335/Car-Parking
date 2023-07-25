using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI movesCounter;

    [SerializeField] private TextMeshProUGUI retryText;

    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button nextLevelButton;

    [SerializeField] private GameObject ftueScreen;

    private void OnEnable()
    {
        EventsModel.CAR_MOVED_FORWARD -= UpdateMoves;
        EventsModel.CAR_MOVED_FORWARD += UpdateMoves;

        EventsModel.NO_MOVES -= GameEndOnNoMoves;
        EventsModel.NO_MOVES += GameEndOnNoMoves;

        EventsModel.LEVEL_COMPLETED -= OnLevelComplete;
        EventsModel.LEVEL_COMPLETED += OnLevelComplete;

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(OnExitButtonClicked);

        nextLevelButton.onClick.RemoveAllListeners();
        nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
    }

    private void Start()
    {
        StartCoroutine(ShowFTUE());
        levelText.text = $"Level : {DI.di.gameManager.currentLevel}";
        movesCounter.text = $"{DI.di.gameManager.moves[DI.di.gameManager.currentLevel - 1]} Moves";
        gameObject.GetComponent<Image>().color = new Color32(255, 127, 132, 0);
        retryButton.gameObject.SetActive(false);
        retryText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

    }

    private IEnumerator ShowFTUE()
    {
        ftueScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        ftueScreen.SetActive(false);
    }

    private void UpdateMoves()
    {
        movesCounter.text = $"{--DI.di.gameManager.moves[DI.di.gameManager.currentLevel - 1]} Moves";
    }

    private void GameEndOnNoMoves()
    {
        Debug.Log("No Moves Available");
        Time.timeScale = 0;
        gameObject.GetComponent<Image>().color = new Color32(255, 127, 132, 255);
        retryText.text = "Level Failed Please Retry";
        retryText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    private void OnLevelComplete()
    {
        Debug.Log("UIManager ::Level Completed");
        Time.timeScale = 0;
        gameObject.GetComponent<Image>().color = new Color32(255, 127, 132, 255);
        if (DI.di.gameManager.currentLevel <= 3) retryText.text = "Level Cleared";
        else retryText.text = "Game Cleared, other levels are in Development";
        retryText.gameObject.SetActive(true);
        if (DI.di.gameManager.currentLevel <= 3) nextLevelButton.gameObject.SetActive(true);
        else exitButton.gameObject.SetActive(true);
    }

    private void OnRetryButtonClicked()
    {
        EventsModel.RETRY_LEVEL?.Invoke();
    }

    private void OnNextLevelButtonClicked()
    {
        EventsModel.LOAD_NEXT_LEVEL?.Invoke();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        EventsModel.CAR_MOVED_FORWARD -= UpdateMoves;
        EventsModel.NO_MOVES -= GameEndOnNoMoves;
        EventsModel.LEVEL_COMPLETED -= OnLevelComplete;
    }

}
