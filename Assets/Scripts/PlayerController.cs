using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public GameObject winTextObject;

    bool onGround;
  
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
       
    }

     
    void OnJump(InputValue val)
    {
      
        if (onGround)
        {
            Debug.Log("Jumped");
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
       
    }

    void OnCollisionEnter(Collision collision)
    {
        int collisionCount = collision.contactCount;
        for (int i = 0; i < collisionCount; i++)
        {
            Vector3 contactNormal = collision.contacts[i].normal;
            bool isGround = Vector3.Dot(contactNormal, Vector3.up) > 0.9;
            if (isGround)
            {
                onGround = true;
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)

    {
        onGround = false;
    }


    void OnMove(InputValue val)
    { 
        Vector2 v = val.Get<Vector2>();
        Debug.Log(v);

        movementX = v.x;
        movementY = v.y;

        Vector3 newVelocity = new Vector3();
        newVelocity.x = v.x;
        newVelocity.z = v.y;
        rb.velocity = newVelocity;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 20)
        {
            winTextObject.SetActive(true);
        }

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();

        }
    }

  

}

