using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    private float smoothTime = 0.2f;
    private float SmoothVelocity; 
    private float speed = 5;
    [SerializeField] Transform cam; 

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0.0f, vertical).normalized;
        
        if (dir.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float SmoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref SmoothVelocity, smoothTime); 

            transform.rotation = Quaternion.Euler(0.0f, SmoothedAngle, 0.0f);
            Vector3 moveDir = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;  

            controller.Move(dir * speed * Time.deltaTime); 
        }
    }
}
