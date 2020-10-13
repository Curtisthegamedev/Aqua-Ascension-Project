using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    private float rotationSpeed = 0.5f;
    [SerializeField] Transform target, player;
    float mouseX, MouseY;

    private void Start()
    {
        Cursor.visible = false; 
    }

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        MouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        MouseY = Mathf.Clamp(MouseY, -35, 75);

        transform.LookAt(target);
        target.rotation = Quaternion.Euler(MouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0); 
    }
}
