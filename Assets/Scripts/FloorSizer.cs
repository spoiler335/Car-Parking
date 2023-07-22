using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSizer : MonoBehaviour
{
    [SerializeField] private float floorScaleMultiplier = 1.0f;

    void Start()
    {
        ResizeFloorToFitScreen();
    }

    void ResizeFloorToFitScreen()
    {
        // Get the screen dimensions
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;

        // Calculate the aspect ratio of the screen
        float aspectRatio = screenWidth / screenHeight;

        // Get the current scale of the floor
        Vector3 refrenceScale = new Vector3(100, 1, 100);

        // Set the new scale of the floor based on the screen aspect ratio and the multiplier
        transform.localScale = new Vector3(refrenceScale.x * aspectRatio * floorScaleMultiplier, refrenceScale.y, refrenceScale.z);
    }
}


