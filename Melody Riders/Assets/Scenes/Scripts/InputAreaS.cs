using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAreaS : MonoBehaviour
{
    public void HandleInput()
    {
        // Handle the input for this specific area
        Debug.Log("Handle input for " + gameObject.name);
    }
}
