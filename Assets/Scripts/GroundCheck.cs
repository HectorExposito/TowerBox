using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.hasCollidedWithGround)
        {
            GameManager.Instance.hasCollidedWithGround = true;
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
}
