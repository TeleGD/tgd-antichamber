using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10;

    private Vector2 viewAngle;
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir = dir.sqrMagnitude > 1 ? dir.normalized : dir;
        dir *= walkSpeed;
        dir = transform.rotation * dir;
        body.velocity = dir;
    }

    void Update()
    {
        //entrée souris
        viewAngle.x += Input.GetAxis("Mouse X");
        viewAngle.y = Mathf.Clamp(viewAngle.y - Input.GetAxis("Mouse Y"), -80, 80);

        //rotation de la vue
        transform.GetChild(0).localEulerAngles = new Vector3(viewAngle.y, 0, 0);
        transform.eulerAngles = new Vector3(0, viewAngle.x, 0);
    }

    public void RotatePlayer(float amount)
    {
        viewAngle.x += amount;
    }
}
