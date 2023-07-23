using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int requiredMoves;

    private void OnEnable()
    {
        EventsModel.CAR_MOVED_FORWARD -= OnCarForward;
        EventsModel.CAR_MOVED_FORWARD += OnCarForward;

        EventsModel.CAR_MOVED_REVERSE -= OnCarReverse;
        EventsModel.CAR_MOVED_REVERSE += OnCarReverse;

        EventsModel.CAR_CRASHED -= OnCarCrashed;
        EventsModel.CAR_CRASHED += OnCarCrashed;
    }

    private void Start()
    {

    }

    private void OnCarForward()
    {
        --requiredMoves;
        if (requiredMoves == 0)
        {
            Debug.Log("Level Completed");
            EventsModel.LEVEL_COMPLETED?.Invoke();
        }
    }

    private void OnCarReverse()
    {
        ++requiredMoves;
    }


    private void OnCarCrashed()
    {
        Debug.Log("Level Failed");
        iTween.Stop();
        EventsModel.NO_MOVES?.Invoke();
    }

    private void OnDisable()
    {
        EventsModel.CAR_MOVED_FORWARD -= OnCarForward;
        EventsModel.CAR_MOVED_REVERSE -= OnCarReverse;
        EventsModel.CAR_CRASHED -= OnCarCrashed;
    }
}
