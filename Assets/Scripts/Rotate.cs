using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
   public Transform objectToRotate; // ������ �� ������, ������� ����� �������
    public float rotationSpeed = 5f; // �������� ��������
    public float damping = 0.5f; // ��������� ��������

    private Vector3 lastMousePosition; // ��������� ������� ����
    private Vector3 rotationDirection;

    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 currentMousePosition = Input.mousePosition;
                    Vector3 difference = currentMousePosition - lastMousePosition;
                    rotationDirection = new Vector3(-difference.y, difference.x, 0f);
                    objectToRotate.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
                    lastMousePosition = currentMousePosition;
                }
                else
                {
                    objectToRotate.Rotate(rotationDirection * rotationSpeed * damping * Time.deltaTime);
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentMousePosition = Input.mousePosition;
                Vector3 difference = currentMousePosition - lastMousePosition;
                rotationDirection = new Vector3(-difference.y, difference.x, 0f);
                objectToRotate.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
                lastMousePosition = currentMousePosition;
            }
            else
            {
                objectToRotate.Rotate(rotationDirection * rotationSpeed * damping * Time.deltaTime);
            }
        }
    }

    void Start()
    {
        rotationDirection = new Vector3(1, 1, 0f);
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            lastMousePosition = Input.GetTouch(0).position;
        }
        else
        {
            lastMousePosition = Input.mousePosition;
        }
    }
}
