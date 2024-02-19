using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;

public class SnowballGrab : MonoBehaviour
{
    [Header("Credits: TheCoder")]

    [Header("Snap Grabing Manager")]
    public SnapGrabbingManager grabManager;

    [Header("Snowball Prefabs")]
    public GameObject snowballRightPrefab;
    public GameObject snowballLeftPrefab;

    [Header("Spawn Locations")]
    public GameObject rightLocation;
    public GameObject leftLocation;

    [Header("Hand Tags")]
    public string rightHand;
    public string leftHand;

    private bool rightCanGrab = false;
    private bool rightIsGrabbing = false;
    private bool leftCanGrab = false;
    private bool leftIsGrabbing = false;
    private GameObject currentSnowballRight;
    private GameObject currentSnowballLeft;
    private Transform rightOriginalParent;
    private Transform leftOriginalParent;

    void Start()
    {
        rightOriginalParent = snowballRightPrefab.transform.parent;
        leftOriginalParent = snowballLeftPrefab.transform.parent;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(rightHand))
        {
            if (!rightIsGrabbing)
            {
                rightCanGrab = true;
            }
        }

        if (other.CompareTag(leftHand))
        {
            if (!leftIsGrabbing)
            {
                leftCanGrab = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(rightHand))
        {
            if (!rightIsGrabbing)
            {
                rightCanGrab = false;
            }
        }

        if (other.CompareTag(leftHand))
        {
            if (!leftIsGrabbing)
            {
                leftCanGrab = false;
            }
        }
    }

    void Update()
    {
        bool canGrabR = grabManager.canGrabRight;
        bool canGrabL = grabManager.canGrabLeft;

        if (canGrabR && rightCanGrab && EasyInputs.GetGripButtonDown(EasyHand.RightHand))
        {
            rightCanGrab = false;
            grabManager.canGrabRight = false;
            rightIsGrabbing = true;
            currentSnowballRight = Instantiate(snowballRightPrefab, rightLocation.transform.position, rightLocation.transform.rotation);
            currentSnowballRight.transform.SetParent(rightLocation.transform);
            currentSnowballRight.GetComponent<Rigidbody>().isKinematic = true;
            currentSnowballRight.GetComponent<Collider>().enabled = false;

        } else if (canGrabL && leftCanGrab && EasyInputs.GetGripButtonDown(EasyHand.LeftHand))
        {
            leftCanGrab = false;
            grabManager.canGrabLeft = false;
            leftIsGrabbing = true;
            currentSnowballLeft = Instantiate(snowballLeftPrefab, leftLocation.transform.position, leftLocation.transform.rotation);
            currentSnowballLeft.transform.SetParent(leftLocation.transform);
            currentSnowballLeft.GetComponent<Rigidbody>().isKinematic = true;
            currentSnowballLeft.GetComponent<Collider>().enabled = false;
        }

        if (rightIsGrabbing && EasyInputs.GetGripButtonFloat(EasyHand.RightHand) == 0f && currentSnowballRight != null)
        {
            grabManager.canGrabRight = true;
            rightIsGrabbing = false;
            currentSnowballRight.GetComponent<SnowballDestroy>().canDestroy = true;
            currentSnowballRight.GetComponent<Rigidbody>().isKinematic = false;
            currentSnowballRight.GetComponent<Collider>().enabled = true;
            currentSnowballRight.transform.SetParent(rightOriginalParent);
            currentSnowballRight.GetComponent<SnowballThrowing>().canThrow = true;
        } else if (leftIsGrabbing && EasyInputs.GetGripButtonFloat(EasyHand.LeftHand) == 0f && currentSnowballLeft != null)
        {
            grabManager.canGrabLeft = true;
            leftIsGrabbing = false;
            currentSnowballLeft.GetComponent<SnowballDestroy>().canDestroy = true;
            currentSnowballLeft.GetComponent<Rigidbody>().isKinematic = false;
            currentSnowballLeft.GetComponent<Collider>().enabled = true;
            currentSnowballLeft.transform.SetParent(leftOriginalParent);
            currentSnowballLeft.GetComponent<SnowballThrowing>().canThrow = true;
        }
    }
}