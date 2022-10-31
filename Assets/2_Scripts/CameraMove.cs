using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float camSpeed = 20f;
    [SerializeField] float screenSizeThickness = 10;
    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject camLockedUI;

    bool isCameraLock = true;

    bool isSpaceDown;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            isCameraLock = !isCameraLock;

            if (isCameraLock)
            {
                camLockedUI.SetActive(false);
            }
            else
            {
                camLockedUI.SetActive(true);
            }
        }

        isSpaceDown = Input.GetKey(KeyCode.Space);
    }

    private void LateUpdate()
    {
        if (isSpaceDown && !isCameraLock)
        {
            transform.position = playerPos.position + offset;
        }



        if (isCameraLock)
        {
            transform.position = playerPos.position + offset;
        }
        else
        {
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
}
