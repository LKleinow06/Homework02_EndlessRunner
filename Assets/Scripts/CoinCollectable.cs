using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [Header("Coin Movement")]
    public float moveSpeed = 5f;

    [Header("Coin animation")]
    public float rotationSpeed = 180f;

    [Header("Cleanup")]
    public float destroyPosition = -15f;

    void Update()
    {
        //move the coin left at a consistent world speed
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        //rotate the coin as a simple animation
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        //destroy the coin when it leaves screen
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
            Debug.Log("Coin has been collected");

            GameManager.Instance.CollectCoin();

            Destroy(gameObject);
        }
    }
}