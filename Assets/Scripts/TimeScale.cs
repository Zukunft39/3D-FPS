using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public float timeScale;
    void Update()
    {
        Time.timeScale = timeScale;
    }
}
