using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Bird : Agent
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private PipeHandler pipeHandler = null;
    [SerializeField] private Transform birdTransform = null;

    [Header("Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxVelocity = 5f;

    private Vector2 startPos;

    public override void Initialize()
    {
        startPos = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        pipeHandler.ResetPipes();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(0.1f);
        if (Mathf.FloorToInt(vectorAction[0]) != 1)
        {
            return;
        }
        jump();
    }

    private void jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if(!Input.GetKey(KeyCode.Space)) { return; }
        actionsOut[0] = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddReward(-1.0f);
        EndEpisode();
    }

    private void Update()
    {
        birdTransform.rotation = Quaternion.LookRotation(rb.velocity + new Vector2(10f, 0), transform.up);
    }
}
