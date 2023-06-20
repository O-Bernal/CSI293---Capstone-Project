using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public List<NoteSpawner> spawners; // Assign your spawners in the inspector
    public float bpm = 120f;
    private float spawnInterval;
    private float timer;
    public AudioSource audioSource; // Assign the audio source in the inspector
    private bool shouldSpawnNotes = false;
    public float leadTime = 3f;

    void Start()
    {
        spawnInterval = 60f / bpm;
        Invoke("StartSpawningNotes", leadTime);
        Invoke("PlaySong", leadTime * 2);
        Invoke("StopSpawningNotes", audioSource.clip.length - leadTime);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && shouldSpawnNotes)
        {
            timer = 0f;
            SpawnNote();
        }
    }

    private void SpawnNote()
    {
        // Select a random spawner
        int randomIndex = Random.Range(0, spawners.Count);

        // Spawn a note using the selected spawner
        spawners[randomIndex].SpawnNote();
    }

    private void StartSpawningNotes()
    {
        shouldSpawnNotes = true;
    }

    private void PlaySong()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void StopSpawningNotes()
    {
        shouldSpawnNotes = false;
    }
}
