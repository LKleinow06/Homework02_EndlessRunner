using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Cleanup")]
    public float destroyPosition = -15f;

    void Update()
    {
        //move the obstacle towards the player to imitate endlessness
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        //destroy the obstacle once it leaves screen
        if (transform.position.x < destroyPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();

        if (player != null)
        {
            Debug.Log("Player hit an obstacle");

            GameManager.Instance.GameOver();
        }
    }
}