using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playerMOve : MonoBehaviour
{
    public GameObject player;
    public Vector3 move;
    public float speed = 10;
    public Rigidbody rb;
    private float playerRotationX;
    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRotationX = this.transform.localRotation.eulerAngles.x;
        if (playerRotationX > 180f)
        {
            playerRotationX = playerRotationX - 360f;
        }
        
        Debug.Log(playerRotationX);
            
        
        move.x += Input.GetAxis("Mouse X");
        move.y += Input.GetAxis("Mouse Y");

        if (playerRotationX < 0)
        { speed = 10 + (playerRotationX / 9 ); }
        else { speed = 10 + (playerRotationX / 9 ); }


        transform.localRotation = Quaternion.Euler(move.y, move.x, 0);
        rb.velocity = transform.forward * speed;
    }
}
