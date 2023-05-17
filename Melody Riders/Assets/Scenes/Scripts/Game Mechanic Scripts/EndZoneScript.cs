using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneScript : MonoBehaviour
{
    // This function is called when another collider enters this object's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone is a note
        if (other.gameObject.CompareTag("Note"))
        {
            // Destroy the note
            Destroy(other.gameObject);
        }
    }
}

