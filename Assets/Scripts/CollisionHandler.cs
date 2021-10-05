using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is s a friendly object");
                break;
            case "Finished":
                Debug.Log("This is the landing pad object");
                break;
            default:
                Debug.Log("You blew up");
                break;
        }
    }
}
