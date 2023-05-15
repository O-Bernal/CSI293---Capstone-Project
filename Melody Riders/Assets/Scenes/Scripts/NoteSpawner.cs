using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public float bpm = 120f;

    private float spawnInterval;

    private float timer;

    void Start()
    {
        // Calculate the interval between spawns based on the BPM
        spawnInterval = 60f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // If it's time to spawn a new note
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0f;

            // Spawn a new note
            Instantiate(notePrefab, transform.position, Quaternion.identity);
        }
    }
}
