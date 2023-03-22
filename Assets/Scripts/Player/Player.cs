using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


//New Input System 사용
//Input Action Asset을 C# Class로 생성하는 방법을 사용.
//Input Action Asset의 Action Map들을 설정하고, 해당 Action Map의 이벤트를 발생시킬 입력(컨트롤러, 키보드 등)을 바인딩한다.
//해당하는 키 입력이 들어오면 액션의 설정한 입력의 값이나, 버튼이 눌렸는지 여부(입력 정보)를 담은 이벤트가 실행된다.
//이벤트 실행 시 액션에 등록된 이벤트 핸들러 함수들을 전부 실행하며, 입력 정보를 이벤트 핸들러 함수의 인자로 전달한다.

public class Player : MonoBehaviour
{
    private Camera playerCamera;
    private Animator animator;

    private bool isMoving = false;
    private float moveSpeed = 5;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        if(Camera.main == null)
        {
            playerCamera = new GameObject().AddComponent<Camera>();
            playerCamera.tag = "MainCamera";
        }
        else
        {
            playerCamera = Camera.main;
        }
        playerCamera.AddComponent<PlayerCamera>();
        playerCamera.name = "PlayerCamera";
    }

    //예시로 Performed는 입력이 진행중일 때,  canceled는 입력이 끊기는 순간 발생하는 이벤트.
    private void OnEnable()
    {
        //플레이어 조작 중일 때는 UI 조작 모드를 비활성화
        InputManager.Instance.KeyActions.UI.Disable();

        InputManager.Instance.KeyActions.Player.Move.performed += OnMovePerformed;
        InputManager.Instance.KeyActions.Player.Move.canceled += OnMoveCanceled;
        InputManager.Instance.KeyActions.Player.Check.started += Talk;
        LevelManager.Instance.onLevelClear += Spawn;
    }

    private void OnDisable()
    {
        InputManager.Instance.KeyActions.UI.Enable();

        InputManager.Instance.KeyActions.Player.Move.performed -= OnMovePerformed;
        InputManager.Instance.KeyActions.Player.Move.canceled -= OnMoveCanceled;
        InputManager.Instance.KeyActions.Player.Check.started -= Talk;
        LevelManager.Instance.onLevelClear -= Spawn;
    }

    //이동, 회전
    private void Update()
    {
        //레이캐스트에서 심볼이 걸린 채로 말을 걸면, 충돌체의 정보를 얻는다
        //충돌체가 심볼이면 심볼의 종류에 따른 대화창을 출력
        //말을 거는 것은 인풋매니저에 특정 키 & 심볼이 있을 때 함수로 등록
        Debug.DrawRay(transform.position + Vector3.up, moveDirection * 2.0f, Color.red);

        if (isMoving)
        {
            gameObject.transform.position += moveDirection * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), 0.2f);
        }
    }

	//Action의 입력 정보를 context, 리턴 값은 ReadValue로 가져올 수 있음.Up으로 매핑한 입력이 들어오면 Vector2(0, 1) 값을 가져오는 식.
    //이동 방향은 카메라 기준이므로, 카메라를 기준으로 플레이어가 이동할 방향 벡터를 만들어 준다.
    public void OnMovePerformed(InputAction.CallbackContext context)
    {
        isMoving = true;
        Vector2 input = context.ReadValue<Vector2>();
        moveDirection = (input.x * playerCamera.transform.right) + (input.y * playerCamera.transform.forward);
        moveDirection.y = 0;
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        isMoving = false;
    }

    //심볼을 바라보고 엔터키 누르면 작동
    //바라보는 곳에 RoomSymbol이 있으면 RoomSymbol의 SymbolEncounter 함수 실행
    public void Talk(InputAction.CallbackContext context)

    {
        Physics.Raycast(transform.position + Vector3.up, moveDirection, out RaycastHit raycastHit, 2.0f);

        if (raycastHit.collider == null)
        {
            return;
        }

        if (raycastHit.collider.TryGetComponent(out RoomSymbol encountedSymbol))
        {
            encountedSymbol.SymbolEncounter();
        }
    }

    public void Spawn()
    {
        transform.position = Vector3.zero;
    }
}
