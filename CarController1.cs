using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController1 : MonoBehaviour
{
    private float speed = 20f;
    private float rotationSpeed = 400f;
    [SerializeField]private Transform parkingspot; // The parking spot's position
    private float parkingTolerance = 1f; // How close the car needs to be to the parking spot to be considered parked
    private bool isParked = false; // Flag to check if the car is parked

    void Update()
    {
        if(!isParked)
        {
        // Move the car forward and backward
        float moveInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(Vector3.left * moveInput, Space.Self);  // Use Space.Self for local movement

        // Rotate the car

        float rotationInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime*(2*moveInput);
        transform.Rotate(Vector3.forward * -rotationInput);
        CheckParking();
        }
        else{
            Debug.Log("Parking Successful!");
            return;
        }
    }
    private void CheckParking()
    {
        if (parkingspot == null)
        {
            Debug.LogError("Parking spot is not assigned!");
            return;
        }

        // Calculate distance to the parking spot
        float distanceToSpot = Vector3.Distance(transform.position, parkingspot.position);

        // Check if the car is within the parking spot's tolerance
        if (distanceToSpot <= parkingTolerance)
        {
            isParked = true;
        }
    }
}
