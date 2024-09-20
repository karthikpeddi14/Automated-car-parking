using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class testingh : Agent
{
    [SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(-39.1f ,-8f,0f);
        transform.rotation = Quaternion.Euler(0f,0f,180f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float forward = actions.DiscreteActions[0];
        float backward = actions.DiscreteActions[1];
        float clockrotate = actions.DiscreteActions[2];
        float anticlockrotate = actions.DiscreteActions[3];

        float speed = 10f;
        float rotationSpeed = 200f;

        float moveInput = (forward-backward) * speed * Time.deltaTime;
        transform.Translate(Vector3.left * moveInput, Space.Self);

        float rotationInput = (clockrotate-anticlockrotate) * rotationSpeed * Time.deltaTime*(2*moveInput);
        transform.Rotate(Vector3.forward * -rotationInput);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        if(Input.GetAxisRaw("Vertical")>0) discreteActions[0] = 1;
        else discreteActions[0] = 0;

        if(Input.GetAxisRaw("Vertical")<0) discreteActions[1] = 1;  
        else discreteActions[1] = 0;

        if(Input.GetAxisRaw("Horizontal")>0) discreteActions[2] = 1;
        else discreteActions[2] = 0;

        if(Input.GetAxisRaw("Horizontal")<0) discreteActions[3] = 1;
        else discreteActions[3] = 0;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("collided");
        if(other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(1f);
            EndEpisode();
        }

        if(other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }
        
    }
}
