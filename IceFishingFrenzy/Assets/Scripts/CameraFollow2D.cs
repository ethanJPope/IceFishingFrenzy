using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float followSmoothTime = 0.15f;

    private Vector3 surfacePosition;
    private Vector3 followVelocity;

    void Start()
    {
        surfacePosition = transform.position;
    }

    void Update()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        if (target == null)
        {
            return;
        }

        if (GameStateManager.Instance == null)
        {
            return;
        }

        if (GameStateManager.Instance.IsState(GameState.Dropping) || GameStateManager.Instance.IsState(GameState.Reeling))
        {
            FollowTargetVertically();
        }
        else
        {
            ReturnToSurface();
        }
    }

    private void FollowTargetVertically()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentPosition;

        targetPosition.y = target.position.y + verticalOffset;

        transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref followVelocity, followSmoothTime);
    }

    private void ReturnToSurface()
    {
        Vector3 currentPosition = transform.position;

        transform.position = Vector3.SmoothDamp(currentPosition, surfacePosition, ref followVelocity, followSmoothTime);
    }
}
