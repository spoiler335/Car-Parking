using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private bool isAtDestination;
    private bool isAtStartingPoint;
    private bool isTravelling;

    [SerializeField] private Transform[] path;
    [SerializeField] private float time = 2f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform initialPosition;

    private void OnEnable()
    {

    }

    private void Start()
    {
        isAtDestination = false;
        isAtStartingPoint = true;
        isTravelling = false;

        transform.position = initialPosition.position;
    }

    private void OnMouseDown()
    {
        if (isTravelling) return;
        Debug.Log("Car clicked ");
        if (!DI.di.gameManager.areMovesAvailable)
        {
            Debug.Log("No Moves Available");
            EventsModel.NO_MOVES?.Invoke();
            return;
        }
        if (isAtStartingPoint && !isAtDestination) MoveForward();
        else if (!isAtStartingPoint && isAtDestination) MoveReverse();
    }

    private void MoveForward()
    {
        Debug.Log($"{gameObject.name} Moving Forward");
        isTravelling = true;
        iTween.MoveTo(gameObject, iTween.Hash(
            "path", path,
            "time", time,
            "easetype", iTween.EaseType.linear,
            "movetopath", true,
            "orienttopath", true,
            "scaletopath", true
        ));
        StartCoroutine(OnForwardPathCompleted());
    }

    private void MoveReverse()
    {
        Debug.Log($"{gameObject.name} Moving Reverse");
        isTravelling = true;
        Transform[] reverse = new Transform[path.Length];
        for (int i = 0; i < path.Length; ++i)
        {
            reverse[i] = path[path.Length - 1 - i];
        }
        Debug.Log("Reverse Path");
        iTween.MoveTo(gameObject, iTween.Hash(
            "path", reverse,
            "time", time,
            "easetype", iTween.EaseType.linear,
            "movetopath", true,
            "orienttopath", true,
            "scaletopath", true
        ));
        StartCoroutine(OnReversePathCompleted());
    }

    private IEnumerator OnForwardPathCompleted()
    {
        yield return new WaitForSeconds(time + 0.5f);
        Debug.Log("Destination Reached");
        isTravelling = false;
        isAtDestination = true;
        isAtStartingPoint = false;
        EventsModel.CAR_MOVED_FORWARD?.Invoke();
    }

    private IEnumerator OnReversePathCompleted()
    {
        yield return new WaitForSeconds(time + 0.5f);
        Debug.Log("Starting Point Reached");
        isTravelling = false;
        isAtStartingPoint = true;
        isAtDestination = false;
        EventsModel.CAR_MOVED_REVERSE?.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision :: {other.gameObject.tag}");
        if (other.gameObject.CompareTag("Car"))
        {
            Debug.Log("End the Game");
            EventsModel.CAR_CRASHED?.Invoke();
        }
    }


    private void OnDisable()
    {

    }
}