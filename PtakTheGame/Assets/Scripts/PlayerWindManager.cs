using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindManager : MonoBehaviour
{
   void OnCollisionEnter(Collision other)
     {
         if (other.gameObject.tag == "Wind")
         {
             Destroy(other.gameObject);
         }
     }
}
