using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private bool isReachedDestination;


    private void OnEnable()
    {
        isReachedDestination = false;
        EventsModel.REACHED_DESTINATION -= OnReachedDestination;
        EventsModel.REACHED_DESTINATION += OnReachedDestination;
    }

    private void OnReachedDestination()
    {
        isReachedDestination = !isReachedDestination;
    }

    private void OnMouseDown()
    {
        Debug.Log("Car clicked " + isReachedDestination);
        if (isReachedDestination)
        {
            isReachedDestination = false;
            EventsModel.START_REVERSE_MOVEMENT?.Invoke(gameObject);
        }
        else EventsModel.START_MOVEMENT?.Invoke(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Car")
        {
            Debug.Log("End the Game");
            EventsModel.CAR_CRASHED?.Invoke(gameObject);
        }
    }

    private void OnDisable()
    {
        EventsModel.REACHED_DESTINATION -= OnReachedDestination;
    }
}