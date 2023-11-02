using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MRU : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float[] timesToReachPoints; // Tiempo en segundos para llegar a cada punto
    public float speed = 2; // Velocidad en unidades por segundo
    private int currentPointIndex = 0;
    private float journeyStartTime;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        if (patrolPoints.Length < 2 || patrolPoints.Length != timesToReachPoints.Length)
        {
            Debug.LogError("Asegúrate de tener al menos dos puntos de patrulla y que los tiempos coincidan.");
            enabled = false;
            return;
        }

        startPosition = transform.position;
        endPosition = patrolPoints[currentPointIndex].position;
        speed = Vector3.Distance(startPosition, endPosition) / timesToReachPoints[currentPointIndex];
        journeyStartTime = Time.time;
    }

    void Update()
    {
        float journeyDuration = timesToReachPoints[currentPointIndex];
        float elapsed_time = Time.time - journeyStartTime;

        // Calcula la nueva posición con MRU
        if (elapsed_time <= journeyDuration)
        {
            transform.position = startPosition + (endPosition - startPosition) * (elapsed_time / journeyDuration);
        }
        else
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            startPosition = transform.position;
            endPosition = patrolPoints[currentPointIndex].position;
            speed = Vector3.Distance(startPosition, endPosition) / timesToReachPoints[currentPointIndex];
            journeyStartTime = Time.time;
        }
    }
}
