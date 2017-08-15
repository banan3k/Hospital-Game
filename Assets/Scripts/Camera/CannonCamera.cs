using UnityEngine;
using System.Collections;

public class CannonCamera : MonoBehaviour
{
    public GameObject activeCannon;
    public CannonPosToAnim cannonPosToAnim;
    public CameraFollow cameraFollow;

    private Vector3 offset;
    private GameObject gunBarrel;

	// Use this for initialization
	void Start ()
	{
	    cameraFollow = GetComponent<CameraFollow>();
	    offset = cameraFollow.GetOffset();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (activeCannon == null)
	        return;

	    float cannonPower = cannonPosToAnim.GetCannonPower();
	    if (cannonPower > 1f)
	    {
            GetComponent<Camera>().orthographicSize = cannonPower * 2;
            transform.position += cannonPosToAnim.getBarrelEndPoint() * Time.deltaTime * .5f;
        }
	}

    public void SetCannon(GameObject cannon)
    {
        this.activeCannon = cannon;
        this.cannonPosToAnim = cannon.GetComponent<CannonPosToAnim>();
    }

    public void EmptyCannon()
    {
        this.activeCannon = null;
        this.cannonPosToAnim = null;
    }
}