using UnityEngine;
using System.Collections;

public class CannonballCollision : MonoBehaviour {

    FireCannonball parent;

    void Awake()
    {
        parent = GetComponentInParent<FireCannonball>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name != "Cannonball")
        {
            parent.hasCollided();
        }
    }

}
