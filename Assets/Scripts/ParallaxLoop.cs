using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Background layers")]
    public Transform layerA;
    public Transform layerB;

    [Header("Scrolling settings")]
    [Range(0f, 1.5f)]
    public float speedMultiplier = 0.5f;

    public float scrollSpeed = 5f;

    private Camera cam;
    private float layerWidth;

    void Start()
    {
        cam = Camera.main;

        if(layerA == null || layerB == null)
        {
            Debug.LogError("Assign both background layers");
            enabled = false;
            return;
        }

        SpriteRenderer renderer = layerA.GetComponent<SpriteRenderer>();

        if(renderer == null)
        {
            Debug.LogError("LayerA needs spriterenderer");
            enabled = false;
            return;
        }

        layerWidth = renderer.bounds.size.x;

        //position the second layer directly after the first
        layerB.position = new Vector3(layerA.position.x + layerWidth, layerA.position.y, layerA.position.z);
    }

    void Update()
    {
        float movement = scrollSpeed * speedMultiplier * Time.deltaTime;

        layerA.position += Vector3.left * movement;
        layerB.position += Vector3.left * movement;

        float cameraLeft = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;

        if(layerA.position.x + layerWidth / 2 < cameraLeft)
        {
            layerA.position = new Vector3(layerB.position.x + layerWidth, layerA.position.y, layerA.position.z);
        }
        
        if(layerB.position.x + layerWidth / 2 < cameraLeft)
        {
            layerB.position = new Vector3(layerA.position.x + layerWidth, layerB.position.y, layerB.position.z);
        }
    }
}