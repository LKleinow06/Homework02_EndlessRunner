using UnityEngine;

public class EndlessScroll : MonoBehaviour
{
    [Header("Ground tiles")]
    public Transform groundA;
    public Transform groundB;
    public Transform groundC;

    [Header("Scrolling")]
    public float scrollSpeed = 5f;

    private Transform[] groundTiles;
    private float groundWidth;

    private Camera cam;
    private float cameraLeft;

    void Start()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("No main camera");
            enabled = false;
            return;
        }

        groundTiles = new Transform[]
        {
            groundA,
            groundB,
            groundC
        };

        //Ensure that all 3 tiles are assigned in inspector
        foreach (Transform tile in groundTiles)
        {
            if (tile == null)
            {
                Debug.LogError("Assign the 3 ground tiles");
                enabled = false;
                return;
            }

            if (tile.GetComponent<SpriteRenderer>() == null)
            {
                Debug.LogError(tile.name + " needs a spriterenderer");
                enabled = false;
                return;
            }
        }

        //get the width of the ground sprite
        groundWidth = groundA.GetComponent<SpriteRenderer>().bounds.size.x;

        if (groundWidth <= 0)
        {
            Debug.LogError("Invalid ground width");
            enabled = false;
            return;
        }

        //position the tiles one after the other
        PositionGroundTiles();
    }

    void Update()
    {
        //move all ground tiles left
        float movement = scrollSpeed * Time.deltaTime;

        foreach (Transform tile in groundTiles)
        {
            tile.position += Vector3.left * movement;
        }

        //get the left edge of the camera
        cameraLeft = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        //check each tile to see if it needs to be recycled
        foreach (Transform tile in groundTiles)
        {
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();

            float tileRight = sr.bounds.max.x;

            //recycle when the tile has completely passed the left of the camera
            if (tileRight < cameraLeft)
            {
                MoveTileToRight();
            }
        }
    }

    void PositionGroundTiles()
    {
        //use the first tile as starting position
        float startX = groundA.position.x;

        groundTiles[0].position = new Vector3(startX, groundA.position.y, groundA.position.z);

        groundTiles[1].position = new Vector3(startX + groundWidth, groundA.position.y, groundA.position.z);

        groundTiles[2].position = new Vector3(startX + groundWidth * 2, groundA.position.y, groundA.position.z);
    }

    void MoveTileToRight()
    {
        //find the most right tile
        Transform rightmost = groundTiles[0];

        foreach (Transform tile in groundTiles)
        {
            if (tile.position.x > rightmost.position.x)
            {
                rightmost = tile;
            }
        }

        //find the tile that needs to be recycled
        Transform leftmost = groundTiles[0];

        foreach (Transform tile in groundTiles)
        {
            if (tile.position.x < leftmost.position.x)
            {
                leftmost = tile;
            }
        }

        //place the most left tile directly after the current most right tile
        SpriteRenderer rightRenderer = rightmost.GetComponent<SpriteRenderer>();

        float rightEdge = rightRenderer.bounds.max.x;

        SpriteRenderer leftRenderer = leftmost.GetComponent<SpriteRenderer>();

        float leftHalfWidth = leftRenderer.bounds.extents.x;

        Vector3 newPosition = leftmost.position;

        newPosition.x = rightEdge + leftHalfWidth;

        leftmost.position = newPosition;
    }
}