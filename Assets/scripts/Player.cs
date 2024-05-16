using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isRunning = false;
    private GameObject currentObject = null;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraMovement();
        HandleJumping();
        HandlePickup();
    }

    void HandleMovement()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;
        float moveDirectionY = moveDirection.y;

        moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * speed;
        moveDirection.y = moveDirectionY;

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleCameraMovement()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleJumping()
    {
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }
    }

    void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            PickUpObject();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            currentObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            currentObject = null;
        }
    }

    void PickUpObject()
    {
        if (currentObject != null)
        {
            currentObject.transform.SetParent(transform);
            currentObject.transform.localPosition = new Vector3(0.5f, -0.5f, 1f); // Adjust based on your player's hand position
            currentObject.transform.localRotation = Quaternion.identity;

            Rigidbody rb = currentObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            Collider col = currentObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }

            Debug.Log("Object picked up: " + currentObject.name);
        }
    }
}
