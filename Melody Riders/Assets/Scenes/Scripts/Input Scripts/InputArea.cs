using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputArea : MonoBehaviour
{
    public KeyCode hitKey;

    private NoteMovement currentNote;
    public NoteSpawner.Lane lane;
    public float perfectRange = 0.2f;
    public float earlyLateRange = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        NoteMovement note = other.GetComponent<NoteMovement>();
        if (note.lane == lane)
        {
            currentNote = note;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            // Register a miss if the note wasn't hit
            if (currentNote)
            {
                Debug.Log("Missed!");
                Destroy(currentNote.gameObject);
            }

            currentNote = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(hitKey) && currentNote && currentNote.lane == lane)
        {
            // Determine how well-timed the hit was based on the note's position
            // Calculate the difference between the note's position and the hit point
            float hitDifference = Mathf.Abs(transform.position.z - currentNote.transform.position.z);

            if (hitDifference <= perfectRange)
            {
                Debug.Log("Perfect!");
            }
            else if (hitDifference <= earlyLateRange)
            {
                if (currentNote.transform.position.z > transform.position.z)
                {
                    Debug.Log("Early!");
                }
                else
                {
                    Debug.Log("Late!");
                }
            }

            // Destroy the note
            Destroy(currentNote.gameObject);
        }
    }
}
