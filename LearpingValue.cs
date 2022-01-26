using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearpingValue : MonoBehaviour
{

    float start = 5.2f; // Could be any number.
    float end = 0;  // Could be any number too.

    float totalTime = 2; // The time it takes to transit from start to target.
    float t; // The variable holding the current time passed.



    void Update()
    {
        t += Time.deltaTime;

        float T = t / totalTime; // The percentage of our "progress" towards totalTime.
        float currentFloat = Mathf.Lerp(start, end, T);
        //Debug.Log(currentFloat);
    }

}
