using System.Collections;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    // Speed at which the note moves
    public float speed = 10f;
    // The lane this note is in
    public Lane lane;
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            originalColor = rend.material.color;
        }
        else
        {
            Debug.LogError("Renderer not found on the object.");
        }

        StartCoroutine(Glow());
    }

    void Update()
    {
        // Moves the spawned notes along the track
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    public void Hit()
    {
        StartCoroutine(Pop());
    }

    private IEnumerator Glow()
    {
        if (rend != null)
        {
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.5f);
            rend.material.color = originalColor;
        }
    }

    private IEnumerator Pop()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.2f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
        Destroy(gameObject);
    }
}
