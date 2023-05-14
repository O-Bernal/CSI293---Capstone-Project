using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Code to handle when 'A' is pressed
            Debug.Log("A Key Pressed");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Code to handle when 'S' is pressed
            Debug.Log("S Key Pressed");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Code to handle when 'D' is pressed
            Debug.Log("D Key Pressed");
        }
    }
}
