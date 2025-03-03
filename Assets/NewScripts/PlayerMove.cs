using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;

    public float walkSpeed = 10f;
    public float runSpeed = 25f;
    private Vector3 moveDirection;

    // 검 뽑기 관련 변수
    private bool isSwordDrawn = false;

    // 백대시 관련 변수
    private bool canBackDash = true;
    public float backDashSpeed = 10f;
    private float lastSPressTime = 0f;
    private float doublePressDelay = 0.3f;

    // 카메라 회전 변수
    public float turnSpeed = 100.0f;
    public float lookSpeed = 2.0f;
    private float cameraRotationX = 0.0f;

    private static PlayerMove instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //  씬 전환 시 플레이어 유지
        }
        else
        {
            Destroy(gameObject); //  이미 존재하면 중복 생성 방지
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseRotation();
        HandleSword();
        HandleBackDash();
    }

    private void HandleMovement()
    {
        if (!cc.enabled) return; //  CharacterController가 비활성화면 이동 중지

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveInput = transform.right * moveX + transform.forward * moveZ;
        moveInput.Normalize();

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        moveDirection.x = moveInput.x * currentSpeed;
        moveDirection.z = moveInput.z * currentSpeed;

        cc.Move(moveDirection * Time.deltaTime);

        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveZ", moveZ);
        animator.SetBool("IsRunning", isRunning);
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -80f, 80f);
    }

    private void HandleSword()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isSwordDrawn)
        {
            animator.SetTrigger("DrawSword");
            isSwordDrawn = true;
            StartCoroutine(TransitionToIdle2());
        }
        if (Input.GetKeyDown(KeyCode.E) && isSwordDrawn)
        {
            animator.SetTrigger("SheatheSword");
            isSwordDrawn = false;
        }
    }

    private IEnumerator TransitionToIdle2()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("Idle1ToIdle2");
    }

    private void HandleBackDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSPressTime < doublePressDelay && canBackDash)
            {
                StartCoroutine(BackDash());
            }
            lastSPressTime = Time.time;
        }
    }

    private IEnumerator BackDash()
    {
        canBackDash = false;
        animator.SetTrigger("BackDashTrigger");

        Vector3 dashDirection = -transform.forward * backDashSpeed;
        cc.Move(dashDirection * Time.deltaTime);

        yield return new WaitForSeconds(0.2f);

        yield return new WaitForSeconds(1f);
        canBackDash = true;
    }
}




