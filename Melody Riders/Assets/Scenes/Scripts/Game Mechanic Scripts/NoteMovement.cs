using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    // Speed at which the note moves
    public float speed = 10f;
    // The lane this note is in
    public NoteSpawner.Lane lane;

    void Start()
    {
        
    }

    void Update()
    {
        // Moves the spawned notes along the track
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
}
