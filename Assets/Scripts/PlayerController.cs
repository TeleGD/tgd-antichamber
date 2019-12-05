using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10;

    private Vector2 viewAngle;
    private Rigidbody body;

	private bool isCrouched = false;

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

		if (Input.GetButtonDown("Crouch"))
		{
			Crouch();
		}
	}

    public void RotatePlayer(float amount)
    {
        viewAngle.x += amount;
    }

	public void Crouch()
	{
		if (isCrouched)
		{
			this.transform.position += Vector3.up * 0.45f;

			this.gameObject.GetComponent<CapsuleCollider>().height += 0.9f;

			Vector3 cameraPos = this.transform.Find("Main Camera").gameObject.transform.position;
			this.transform.Find("Main Camera").gameObject.transform.position += Vector3.up * 0.425f;

			isCrouched = false;
		}
		else
		{
			Debug.Log("Avant"+transform.position.y);
			this.transform.position += Vector3.down * 0.45f;
			Debug.Log("Après"+transform.position.y);

			this.gameObject.GetComponent<CapsuleCollider>().height -= 0.9f;

			Vector3 cameraPos = this.transform.Find("Main Camera").gameObject.transform.position;
			this.transform.Find("Main Camera").gameObject.transform.position += Vector3.down * 0.425f;

			isCrouched = true;
		}
	}
}
