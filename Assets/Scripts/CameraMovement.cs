using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 3.80f, -5f);
    private float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    [SerializeField] private float wallDetectionRange = 1f; // Adjust this as needed

    private bool isNearWall;

    void Update()
    {
        // Check if the player is near any wall
        isNearWall = IsNearWall();

        if (!isNearWall)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    private bool IsNearWall()
    {
        float distanceToLeftWall = Mathf.Abs(target.position.x - leftWall.position.x);
        float distanceToRightWall = Mathf.Abs(target.position.x - rightWall.position.x);

        return distanceToLeftWall < wallDetectionRange || distanceToRightWall < wallDetectionRange;
    }
}
