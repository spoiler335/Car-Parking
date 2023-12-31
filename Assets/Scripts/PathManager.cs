using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class PathManager : MonoBehaviour
{
    [SerializeField] Transform[] path1;
    [SerializeField] private float speed = 5f;
    private int currentWayPointIndex = 0;
    private void OnEnable()
    {

    }

    private void MoveReverse(GameObject car)
    {
        Transform[] reverse = new Transform[path1.Length];
        for (int i = 0; i < path1.Length; ++i)
        {
            reverse[i] = path1[path1.Length - 1 - i];
        }
        OnPathStart();
        Debug.Log("Reverse Path");
        car.transform.rotation = Quaternion.Euler(0, 180, 0);
        iTween.MoveTo(car, iTween.Hash(
            "path", reverse,
            "time", 2f,
            "easetype", iTween.EaseType.linear,
            "movetopath", true,
            "orienttopath", true
        ));
    }

    private void MoveCar(GameObject car)
    {
        OnPathStart();
        car.transform.rotation = Quaternion.Euler(0, 0, 0);
        iTween.MoveTo(car, iTween.Hash(
            "path", path1,
            "time", 2f,
            "easetype", iTween.EaseType.easeInOutSine,
            "movetopath", true,
            "orienttopath", true
        ));
        StartCoroutine(OnPathCompleted());
    }

    private IEnumerator ForwardPath(GameObject car)
    {
        OnPathStart();
        while (currentWayPointIndex < path1.Length)
        {
            Vector3 targetPos = path1[currentWayPointIndex].position;
            Vector3 startPosition = car.transform.position;
            float distance = Vector3.Distance(startPosition, targetPos);
            float startTime = Time.time;
            while (car.transform.position != targetPos)
            {
                float journeyLen = Vector3.Distance(startPosition, targetPos);
                float fractionOfJourney = (Time.time - startTime) * speed / journeyLen;
                car.transform.position = Vector3.Lerp(startPosition, targetPos, fractionOfJourney);
                yield return null;
            }
            ++currentWayPointIndex;
        }
        OnPathCompleted();
    }

    public void OnPathStart()
    {
        Debug.Log("Path Started");
    }

    private IEnumerator OnPathCompleted()
    {
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Destination Reached");

    }



    private void OnDisable()
    {

    }
}
