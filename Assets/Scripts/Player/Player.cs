using UnityEngine;
using UnityEngine.InputSystem;


//New Input System ���
//Input Action Asset�� C# Class�� �����ϴ� ����� ���.
//Input Action Asset�� Action Map���� �����ϰ�, �ش� Action Map�� �̺�Ʈ�� �߻���ų �Է�(��Ʈ�ѷ�, Ű���� ��)�� ���ε��Ѵ�.
//�ش��ϴ� Ű �Է��� ������ �׼��� ������ �Է��� ���̳�, ��ư�� ���ȴ��� ����(�Է� ����)�� ���� �̺�Ʈ�� ����ȴ�.
//�̺�Ʈ ���� �� �׼ǿ� ��ϵ� �̺�Ʈ �ڵ鷯 �Լ����� ���� �����ϸ�, �Է� ������ �̺�Ʈ �ڵ鷯 �Լ��� ���ڷ� �����Ѵ�.

public class Player : MonoBehaviour
{
    private Camera playerCamera;
    private Animator animator;

    private bool isMoving = false;
    private float moveSpeed = 5;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        playerCamera = Camera.main; 
        animator = GetComponentInChildren<Animator>();    
    }

    //���÷� Performed�� �Է��� �������� ��, canceled�� �Է��� ����� ���� �߻��ϴ� �̺�Ʈ.
    private void OnEnable()
    {
        //�÷��̾� ���� ���� ���� UI ���� ��带 ��Ȱ��ȭ
        InputActions.keyActions.UI.Disable();

        //�پ��� Ű�Է¿� ���� �߻��ϴ� �̺�Ʈ ���
        InputActions.keyActions.Player.Move.performed += OnMovePerformed;
        InputActions.keyActions.Player.Move.canceled += OnMoveCanceled;
        InputActions.keyActions.Player.Check.started += OnCheckStarted;
        InputActions.keyActions.Player.Menu.started += OnMenuStarted;

        //���� Ŭ���� �� �߻��ϴ� �̺�Ʈ ���
        LevelManager.Instance.OnLevelClear += Spawn;
    }

    private void OnDisable()
    {
        InputActions.keyActions.UI.Enable();

        InputActions.keyActions.Player.Move.performed -= OnMovePerformed;
        InputActions.keyActions.Player.Move.canceled -= OnMoveCanceled;
        InputActions.keyActions.Player.Check.started -= OnCheckStarted;
        InputActions.keyActions.Player.Menu.started -= OnMenuStarted;

        LevelManager.Instance.OnLevelClear -= Spawn;
    }

    //�̵�, ȸ��
    private void Update()
    {
        //����� �� �� �߻�
        Debug.DrawRay(transform.position + Vector3.up, moveDirection * 2.0f, Color.red);

        if (isMoving)
        {
            gameObject.transform.position += moveDirection * Time.deltaTime * moveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), 0.2f);
        }
    }

	//Action�� �Է� ������ context, ���� ���� ReadValue�� ������ �� ����.Up���� ������ �Է��� ������ Vector2(0, 1) ���� �������� ��.
    //�̵� ������ ī�޶� �����̹Ƿ�, ī�޶� �������� �÷��̾ �̵��� ���� ���͸� ����� �ش�.
    public void OnMovePerformed(InputAction.CallbackContext context)
    {
        isMoving = true;
        Vector2 input = context.ReadValue<Vector2>();
        moveDirection = (input.x * playerCamera.transform.right) + (input.y * playerCamera.transform.forward);
        moveDirection.y = 0;
        animator.CrossFade("RUN", 0.1f); //RUN �ִϸ��̼����� 0.1f ���� ��ġ�� �ð��� ������ �ε巴�� ��ȯ.
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        isMoving = false;
        animator.CrossFade("WAIT", 0.2f);
    }

    //����Ű ������ �۵��ϴ� �ɺ��� ��ȣ�ۿ��� �ϴ� �Լ�. 
    public void OnCheckStarted(InputAction.CallbackContext context)
    {

        //����ĳ��Ʈ ������ �ɺ��� �ɸ� ä�� ���� �ɸ�, �浹�� ������Ʈ�� ������ ��´�
        //�浹�� ������Ʈ�� �ɺ��� ���, �ɺ��� ��ī���� �Լ��� ����.
        Physics.Raycast(transform.position + Vector3.up, moveDirection, out RaycastHit raycastHit, 2.0f);

        if (raycastHit.collider == null)
        {
            return;
        }

        if (raycastHit.collider.TryGetComponent(out RoomSymbol encountedSymbol))
        {
            encountedSymbol.TalkStart();
        }
    }

    //I��ư���� �κ��丮 UI ����
    public void OnMenuStarted(InputAction.CallbackContext context)
    {
        LibraryUI libraryUI = UIManager.Instance.ShowUI("LibraryUI").GetComponent<LibraryUI>();
        libraryUI.Init(false);

    }

    public void Spawn()
    {
        transform.position = Vector3.zero;
    }
}
