using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Transform view;
	private int flashlightSpeed = 10;

    void Start()
    {
        view = Camera.main.transform;
    }
    
    void Update()
    {
        transform.position = view.position + Vector3.down * 0.4f + view.rotation * (Vector3.right * 0.1f);

        Quaternion targetRot;
        RaycastHit hit;
        if (Physics.Raycast(view.position, view.forward, out hit, 100f))
            targetRot = Quaternion.LookRotation(hit.point - transform.position);
        else
            targetRot = view.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * flashlightSpeed);
    }
}
