using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        float rooftopSpeed = Preferences.GetSpeedValue();
        scrollSpeed = rooftopSpeed * 0.05f;
    }

    void Update()
    {
        if (!GameManager.gameStarted) return;
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}