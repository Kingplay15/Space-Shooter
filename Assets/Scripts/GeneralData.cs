using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralData : MonoBehaviour
{
    public static Vector2 minBound;
    public static Vector2 maxBound;

    private void Start()
    {
        InitBounds();
    }

    private void InitBounds()
    {
        Camera mainCam = Camera.main;
        minBound = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
