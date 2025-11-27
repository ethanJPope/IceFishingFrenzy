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

    private void HandleInput() {
        if (Input.GetKey(KeyCode.Space)) {
            TryStartCast();
        }
    }

    private void TryStartCast() {
        if (GameStateManager.Instance == null) {
            return;
        }

        if (GameStateManager.Instance.IsState(GameState.Idle)) {
            GameStateManager.Instance.SetState(GameState.Dropping);
        }
    }

    private void HandleMovement()
    {
        if (GameStateManager.Instance == null) {
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

    private void HandleDropMovement() {
        Vector3 postion = transform.position;

        postion.y -= dropSpeed * Time.deltaTime;
        transform.position = postion;

        if (postion.y <= maxDepth) {
            GameStateManager.Instance.SetState(GameState.Reeling);
        }
    }

    private void HandleReelMovement() {
        Vector3 postion = transform.position;

        postion.y += reelSpeed * Time.deltaTime;

        float horizontalInput = Input.GetAxis("Horizontal");
        postion.x += horizontalInput * horizontalSpeed * Time.deltaTime;
        transform.position = postion;

        if (postion.y >= startPosition.y) {
            transform.position = startPosition;
            GameStateManager.Instance.SetState(GameState.Idle);
        }
    }
}
