using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D boxRigidbody2d;
    [SerializeField]
    private bool hasCollided;
    

    private Transform boxSpawner;

    private Vector2 pointA;
    private Vector2 pointB;

    public float horizontalSpeed = 3f;

    private bool movingRight;

    public Sound[] dropSounds;
    public Sound[] hitSounds;

    void Start()
    {
        boxRigidbody2d = GetComponent<Rigidbody2D>();
        boxSpawner = CameraManager.Instance.boxSpawner;

        pointA = new Vector2(-2,boxSpawner.position.y);
        pointB = new Vector2(2, boxSpawner.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && (collision.collider.CompareTag(GameTags.Box) || collision.collider.CompareTag(GameTags.Ground)))
        {
            hasCollided = true;
            
            CheckShake();
            AudioManager.Instance.PlaySound(hitSounds[Random.Range(0, hitSounds.Length)]);
            CameraManager.Instance.UpdateBoxesHeight(transform.position.y);

            /* if (GameManager.Instance.hasCollidedWithGround && collision.collider.CompareTag(GameTags.Ground))
             {
                 GameManager.Instance.GameOver();
             }
             else
             {
                 if (collision.collider.CompareTag(GameTags.Ground))
                 {
                     GameManager.Instance.hasCollidedWithGround = true;
                 }
                 GameManager.Instance.SetScore();
             }
             */
            GameManager.Instance.SetScore();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTags.GameOverZone))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void Move()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB, horizontalSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA, horizontalSpeed * Time.deltaTime);
        }

        if (transform.position.x>=pointB.x)
        {
            movingRight = false;
        }
        else if(transform.position.x<=pointA.x)
        {
            movingRight = true;
        }
    }
    private void CheckShake()
    {
        if (GameManager.Instance.Score == 0)
        {
            CameraManager.Instance.Shake();
        }
        else
        {
            int shakeRandom = Random.Range(0, 10);

            if (shakeRandom > 7)
            {
                CameraManager.Instance.Shake();
            }
        }
        
    }

    private void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            if (boxRigidbody2d.gravityScale == 0f)
            {
                Move();
            }

            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySound(dropSounds[Random.Range(0, dropSounds.Length)]);
                boxRigidbody2d.gravityScale = 1f;
            }
        }
        else
        {
            gameObject.SetActive(false);
            
        }
    }
}
