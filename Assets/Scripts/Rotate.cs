using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
   public Transform objectToRotate; // Ссылка на объект, который нужно вращать
    public float rotationSpeed = 5f; // Скорость вращения
    public float damping = 0.5f; // Затухание вращения

    private Vector3 lastMousePosition; // Последняя позиция мыши
    private Vector3 rotationDirection;

    public float sensitivity = 5.0f;

    // Переменные для хранения текущих координат мыши
    private float mouseX;
    private float mouseY;

    // Переменная для хранения начальных координат мыши
    private float initialX;
    private float initialY;

    private bool isRotating = false;

    void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    mouseX = touch.position.x;
                    mouseY = touch.position.y;

                    float deltaX = mouseX - initialX;
                    float deltaY = mouseY - initialY;

                    float rotationX = deltaY * sensitivity * Time.deltaTime;
                    float rotationY = -deltaX * sensitivity * Time.deltaTime;
                    rotationDirection = new Vector3(rotationY, rotationX, 0f);

                    objectToRotate.Rotate(Vector3.up, rotationY * rotationSpeed, Space.World);
                    objectToRotate.Rotate(Vector3.right, rotationX * rotationSpeed, Space.World);

                    initialX = mouseX;
                    initialY = mouseY;
                }
                else
                {
                    objectToRotate.Rotate(Vector3.up, rotationDirection.x * damping, Space.World);
                    objectToRotate.Rotate(Vector3.right, rotationDirection.y * damping, Space.World);
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                mouseX = Input.mousePosition.x;
                mouseY = Input.mousePosition.y;

                float deltaX = mouseX - initialX;
                float deltaY = mouseY - initialY;

                float rotationX = deltaY * sensitivity * Time.deltaTime;
                float rotationY = -deltaX * sensitivity * Time.deltaTime;
                rotationDirection = new Vector3(rotationY, rotationX, 0f);

                objectToRotate.Rotate(Vector3.up, rotationY * rotationSpeed, Space.World);
                objectToRotate.Rotate(Vector3.right, rotationX * rotationSpeed, Space.World);

                initialX = mouseX;
                initialY = mouseY;
            }
            else
            {

                objectToRotate.Rotate(Vector3.up, rotationDirection.x * damping, Space.World);
                objectToRotate.Rotate(Vector3.right, rotationDirection.y * damping, Space.World);
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
