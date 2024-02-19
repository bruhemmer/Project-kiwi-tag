using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHandPhase : MonoBehaviour
{
    [Header("THIS SCRIPT WAS MADE BY FIZZY NOT YOU, PLEASE GIVE CREDIT")]
    public Transform sphere;
    public Transform controller;

    void Update()
    {
        if (true)
        {
            sphere.rotation = controller.rotation;
        }
    }
}
