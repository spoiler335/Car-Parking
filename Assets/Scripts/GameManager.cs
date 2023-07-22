using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        DI.di.SetGameManager(this);
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level-1");
    }
}
