using UnityEngine;
using UnityEngine.Events;

public static partial class EventsModel
{
    public static UnityAction<GameObject> START_MOVEMENT;
    public static UnityAction<GameObject> START_REVERSE_MOVEMENT;
    public static UnityAction REACHED_DESTINATION;
    public static UnityAction<GameObject> CAR_CRASHED;
}