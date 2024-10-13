using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;
    private CharacterController characterController;

    // Reference to GameSceneScript
    private GameSceneScript gameSceneScript;

    // Reference to animator component
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Get reference to GameSceneScript (assuming it's on a GameObject in the scene)
        gameSceneScript = FindObjectOfType<GameSceneScript>();
        // Refernce to animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Vector3(x,y,z)
        // 0 means the character cannot move up and down
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        // Make the speed of diagonal movement the same as horizontal and vertical movement
        movementDirection.Normalize();

        // Start moving character
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        characterController.SimpleMove(movementDirection * magnitude);

        // Rotating the character according to the direction of movement
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            transform.forward = movementDirection;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    // This function is called when the character controller collides with another object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit by the player is the cube (assuming the cube has the tag "Obstacle")
        if (other.gameObject.name == "FinalPoint")
        {
            // Stop the game
            gameSceneScript.GameOver();
            // Calculate best score
            if (PlayerPrefs.GetFloat("Best Run Time") < PlayerPrefs.GetFloat("Current Score"))
            {
                PlayerPrefs.SetFloat("Best Run Time", PlayerPrefs.GetFloat("Current Score"));
            }
        }
    }

}
