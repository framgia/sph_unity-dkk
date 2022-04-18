using UnityEngine;

public class Player : MonoBehaviour
{
    private new SpriteRenderer spriteRenderer;
    public new Sprite[] runSprites;
    public new Sprite climbSprite;
    public new Sprite jumpSprite;
    private new Rigidbody2D rigidbody;
    private int spriteIndex;
    private new Collider2D collider;
    private new Collider2D[] results;
    private Vector2 direction;
    private bool isGrounded;
    private bool isClimbing;
    public float moveSpeed = 1f;
    public float jumpStrength = 1f;

    private void CheckCollision() {
        isGrounded = false;
        isClimbing = false;

        Vector2 size = collider.bounds.size;
        size.y += .1f;
        size.x /= 2f;
        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        for (int i = 0; i < amount; i++) {
            GameObject hit = results[i].gameObject;

            if (hit.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = hit.transform.position.y < (transform.position.y - 0.5f);
                Physics2D.IgnoreCollision(collider, results[i], !isGrounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder")) {
                isClimbing = true;
            }
        }
    }

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results = new Collider2D[4];
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update() 
    {
        CheckCollision();

        if (isClimbing)
        {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        }
        else if (isGrounded && Input.GetButtonDown("Jump"))
        {
            direction = Vector2.up * jumpStrength;
        }
        else 
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        if (isGrounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }

        if (direction.x > 0f) 
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }

    private void AnimateSprite() 
    {
        if (isClimbing) 
        {
            spriteRenderer.sprite = climbSprite;
        }
        else if (!isGrounded)
        {
            spriteRenderer.sprite = jumpSprite;
        }
        else if (direction.x != 0f)
        {
            spriteIndex++;

            if (spriteIndex >= runSprites.Length)
            {
                spriteIndex = 0;
            }

            spriteRenderer.sprite = runSprites[spriteIndex];
        }
        else
        {
            spriteRenderer.sprite = runSprites[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Objective")) 
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            FindObjectOfType<GameManager>().LevelFailed();
        }
    }
}
