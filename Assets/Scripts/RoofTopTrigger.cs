using UnityEngine;

public class RooftopTrigger : MonoBehaviour
{
    public GameObject mathZoneCanvas;
    private bool hasSpawned = false;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (hasSpawned) return;
        if (coll.collider.CompareTag("Player") && gameObject.CompareTag("mathZone"))
        {
            hasSpawned = true;
            mathZoneCanvas.SetActive(true);
            mathZoneCanvas.GetComponent<MathZone>().Initialize(() =>
            {
                FindObjectOfType<RoofTopSpawner>().UnpauseSpawner();
                mathZoneCanvas.SetActive(false);
            });
        }
        else if (gameObject.CompareTag("winZone"))
        {
            hasSpawned = true;
            FindObjectOfType<GameManager>().TriggerWinScene();
        }
    }
}