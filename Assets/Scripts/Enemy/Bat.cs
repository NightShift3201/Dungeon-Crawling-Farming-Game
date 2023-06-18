using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float swoopSpeed = 5f;
    public float swoopDuration = 1.5f;
    public float swoopCooldown = 3f;
    public float minDistanceFromPlayer = 5f;

    private Transform playerTransform;
    private Vector3 initialPosition;
    private bool isSwooping = false;
    private bool canSwoop = true;

    private void Awake()
    {
        // Assuming the player has a tag "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isSwooping)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceFromPlayer > minDistanceFromPlayer)
            {
                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
            }
            else
            {
                // Stay at a certain distance from the player
                Vector3 targetPosition = (transform.position - playerTransform.position).normalized * minDistanceFromPlayer + playerTransform.position;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            }

            if (canSwoop && distanceFromPlayer <= minDistanceFromPlayer)
            {
                // Start swooping towards the player
                StartCoroutine(Swoop());
            }
        }
        else
        {
            // Fly around the dungeon
            FlyAround();
        }
    }

    private void FlyAround()
    {
        // Implement your own flying movement logic here
        // For example, you can make the bat move in a circular pattern, randomly change direction, or follow a predefined path.
        // Here's a simple example of a circular movement:

        float time = Time.time;
        float angle = time * movementSpeed;

        float x = Mathf.Cos(angle) * movementSpeed;
        float y = Mathf.Sin(angle) * movementSpeed;

        Vector3 newPosition = initialPosition + new Vector3(x, y, 0f);
        transform.position = newPosition;
    }

    private IEnumerator Swoop()
    {
        isSwooping = true;
        canSwoop = false;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = playerTransform.position;

        float elapsedTime = 0f;

        while (elapsedTime < swoopDuration)
        {
            elapsedTime += Time.deltaTime;

            // Move towards the player with increased speed during the swoop
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / swoopDuration) * swoopSpeed * Time.deltaTime;

            yield return null;
        }

        // Reset back to initial position
        transform.position = initialPosition;
        isSwooping = false;

        // Cooldown before the next swoop
        yield return new WaitForSeconds(swoopCooldown);
        canSwoop = true;
    }
}
