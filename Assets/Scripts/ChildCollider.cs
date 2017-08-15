using UnityEngine;
using System.Collections;

public class ChildCollider : MonoBehaviour {

    bool CollisionExceptions(Collider col)
    {
        if (col.gameObject.name == "Cannonball")
        {
            return true;
        }
        else if (col.gameObject.name == "Player") //player, maybe go for tags?
        {
            return true;
        }
        else
            return false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(!CollisionExceptions(col))
        {
            transform.parent.gameObject.GetComponent<CannonPosToAnim>().isColliding = true;
        }
        else
            transform.parent.gameObject.GetComponent<CannonPosToAnim>().isColliding = false;
    }

    void OnTriggerExit()
    {
        transform.parent.gameObject.GetComponent<CannonPosToAnim>().isColliding = false;
    }
}
