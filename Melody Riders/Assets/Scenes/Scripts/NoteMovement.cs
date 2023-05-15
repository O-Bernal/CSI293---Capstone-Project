using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Move the note along the track
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
}
