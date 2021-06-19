using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 curInput;
    private Vector2 lastDirection;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.AddForce(this.transform.right * rb.mass * Time.fixedDeltaTime * 120);
        
        Vector3 vel = rb.velocity;
        Vector3 velDir = vel.normalized;
        float speed = vel.magnitude;

        //Debug.Log(velDir);

        if(curInput.magnitude > 0f)
        {
            Vector3 newDir = Vector3.Lerp(this.transform.right, new Vector3(curInput.x, curInput.y, 0), (1f / 15f) * Mathf.Clamp(0f, 1f, Time.deltaTime));

            float angle = Vector3.SignedAngle(this.transform.right, new Vector3(curInput.x, curInput.y, 0).normalized, new Vector3(0,0,1));
            Debug.Log(angle);
            float wishChange = Mathf.Sign(angle) * 180 * Time.fixedDeltaTime; // Maximum amount to change ang by
            if (Mathf.Abs(angle) < Mathf.Abs(wishChange))
            {
                rb.rotation += angle;
            }
            else
            {
                rb.rotation += wishChange;
            }
        }
        
    }

    public void movementUpdated(InputAction.CallbackContext value)
    {
        curInput = value.ReadValue<Vector2>();
        if(curInput.magnitude != 0f)
        {
            lastDirection = curInput;
        }
    }
}
