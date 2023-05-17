using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Beat
{
    public float time;
    public int lane;
}

public class BeatMap : MonoBehaviour
{
    public Beat[] beats;
    public NoteSpawner[] spawners;

    private void Start()
    {
        StartCoroutine(SpawnBeats());
    }

    private IEnumerator SpawnBeats()
    {
        foreach (var beat in beats)
        {
            yield return new WaitForSeconds(beat.time);
            spawners[beat.lane].SpawnNote();
        }
    }
}