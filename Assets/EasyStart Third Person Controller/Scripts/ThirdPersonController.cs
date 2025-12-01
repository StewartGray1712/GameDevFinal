using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float velocity = 5f;
    public float sprintAdittion = 3.5f;
    public float jumpForce = 18f;
    public float jumpTime = 0.85f;
    public float gravity = 9.8f;

    float jumpElapsedTime = 0;

    // Player states
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Inputs
    float inputHorizontal;
    float inputVertical;

    Animator animator;
    CharacterController cc;

    private PlantingManager plantingManager;
    private InventoryUIManager inventoryUIManager;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        plantingManager = FindAnyObjectByType<PlantingManager>();
        inventoryUIManager = FindAnyObjectByType<InventoryUIManager>();

        if (animator == null)
            Debug.LogWarning("You do not have an Animator on your player!");
    }

    void Update()
    {
        // ----- Movement Inputs -----
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        // ----- Jump -----
        bool inputJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0);
        if (inputJump && cc.isGrounded)
            isJumping = true;

        // ----- Sprint -----
        bool inputSprint = Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Fire3") > 0.1f;
        isSprinting = inputSprint;

        // ----- Crouch (Toggle) -----
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton8))
            isCrouching = !isCrouching;

        // ----- Inventory -----
        // Removed F key — inventory is only toggled via controller (JoystickButton7)
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
            ToggleInventory();

        // ----- Pickup, Plant, Harvest -----
        if (Input.GetKeyDown(KeyCode.JoystickButton1)) // East
            TriggerPickup();
        if (Input.GetKeyDown(KeyCode.JoystickButton2)) // West
            TriggerPlant();
        if (Input.GetKeyDown(KeyCode.JoystickButton3)) // North
            TriggerHarvest();

        // ----- Animations -----
        if (cc.isGrounded && animator != null)
        {
            animator.SetBool("crouch", isCrouching);

            float minSpeed = 0.9f;
            animator.SetBool("run", cc.velocity.magnitude > minSpeed);
            animator.SetBool("sprint", isSprinting && cc.velocity.magnitude > minSpeed);
        }

        if (animator != null)
            animator.SetBool("air", !cc.isGrounded);

        HeadHittingDetect();
    }

    private void FixedUpdate()
    {
        float velocityAdittion = 0;
        if (isSprinting)
            velocityAdittion = sprintAdittion;
        if (isCrouching)
            velocityAdittion = -(velocity * 0.5f);

        float dx = inputHorizontal * (velocity + velocityAdittion) * Time.deltaTime;
        float dz = inputVertical * (velocity + velocityAdittion) * Time.deltaTime;
        float dy = 0;

        if (isJumping)
        {
            dy = Mathf.SmoothStep(jumpForce, jumpForce * 0.3f, jumpElapsedTime / jumpTime) * Time.deltaTime;
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        dy -= gravity * Time.deltaTime;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * dz + right * dx;

        if (dx != 0 || dz != 0)
        {
            float angle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.15f);
        }

        cc.Move(moveDir + Vector3.up * dy);
    }

    void HeadHittingDetect()
    {
        float distance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float calc = cc.height / 2f * distance;

        if (Physics.Raycast(ccCenter, Vector3.up, calc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }

    // ----- Controller Action Hooks -----
    void TriggerPickup()
    {
        Debug.Log("Pickup triggered via controller.");
        InputSimulator.ClickMouseLeft();
    }

    void TriggerPlant()
    {
        if (plantingManager != null)
        {
            Debug.Log("Plant triggered via controller.");
            plantingManager.SendMessage("TryPlant");
        }
    }

    void TriggerHarvest()
    {
        Debug.Log("Harvest triggered via controller.");
        InputSimulator.PressKey(KeyCode.E);
    }

    void ToggleInventory()
    {
        if (inventoryUIManager != null)
            inventoryUIManager.ToggleInventory();
    }
}

