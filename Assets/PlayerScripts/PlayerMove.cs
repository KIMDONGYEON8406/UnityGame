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




//public class PlayerMove : MonoBehaviour
//{
//    // 컴포넌트 참조 변수
//    private Animator animator; // 플레이어 애니메이터
//    private Rigidbody rb; // 플레이어 Rigidbody

//    // 이동 관련 변수
//    public float walkSpeed = 10f; // 걷기 속도
//    public float runSpeed = 25f; // 달리기 속도

//    // **검 뽑기 관련 변수**
//    private bool isSwordDrawn = false; // 검을 뽑았는지 여부

//    // 백대시 관련 변수
//    private bool canBackDash = true; // 백대시 가능 여부
//    public float backDashSpeed = 10f; // 백대시 속도
//    private float lastSPressTime = 0f; // 마지막 S 키 입력 시간
//    private float doublePressDelay = 0.3f; // 더블탭 인식 딜레이

//    // 중력 적용 변수
//    public float gravity = -9.81f; // 중력 값
//    private Vector3 velocity; // 현재 속도

//    // 카메라 회전 속도 조정 변수
//    public float turnSpeed = 100.0f; // 좌우 회전 속도
//    public float lookSpeed = 2.0f; // 위아래 회전 감도
//    private float cameraRotationX = 0.0f; // 위아래 회전 값 저장

//    private void Start()
//    {
//        // 컴포넌트 가져오기
//        animator = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody>();

//        // Rigidbody의 기본 설정
//        rb.freezeRotation = true; // 회전 고정 (물리 영향 최소화)
//    }

//    private void Update()
//    {
//        // 플레이어 이동 입력 받기
//        float moveX = Input.GetAxisRaw("Horizontal"); // 좌우 이동
//        float moveZ = Input.GetAxisRaw("Vertical"); // 앞뒤 이동

//        // 이동 속도 설정 (Shift 키를 누르면 달리기)
//        bool isRunning = Input.GetKey(KeyCode.LeftShift);
//        float currentSpeed = isRunning ? runSpeed : walkSpeed;

//        //  만약 Rigidbody가 isKinematic이면 이동을 막음 (포탈 이동 중인 경우)
//        if (rb.isKinematic)
//        {
//            return;
//        }

//        // 이동 처리 (Rigidbody를 사용하여 이동)
//        Vector3 moveInput = new Vector3(moveX, 0, moveZ).normalized;
//        rb.velocity = moveInput * currentSpeed;

//        // 애니메이션 상태 업데이트
//        animator.SetFloat("MoveX", moveInput.x);
//        animator.SetFloat("MoveZ", moveInput.z);
//        animator.SetBool("IsRunning", isRunning);

//        // **검 뽑기 (Q) 및 넣기 (E) 기능 추가**
//        if (Input.GetKeyDown(KeyCode.Q) && !isSwordDrawn) // Q 키: 검 뽑기
//        {
//            animator.SetTrigger("DrawSword");
//            isSwordDrawn = true;
//            StartCoroutine(TransitionToIdle2());
//        }
//        if (Input.GetKeyDown(KeyCode.E) && isSwordDrawn) // E 키: 검 넣기
//        {
//            animator.SetTrigger("SheatheSword");
//            isSwordDrawn = false;
//        }

//        // 백대시 감지 (더블탭 S 감지 후 실행)
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            if (Time.time - lastSPressTime < doublePressDelay && canBackDash)
//            {
//                StartCoroutine(BackDash()); // 백대시 실행
//            }
//            lastSPressTime = Time.time;
//        }

//        // 마우스 회전 (좌우 Y축)
//        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
//        transform.Rotate(Vector3.up * mouseX);

//        // 마우스 회전 (위아래 X축)
//        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
//        cameraRotationX -= mouseY;
//        cameraRotationX = Mathf.Clamp(cameraRotationX, -80f, 80f); // 위아래 회전 각도 제한
//    }

//    // **검 뽑기 후 Idle 애니메이션 전환**
//    private IEnumerator TransitionToIdle2()
//    {
//        yield return new WaitForSeconds(1.0f);
//        animator.SetTrigger("Idle1ToIdle2");
//    }

//    // 백대시 기능
//    private IEnumerator BackDash()
//    {
//        canBackDash = false; // 백대시 중에는 연속 입력 방지
//        animator.SetTrigger("BackDashTrigger"); // 백대시 애니메이션 실행

//        // 뒤로 빠르게 이동 (백대시 방향)
//        Vector3 dashDirection = -transform.forward * backDashSpeed;
//        rb.velocity = dashDirection;

//        yield return new WaitForSeconds(0.2f); // 백대시 지속 시간
//        rb.velocity = Vector3.zero; // 백대시 후 정지

//        yield return new WaitForSeconds(1f); // 백대시 쿨타임
//        canBackDash = true; // 다시 백대시 가능하도록 설정
//    }
//}
