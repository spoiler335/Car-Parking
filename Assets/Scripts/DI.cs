using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DI : MonoBehaviour
{
    public static DI di { get; private set; }

    public GameManager gameManager { get; private set; }

    private void Awake()
    {
        if (di != null)
        {
            Destroy(gameObject);
        }
        di = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}

