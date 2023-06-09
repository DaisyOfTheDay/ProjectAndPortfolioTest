﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;

    bool canDash = true;
    public float dashStrength = 2;
    public int dashInterval = 3;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            rigidbody.velocity = transform.rotation * new Vector3(Input.GetAxis("Horizontal") * dashStrength,jumpStrength, Input.GetAxis("Vertical") * dashStrength);
            canDash= false;
            StartCoroutine(dashCooldown());
        }

    }

    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(dashInterval);
        canDash = true;
        StopCoroutine(dashCooldown());
    }
}
