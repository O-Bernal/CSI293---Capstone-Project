using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputArea : MonoBehaviour
{
    public KeyCode hitKey;
    private NoteMovement currentNote;
    public NoteSpawner.Lane lane;
    public float perfectRange = 0.2f;
    public float goodRange = 0.3f;
    public float earlyLateRange = 0.4f;

    private void OnTriggerEnter(Collider other)
    {
        NoteMovement note = other.GetComponent<NoteMovement>();
        if (note && note.lane == this.lane)
        {
            currentNote = note;
            //Debug.Log($"Note entered {lane} trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NoteMovement note = other.GetComponent<NoteMovement>();
        if (note && note.lane == this.lane)
        {
            // Register a miss if the note wasn't hit
            if (currentNote)
            {
                Debug.Log($"{lane} Missed!");
                Destroy(currentNote.gameObject);
                currentNote = null;
            }
            //Debug.Log($"Note exited {lane} trigger");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(hitKey) && currentNote && currentNote.lane == lane)
        {
            // Determine how well-timed the hit was based on the note's position
            // Calculate the difference between the note's position and the hit point
            float hitDifference = Mathf.Abs(transform.position.z - currentNote.transform.position.z);
            bool wasHit = false;
            Debug.Log($"{lane} Hit difference: " + hitDifference);

            if (hitDifference <= perfectRange)
            {
                Debug.Log($"{lane} Perfect!");
                Destroy(currentNote.gameObject);
                currentNote = null;
            }
            else if (hitDifference <= goodRange)
            {
                Debug.Log($"{lane} Good!");
                Destroy(currentNote.gameObject);
                currentNote = null;
            }
            else if (hitDifference <= earlyLateRange)
            {
                wasHit = true;
                if (currentNote.transform.position.z > transform.position.z)
                {
                    Debug.Log($"{lane} Early!");
                    Destroy(currentNote.gameObject);
                    currentNote = null;
                }
                else
                {
                    Debug.Log($"{lane} Late!");
                    Destroy(currentNote.gameObject);
                    currentNote = null;
                }
            }
            else
            {
                Debug.Log($"{lane} Missed!");
                Destroy(currentNote.gameObject);
            }

            // Add to score only if the note was not hit early or late
            if (!wasHit)
            {
                // Add to score here
            }
        }
    }
}

