using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
   public void Interact()
   {
      Debug.Log("You interacted with the counter : " + transform.gameObject.name);
   }
}

