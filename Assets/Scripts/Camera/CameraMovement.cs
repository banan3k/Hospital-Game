using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float smoothness = 1.5f;

    private Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;

	// Use this for initialization
	void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        Vector3 standardPos = player.position + relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
        Vector3[] checkpoints = new Vector3[5];
        checkpoints[0] = standardPos;
        checkpoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkpoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkpoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
        checkpoints[4] = abovePos;

        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (ViewPosCheck(checkpoints[i]))
                break;
        }

        transform.position = Vector3.Lerp(transform.position, newPos, smoothness * Time.deltaTime);
        SmoothLookAt();
    }

    bool ViewPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
            if(hit.transform != player)
                return false;

        newPos = checkPos;
        return true;
    }

    void SmoothLookAt()
    {
        Vector3 relPlayerPosition = player.position - transform.position;
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smoothness * Time.deltaTime);
    }


}
