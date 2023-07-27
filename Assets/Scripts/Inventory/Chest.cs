using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1 represents the right mouse button
        {
            // Check if the right-click occurred over this GameObject
            if (IsRightClickOverSelf())
            {
                // Right-click occurred on this GameObject
                // Call your desired function here
                OpenChest();
            }
        }
    }

    private bool IsRightClickOverSelf()
    {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform a raycast and check if it hits this GameObject
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit && hit.collider.gameObject == gameObject)
        {
            return true;
        }

        return false;
    }

    private void OpenChest()
    {
        // Perform your desired actions when right-clicking on this GameObject
        Debug.Log("Right-clicked on this GameObject!");
    }
}