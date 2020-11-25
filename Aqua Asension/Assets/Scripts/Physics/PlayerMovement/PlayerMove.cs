using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PlayerMove : MonoBehaviourPunCallbacks, IPunObservable
{
    //Health and stun variables. 
    public int health = 2; 

    private float speed = 20;
    private float gravity = 8.5f;
    private float jumpForce = 5.0f;
    private Vector3 verticalVelocity;
    [SerializeField] Transform camTransform;
    [SerializeField] GameObject WaterEffect;
    //this bullet prefab is temporary and should be replaced with water later on. 
    [SerializeField] GameObject tempBulletPrefab;
    private CharacterController controller;
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject TempBullet;
    Rigidbody rb;
    float smoothVel = 0.1f; 

    public void StunDamage(int damage)
    {
        health -= damage;
        Debug.Log("health amount on hit character " + health); 

    }

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        controller = this.gameObject.GetComponent<CharacterController>();
        if(!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false; 
        }
    }
    private void Update()
    {
        if(photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("shooting");
                photonView.RPC("RPC_Shoot", RpcTarget.All);
            }
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
            controller.Move(verticalVelocity * Time.deltaTime);
            controller.Move(MoveVector * Time.deltaTime);
        }
    }

    [PunRPC]
    void RPC_Shoot()
    {
        Bullet bullet = Instantiate(TempBullet, FirePoint.position, FirePoint.rotation).GetComponent<Bullet>();
        bullet.Initialize(speed * Time.deltaTime); 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health); 
        }
        else
        {
            health = (int)stream.ReceiveNext(); 
        }
    }
}
