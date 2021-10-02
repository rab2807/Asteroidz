using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float targetTime = -1;
    private bool isStarted;
    private bool isFinished;
    private Action func;

    public Timer()
    {
    }

    public Timer(float targetTime)
    {
        this.targetTime = targetTime;
    }

    public float TargetTime
    {
        get => targetTime;
        set => targetTime = value;
    }

    public void ScheduleTask(float targetTime, Action func)
    {
        if (targetTime < 0) Debug.Log("invalid time");
        
        this.targetTime = targetTime;
        ScheduleTask(func);
    }

    public void ScheduleTask(Action func)
    {
        this.func = func;
        StartTimer();
    }

    private void StartTimer()
    {
        if (!isStarted) StartCoroutine(SpendTime());
    }

    private IEnumerator SpendTime()
    {
        isStarted = true;
        // isFinished = false;
        yield return new WaitForSeconds(targetTime);
        // isFinished = true;
        isStarted = false;
        func();
    }
}