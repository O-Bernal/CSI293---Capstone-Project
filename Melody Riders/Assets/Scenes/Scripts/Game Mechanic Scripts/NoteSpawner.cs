using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Lane lane;
    public float bpm = 120f;
    private float spawnInterval;
    private float timer;

    void Start()
    {
        // Calculate the interval between spawns based on the BPM
        spawnInterval = 60f / bpm;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // If it's time to spawn a new note
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0f;

            // Spawn a new note
            SpawnNote();
        }
    }

    public enum Lane
    {
        A,
        S,
        D,
        Space
    }

    public void SpawnNote()
    {
        // Instantiate a new note at this spawner's position
        GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);

        // Set the lane of the note
        note.GetComponent<NoteMovement>().lane = lane;
    }
}
