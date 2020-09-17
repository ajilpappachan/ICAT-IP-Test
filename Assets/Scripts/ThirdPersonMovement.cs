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

    GameObject selectedPlant = null;

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
            float targetAngle = PlayerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.localRotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, angle, 0.0f) * direction;
            playerController.Move(moveDirection.normalized * Speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            if(selectedPlant != null)
            {
                selectedPlant.GetComponent<PlantScript>().PickUp();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Plant")
        {
            selectedPlant = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Plant")
        {
            selectedPlant = null;
        }
    }
}
