using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PlayerMove : MonoBehaviourPunCallbacks
{
    private float speed = 5;
    private float gravity = 8.5f;
    private float jumpForce = 5.0f;
    private Vector3 verticalVelocity;
    [SerializeField] Transform camTransform;
    [SerializeField] GameObject WaterEffect;
    //this bullet prefab is temporary and should be replaced with water later on. 
    [SerializeField] GameObject tempBulletPrefab;
    private CharacterController controller;
    Rigidbody rb;
    float smoothVel;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        controller = this.gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if(photonView.IsMine)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 MoveVector = new Vector3(horizontal, 0.0f, vertical).normalized;
            if (MoveVector.magnitude >= 0.1)
            {
                float angle = Mathf.Atan2(MoveVector.x, MoveVector.y) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVel, 0.1f);
                Vector3 moveDirection = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;
                transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            }

            if (controller.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    verticalVelocity.y = jumpForce;
                }

            }
            else
            {
                verticalVelocity.y += -gravity * Time.deltaTime;
            }
            controller.Move(MoveVector * Time.deltaTime);
            controller.Move(verticalVelocity * Time.deltaTime);
        }
    }
}
