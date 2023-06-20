using UnityEngine;

public class NoteBoardScroll : MonoBehaviour
{
    public float speed = 0.5f; // Adjust the speed of the scrolling
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        // Scroll the texture on the plane
        float offset = Time.time * speed;
        _renderer.material.SetTextureOffset("_BaseMap", new Vector2(0, offset));
    }
}