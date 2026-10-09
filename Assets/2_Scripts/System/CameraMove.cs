using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float camSpeed = 20f;
    [SerializeField] float screenSizeThickness = 10;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject camLockedUI;

    private Transform playerPos;
    private bool isCameraLock = true;
    private bool isSpaceDown;

    private void Start()
    {
        playerPos = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            isCameraLock = !isCameraLock;
            camLockedUI.SetActive(isCameraLock);
        }

        isSpaceDown = Input.GetKey(KeyCode.Space);
    }

    private void LateUpdate()
    {
        if (isCameraLock || isSpaceDown)
        {
            transform.position = playerPos.position + offset;
            return;
        }

        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - screenSizeThickness && !isSpaceDown)
        {
            pos.z += camSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= screenSizeThickness && !isSpaceDown)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - screenSizeThickness && !isSpaceDown)
        {
            pos.x += camSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= screenSizeThickness && !isSpaceDown)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
