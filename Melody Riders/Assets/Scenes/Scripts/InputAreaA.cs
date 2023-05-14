using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAreaA : MonoBehaviour
{
    public void HandleInput()
    {
        // Handle the input for this specific area
        Debug.Log("Handle input for " + gameObject.name);
    }
}
