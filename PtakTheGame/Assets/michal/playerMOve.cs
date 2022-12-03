using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playerMOve : MonoBehaviour
{
    public GameObject player;
    public Vector3 move;
    public float secondFlySpeed;
    public float speed = 10;
    public Rigidbody rb;
    private float playerRotationX;
    public bool flying;
    void Start()
    {
        flying = true;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(GlobalVariables.Instance.playerState == "Fly"){
                move.x += Input.GetAxis("Mouse X");
                move.y += Input.GetAxis("Mouse Y");
        
            playerRotationX = this.transform.localRotation.eulerAngles.x;
            if (playerRotationX > 180f)
            {
                playerRotationX = playerRotationX - 360f;
            }

            

            if (flying == false)
            {
                rb.useGravity = true;
                speed = 0;
                transform.localRotation = Quaternion.Euler(0, move.x, 0);
            }
            

            if (flying == true)
            {
                transform.localRotation = Quaternion.Euler(move.y, move.x, 0);
                if (playerRotationX < 0)
                { speed = 10 + (playerRotationX / 9); }
                else { speed = 10 + (playerRotationX / 9); }

                if(GlobalVariables.Instance.playerState == "SecondFly"){
                    rb.velocity = transform.forward * secondFlySpeed;
                } else {
                    rb.velocity = transform.forward * speed;
                }
            }
        }

        Debug.Log(rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "terrain")
        {
            flying = false;
        }
    }

    // public void DisableCollider(){
    //     GetComponent<BoxCollider>().enabled = false;
    // }

    // public void FirstFly(Quaternion dir){
    //     //transform.localPosition = transform.localPosition + transform.forward * 2f;
    //     move = dir.eulerAngles;
    //     //StartCoroutine(ReenableCollider());
    // }

    // private IEnumerator ReenableCollider(){
    //     yield return new WaitForSeconds(1f);
    //     GetComponent<BoxCollider>().enabled = true;
    // }
}
