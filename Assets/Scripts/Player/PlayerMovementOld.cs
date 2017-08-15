using UnityEngine;

public class PlayerMovementOld : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    Quaternion rotation;
    bool walking;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        Animating(h, v);
        if (walking)
        {
            Move(h, v);
            Turning();
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Vector3 lookPos = playerRigidbody.position - transform.position;
        lookPos.y = 0;

        rotation = Quaternion.LookRotation(lookPos);
        playerRigidbody.MoveRotation(rotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    void Animating(float h, float v)
    {
        walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
