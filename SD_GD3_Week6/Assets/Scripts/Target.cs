using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody2D targetRb; // gets the Rigid body attached to object
    public float minSpeed; // the slowest the object can move
    public float maxSpeed; // the fastest the object can move
    public float maxTorque; // how much spin it has
    public float xRange; // defines the range for spawning the object on the x axis
    public float ySpawnPos; // the same for the y axis
    private GameManager gameManager; // this is getting a reference to the game manager script
    public int pointValue; // the score value that will increase when the target is clicked

    void Start()
    {
        targetRb = GetComponent<Rigidbody2D>(); // this is getting the Rigid body component attached to the target this is used to apply physics to the objects
        targetRb.AddForce(RandomForce(), ForceMode2D.Impulse); // applies a random force to the target to it movement
        targetRb.AddTorque(RandomTorque(), ForceMode2D.Impulse); // applies a random spin to the target
        transform.position = RandomSpawnPos(); // sets the spawn position of the target

        
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // this is finding the game manager object in the scene

        // Check if GameManager was found
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    Vector2 RandomForce() //this generate a random force vector within the min and max speed this applies the force upwards
    {
        return Vector2.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque() //this generate a random spin to the target
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector2 RandomSpawnPos() // spawns the target in random place
    {
        return new Vector2(Random.Range(-xRange, xRange), ySpawnPos);
    }

    // Detect mouse click on object
    private void OnMouseDown()
    {
        Destroy(gameObject);

        // Increase score if not a bad target
        gameManager.UpdateScore(pointValue);

        // Check for "BadTarget" tag
        if (gameObject.CompareTag("BadTarget"))
        {
            Debug.Log("Bad target clicked! Decreasing lives...");
            gameManager.DecreaseLives();  // Decrease lives for bad target
        }
    }

    // Detect when bug hits a sensor or another collider (trigger detection)
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Sensor"))  // Assuming you have a sensor below the screen
        {
            Debug.Log("Bug hit the sensor! Decreasing lives...");
            gameManager.DecreaseLives();  // Decrease lives if bug hits sensor
            Destroy(gameObject);  // Destroy the bug when it hits the sensor
        }
    }

}