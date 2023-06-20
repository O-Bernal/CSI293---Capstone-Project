using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Lane lane;

    public Color laneAColor = new Color(1f, 0.4f, 0.7f);
    public Color laneSColor = Color.blue;
    public Color laneDColor = new Color(1f, 0.4f, 0.7f);

    public void SpawnNote()
    {
        GameObject note = Instantiate(notePrefab, transform.position, Quaternion.identity);

        NoteMovement noteMovement = note.GetComponent<NoteMovement>();
        noteMovement.lane = lane;

        Renderer rend = note.GetComponent<Renderer>();
        if (rend != null)
        {
            switch (lane)
            {
                case Lane.A:
                    rend.material.color = laneAColor;
                    break;
                case Lane.S:
                    rend.material.color = laneSColor;
                    break;
                case Lane.D:
                    rend.material.color = laneDColor;
                    break;
                default:
                    rend.material.color = Color.white;
                    break;
            }
        }
    }
}
