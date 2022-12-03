using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class playerMOve : MonoBehaviour
{
    
    public GameObject border;
    public TMP_Text zebrane;
    public GameObject cameraBlock;
    public GameObject kurnik1,kurnik2,kurnik3;
    public Vector3 Offset = new Vector3(0, 10, 0);
    public TMP_Text text;
    public GameObject player;
    public Vector3 move;
    public float speed = 10;
    public Rigidbody rb;
    private float playerRotationX;
    public float force = 500;
    private bool zasieg;
    private float KtoryKurnik;
    private bool zebrane1,zebrane2,zebrane3;
    
    void Start()
    {
        
        zebrane.enabled = false;
        cameraBlock.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        text.enabled = false;
        rb.useGravity = false;
        
    }


    private IEnumerator wait(GameObject g, float w)
    {
        rb.isKinematic = true;
        text.enabled = false;
        zebrane.text = ("zebrales dane z kurnika nr "+ KtoryKurnik.ToString());
        zebrane.enabled = true;
        cameraBlock.SetActive(true);
        Destroy(g);       
        yield return new WaitForSeconds(1);
        zebrane.enabled = false ;
        rb.isKinematic=false;
        cameraBlock.SetActive(false);
        zasieg = false;
        
    }

    // Update is called once per frame


     
    void Update()
    {

        if(Input.GetKey(KeyCode.Escape))Application.Quit();




        if (GlobalVariables.Instance.playerState == "Fly")
        {
            move.x += Input.GetAxis("Mouse X");
            move.y += Input.GetAxis("Mouse Y");


            if (rb.useGravity == false)
            {
                playerRotationX = this.transform.localRotation.eulerAngles.x;
                if (playerRotationX > 180f)
                {
                    playerRotationX = playerRotationX - 360f;

                }
                transform.localRotation = Quaternion.Euler(move.y, move.x, 0);
                if (playerRotationX < 0)
                { speed = 10 + (playerRotationX / 9); }
                else
                {
                    speed = 10 + (playerRotationX / 9);

                }
                rb.velocity = transform.forward * speed;
            }
                if (zasieg == true)
                {


                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        switch (KtoryKurnik)
                        {
                            case 1: StartCoroutine(wait(kurnik1, KtoryKurnik)); zebrane1 = true; break;
                            case 2: StartCoroutine(wait(kurnik2, KtoryKurnik)); zebrane2 = true; break;
                            case 3: StartCoroutine(wait(kurnik3, KtoryKurnik)); zebrane3 = true; break;
                        }



                    }
                }

                if (zebrane1 == true && zebrane2 == true && zebrane3 == true)
                {
                    zebrane.text = "ZEBRALES WSZYSTKO TERAZ UCIEKAJ";
                    zebrane.enabled = true;
                    border.SetActive(false);
                }

            

        }
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="karmnik")
        {
            zasieg = true;
            KtoryKurnik = 1;
            text.enabled = true;
        }
        if (other.gameObject.tag == "karmnik2")
        {
            zasieg = true;
            KtoryKurnik = 2;
            text.enabled = true;
        }
        if (other.gameObject.tag == "karmnik3")
        {
            zasieg = true;
            KtoryKurnik = 3;
            text.enabled = true;
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "karmnik" || other.gameObject.tag == "karmnik2" || other.gameObject.tag == "karmnik3")
        {
            zasieg = false;
            text.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag != "border")
        {
            rb.useGravity = true;
            zebrane.text = "MISSION FAILED! PRESS ESC TO QUIT";
            zebrane.enabled = true;
        }
          

    }
}
