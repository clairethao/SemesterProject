using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    bool isOnRoofTop;
    public float moveSpeed;
    public GameManager gameManager;
    void Start()
    {
        moveSpeed = Preferences.GetSpeedValue();
    }

    void Update()
    {
        if(transform.position.y < -10f)
        {
            gameManager.TriggerGameOver();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnRoofTop) jump();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gameObject.transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("roofTop") || coll.collider.CompareTag("mathZone"))
        {
            isOnRoofTop = true;
        }
        else
        {
            isOnRoofTop = false;
        }
    }

    void jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300.0f));
        isOnRoofTop = false;
    }
}
