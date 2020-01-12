using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10;
    public float jumpForce = 4;

    private Vector2 viewAngle;
    private Rigidbody body;

	private bool isCrouched = false;
	private float canJump = 0f;

	public float sensitivity = 10f;
    public float flashlightMoveIntensity = 5;
    private Transform flashlight;
    private Vector2 flashlightOffset;
    public float viewBobbingAmount = 2;

    private bool cursorLocked = true;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        flashlight = transform.GetChild(0).GetChild(0);
    }

    private void FixedUpdate()
    {
		float yVel = body.velocity.y;
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir = dir.sqrMagnitude > 1 ? dir.normalized : dir;
        dir *= walkSpeed;
        dir = transform.rotation * dir;
		dir += Vector3.up * yVel;
		body.velocity = dir;
    }

    void LateUpdate()
    {
        //entrée souris
        Vector2 mouseMove = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitivity;
        viewAngle.x += mouseMove.x;
        viewAngle.y = Mathf.Clamp(viewAngle.y - mouseMove.y, -80, 80);

        //rotation de la vue
        transform.GetChild(0).localEulerAngles = new Vector3(viewAngle.y, 0, 0);
        transform.eulerAngles = new Vector3(0, viewAngle.x, 0);

        //view bobbing
        float amount = body.velocity.sqrMagnitude > 1 ? viewBobbingAmount : 0;
        Vector3 viewBobbing = new Vector3(
            -Mathf.Abs(Mathf.Cos(Time.time * 6)) * amount,
            Mathf.Sin(Time.time * 6) * amount,
            0);

        flashlightOffset = Vector2.Lerp(flashlightOffset, mouseMove * flashlightMoveIntensity, Time.deltaTime * 4);
        flashlight.localEulerAngles = new Vector3(-flashlightOffset.y, flashlightOffset.x, 0) + viewBobbing;


        if (Input.GetButtonDown("Crouch"))
		{
			Crouch();
		}

		if (Input.GetButtonDown("Jump") && Time.time > canJump)
		{
			Jump();
			canJump = Time.time + 1f;
		}

		if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            cursorLocked = !cursorLocked;
            Cursor.visible = !cursorLocked;
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
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

	public void Jump()
	{
		body.velocity += Vector3.up * jumpForce;
	}
}
