using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MRUV : MonoBehaviour
{
    public Transform target1;
    public Transform target2;
    public float initialSpeed = 5f;
    public float acceleration = 2f;

    private Rigidbody2D rb;
    private Transform currentTarget;

    private float currentSpeed;
    private Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = initialSpeed;
        currentTarget = target1;
    }

    private void Update()
    {
        // Cambia de objetivo cuando el personaje llega a uno de los objetivos
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == target1) ? target2 : target1;
        }

        // Calcula la dirección hacia el objetivo actual
        Vector2 direction = (currentTarget.position - transform.position).normalized;

        // Calcula la aceleración
        float accelerationMagnitude = acceleration;

        // Aplica la fórmula de MRUV para calcular la velocidad
        currentSpeed = Mathf.Sqrt(initialSpeed * initialSpeed + 2 * accelerationMagnitude * Vector2.Distance(transform.position, currentTarget.position));

        // Calcula la velocidad con la dirección y la velocidad actual
        velocity = direction * currentSpeed;
    }

    private void FixedUpdate()
    {
        // Aplica la velocidad al Rigidbody2D
        rb.velocity = velocity;
    }
}
