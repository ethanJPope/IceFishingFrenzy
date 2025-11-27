using UnityEngine;

public class FishController : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private int baseValue = 5;
    public int CurrentValue { get; private set; }

    [Header("Swim Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float swimRange = 2f;

    [Header("Bob Settings")]
    [SerializeField] private float bobAmplitude = 0.1f;
    [SerializeField] private float bobFrequency = 2f;

    private bool isCaught = false;

    private float startX;
    private float baseY;
    private float swimDirection = 1f;
    private float bobOffset;

    private void Start()
    {
        startX = transform.position.x;
        baseY = transform.position.y;
        CurrentValue = baseValue;

        bobOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        if (isCaught)
        {
            return;
        }

        HandleSwim();
        HandleBob();
    }

    private void HandleSwim()
    {
        Vector3 position = transform.position;
        position.x += swimDirection * moveSpeed * Time.deltaTime;
        transform.position = position;
        float distanceFromStart = Mathf.Abs(position.x - startX);

        if (distanceFromStart >= swimRange)
        {
            swimDirection *= -1f;
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * swimDirection;
            transform.localScale = localScale;
        }
    }

    private void HandleBob()
    {
        Vector3 position = transform.position;
        float bob = Mathf.Sin(Time.time * bobFrequency + bobOffset) * bobAmplitude;
        position.y = baseY + bob;
        
        transform.position = position;
    }
}
