using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Timer
{
    public event Action OnTimerFinished;
    private float startTime;
    private float duration;
    private float endTime;
    private bool isActive;

    public Timer(float duration)
    {
        this.duration = duration;

    }
    public void StartTimer()
    {
        startTime = Time.time;
        endTime = startTime + duration;
        isActive = true;
    }
    public void StopTimer()
    {
        isActive = false;
    }
    public void Tick()
    {
        if (!isActive || Time.time < endTime) return;
        OnTimerFinished?.Invoke();
        StopTimer();
    }
}