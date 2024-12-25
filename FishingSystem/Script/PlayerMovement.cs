using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    void Update()
    {
        //d��me h�z�m�z� s�f�rlamak i�in yere �arp�p �arpmad���m�z� kontrol ediyoruz, aksi takdirde bir dahaki sefere daha h�zl� d��ece�iz
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //sa� k�rm�z� eksen, ileri mavi eksen
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        // oyuncunun z�playabilmesi i�in yerde olup olmad���n� kontrol ediyoruz
        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            //atlama matemati�i
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
