using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    public float duration = 10f; // Countdown duration in seconds
    public UnityEvent onTimerEnd; // Event triggered when timer ends

    private float timeRemaining;
    private bool isRunning = false;

    private void Start()
    {
        StartTimer(duration);
    }

    public void StartTimer(float seconds)
    {
        timeRemaining = seconds;
        isRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        isRunning = false;
        onTimerEnd?.Invoke(); // Invoke the UnityEvent callback
    }

    public void StopTimer()
    {
        isRunning = false;
        StopAllCoroutines();
    }
}
