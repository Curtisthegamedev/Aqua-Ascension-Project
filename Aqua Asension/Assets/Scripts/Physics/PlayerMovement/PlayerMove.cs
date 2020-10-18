using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 10;
    private float gravity = 8.5f;
    private float jumpForce = 5.0f; 
    private float verticalVelocity;
    [SerializeField] GameObject WaterEffect;
    //this bullet prefab is temporary and should be replaced with water later on. 
    [SerializeField] GameObject tempBulletPrefab; 
    private CharacterController controller; 
    Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        controller = this.gameObject.GetComponent<CharacterController>(); 
    }

    private void Shoot()
    {
        if(Input.GetKey(KeyCode.E))
        {
            //Debug.Log("shooting water"); 
            //WaterEffect.SetActive(true); 

        }
        else
        {
            //WaterEffect.SetActive(false); 
        }
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Shoot(); 
        if(controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime; 
            if(Input.GetButton("Jump"))
            {
                verticalVelocity = jumpForce; 
            }
            
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; 
        }
        Vector3 MoveVector = new Vector3(vertical * speed, verticalVelocity, -horizontal * speed);
        controller.Move(MoveVector * Time.deltaTime); 
    }
}
