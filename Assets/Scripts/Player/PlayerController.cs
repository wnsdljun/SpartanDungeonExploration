using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;


    #region 이동/점프 관련
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpPower;
    public LayerMask groundLayerMask;

    private Vector2 movementInput;
    private bool sprint;


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            movementInput = Vector2.zero;
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnSprintInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //달리기
            sprint = true;
        }
        if (context.canceled)
        {
            //원래 속도로 돌아오기
            sprint = false;
        }
    }
    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask)) return true;
        }
        return false;
    }
    private void Move()
    {
        //달릴까 말까
        if (sprint && CharacterManager.Instance.Player.conditions.CanSprint())
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        Vector3 dir = transform.forward * movementInput.y + transform.right * movementInput.x;
        dir.Normalize();
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    #endregion

    #region 카메라 조작 관련
    [Header("Look")]
    public Transform cameraContainer;
    public float minLookX;
    public float maxLookX;
    public float lookSensitivity;

    private Vector2 mouseInputDelta;
    private float cameraRotX;
    [HideInInspector] public bool canLook = true;


    public void OnMouseLookInput(InputAction.CallbackContext context)
    {
        mouseInputDelta = context.ReadValue<Vector2>();
    }

    private void CameraLook()
    {
        cameraRotX += mouseInputDelta.y * lookSensitivity;
        cameraRotX = Mathf.Clamp(cameraRotX, minLookX, maxLookX);
        cameraContainer.localEulerAngles = new Vector3(-cameraRotX, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseInputDelta.x * lookSensitivity, 0);
    }



    #endregion

    #region 인벤토리 관련
    public Action playerInventory;

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerInventory?.Invoke();
            ToggleCursor();
        }
    }

    private void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
    #endregion
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

}
