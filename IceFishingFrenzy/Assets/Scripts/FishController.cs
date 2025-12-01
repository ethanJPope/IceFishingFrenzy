using UnityEngine;

public class FishController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private SpriteRenderer fishSpriteRenderer;
    [SerializeField] private Sprite fishSprite1;
    [SerializeField] private Sprite fishSprite2;
    [SerializeField] private Sprite fishSprite3;
    [SerializeField] private Sprite fishSprite4;

    [Header("Value")]
    [SerializeField] private int baseValue = 5;
    public int CurrentValue { get; private set; }

    [Header("Swim Settings")]
    [SerializeField] private float moveSpeed = 1.5f;
    //[SerializeField] private float swimRange = 2f;

    [Header("Gloabl Swim Bounds")]
    [SerializeField] private float leftBoundX = -8f;
    [SerializeField] private float rightBoundX = 8f;

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
        fishSpriteRenderer = GetComponent<SpriteRenderer>();
        startX = transform.position.x;
        baseY = transform.position.y;

        SetSprite();

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
        SetSprite();
    }

    private void SetSprite()
    {
        float currentY = transform.position.y;
        if (currentY >= -10)
        {
            fishSpriteRenderer.sprite = fishSprite1;
        }
        else if (currentY >= -20)
        {
            fishSpriteRenderer.sprite = fishSprite2;
        }
        else if (currentY >= -30)
        {
            fishSpriteRenderer.sprite = fishSprite3;
        }
        else
        {
            fishSpriteRenderer.sprite = fishSprite4;
        }
    }

    private void HandleSwim()
    {
        Vector3 position = transform.position;
        position.x += swimDirection * moveSpeed * Time.deltaTime;

        if (position.x <= leftBoundX)
        {
            position.x = leftBoundX;
            swimDirection = 1f;
            FlipSprite();
        }
        else if (position.x >= rightBoundX)
        {
            position.x = rightBoundX;
            swimDirection = -1f;
            FlipSprite();
        }

        transform.position = position;
    }

    
    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * swimDirection;
        transform.localScale = scale;
    }

    private void HandleBob()
    {
        Vector3 position = transform.position;
        float bob = Mathf.Sin(Time.time * bobFrequency + bobOffset) * bobAmplitude;
        position.y = baseY + bob;

        transform.position = position;
    }

    public void SetValueMultiplier(float multiplier)
    {
        if (multiplier < 0f)
        {
            multiplier = 0f;
        }

        float newValue = baseValue * multiplier;
        CurrentValue = Mathf.RoundToInt(newValue);
    }

    public void AttachToHook(Transform hookTransform)
    {
        isCaught = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }
        transform.SetParent(hookTransform);
    }
}
