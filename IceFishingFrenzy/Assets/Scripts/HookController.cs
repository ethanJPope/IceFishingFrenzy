using UnityEngine;

public class HookController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float dropSpeed = 5f;
    [SerializeField] private float reelSpeed = 3f;
    [SerializeField] private float horizontalSpeed = 4f;

    [Header("Depth Settings")]
    [SerializeField] private float maxDepth = -20f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryStartCast();
        }
    }

    private void TryStartCast()
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        if (GameStateManager.Instance.IsState(GameState.Idle))
        {
            GameStateManager.Instance.SetState(GameState.Dropping);
        }
    }

    private void HandleMovement()
    {
        if (GameStateManager.Instance == null)
        {
            return;
        }

        if (GameStateManager.Instance.IsState(GameState.Dropping))
        {
            HandleDropMovement();
        }
        else if (GameStateManager.Instance.IsState(GameState.Reeling))
        {
            HandleReelMovement();
        }
    }

    private void HandleDropMovement()
    {
        Vector3 position = transform.position;

        position.y -= dropSpeed * Time.deltaTime;
        transform.position = position;

        if (position.y <= maxDepth)
        {
            GameStateManager.Instance.SetState(GameState.Reeling);
        }
    }

    private void HandleReelMovement()
    {
        Vector3 position = transform.position;

        position.y += reelSpeed * Time.deltaTime;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) < 0.4f)
        {
            horizontalInput = 0f;
        }

        position.x += horizontalInput * horizontalSpeed * Time.deltaTime;

        transform.position = position;

        if (position.y >= startPosition.y)
        {
            transform.position = startPosition;
            GameStateManager.Instance.SetState(GameState.Scoring);
        }
    }
}
