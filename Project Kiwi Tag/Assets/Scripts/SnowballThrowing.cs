using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

public class SnowballThrowing : MonoBehaviour
{
    [Header("Credits: TheCoder")]

    List<Vector3> trackingPos = new List<Vector3>();
    [Header("Throw Force")]
    public float velocity = 10f;

    [Header("Dont Touch")]
    public bool canThrow = false;

    void Update()
    {
        if (!canThrow)
        {
            if (trackingPos.Count > 15)
            {
                trackingPos.RemoveAt(0);
            }
            trackingPos.Add(transform.position);
        }

        if (canThrow && EasyInputs.GetGripButtonFloat(EasyHand.RightHand) == 0f)
        {
            ThrowSnowball();
        }

        if (canThrow && EasyInputs.GetGripButtonFloat(EasyHand.LeftHand) == 0f)
        {
            ThrowSnowball();
        }
    }

    void ThrowSnowball()
    {
        Vector3 direction = trackingPos[trackingPos.Count - 1] - trackingPos[0];
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(direction.normalized * velocity, ForceMode.VelocityChange);
        canThrow = false;
    }
}
