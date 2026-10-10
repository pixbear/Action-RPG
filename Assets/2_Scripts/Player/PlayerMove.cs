using PB.MANAGER;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed => player.Data.MoveSpeed;
    private float turnSpeed => moveSpeed * 3f;

    Vector3 movePoint;
    Vector3 moveDir;
    Player player;

    private NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        nav.updateRotation = false;
        nav.speed = moveSpeed;
    }

    private void OnEnable()
    {
        player.onStateChanged += (state) =>
        {
            if (state == State.Dead) Stop();
            if (state == State.Attack) Stop();
        };
    }

    private void OnDisable()
    {
        player.onStateChanged -= (state) =>
        {
            if (state == State.Dead) Stop();
            if (state == State.Attack) Stop();
        };
    }

    private void Update()
    {
        SetMovePoint();
        TryMove();
        TryStop();
    }

    private void SetMovePoint()
    {
        if (Input.GetMouseButtonDown(1)) // 1 = Mouse Right Button
        {
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("Ground");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer))
            {
                movePoint = hit.point;
                nav.SetDestination(movePoint);
                ShowClickEffect(movePoint);
            }
        }
    }

    private void TryMove()
    {
        moveDir = new Vector3(movePoint.x, transform.position.y, movePoint.z) - transform.position;
        bool isMoving = moveDir.sqrMagnitude > 0.01f;
        if (isMoving)
        {
            player.SetState(State.Move);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), turnSpeed * Time.deltaTime);
        }
        else
        {
            player.SetState(State.Idle);
        }
    }

    private void TryStop()
    {
        if (Input.GetKeyDown(KeyCode.S)) Stop();
    }

    public void Stop()
    {
        nav.ResetPath(); // Stop the player's movement
        player.SetState(State.Idle);
    }

    private void ShowClickEffect(Vector3 position)
    {
        var pos = position + Vector3.up * 0.5f;
        var effect = PObjectPoolManager.Instance.Get("ClickEffect", pos);
        PObjectPoolManager.Instance.ReleaseAfterDelay("ClickEffect", effect, 1f);
    }
}