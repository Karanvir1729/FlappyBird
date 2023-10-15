using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdController : MonoBehaviour
{
    public float flapForce = 200f;
    public float horizontalSpeed = 2f;
    public float rotationMultiplier = 2f;
    public UnityEvent onGameOver;

    private Rigidbody2D rb2d;
    private bool hasStarted;

    private bool isGameOver;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hasStarted = false;
        rb2d.isKinematic = true;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver) return;

        if (!hasStarted && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
        {
            hasStarted = true;
            rb2d.isKinematic = false;
        }

        if (hasStarted && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(0, flapForce));
        }
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        if (hasStarted)
        {
            rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
            UpdateRotation();
        }
    }

    void UpdateRotation()
    {
        float rotationZ = rb2d.velocity.y * rotationMultiplier;
        rotationZ = Mathf.Clamp(rotationZ, -90f, 45f);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    public GameOverPanelController gameOverPanelController;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted && !isGameOver)
        {
            isGameOver = true;
            onGameOver.Invoke();
            gameOverPanelController.Show();
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool HasStarted()
    {
        return hasStarted;
    }
}
