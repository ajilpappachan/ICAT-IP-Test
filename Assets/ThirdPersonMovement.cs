using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController playerController;
    public Animator playerAnimator;
    public Transform PlayerCamera;
    public float Speed = 1.0f;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("Horizontal", horizontal);
        float vertical = Input.GetAxis("Vertical");
        playerAnimator.SetFloat("Vertical", vertical);
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.01)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;
            playerController.Move(moveDirection * Speed * Time.deltaTime);
        }
        
    }
}
