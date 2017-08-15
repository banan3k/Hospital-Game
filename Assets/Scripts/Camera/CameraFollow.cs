using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    private bool repositionCam = true;
    private Vector3 offset;

    private TurnHandler turnHandler;

    public bool clicked = false;
    public bool cameraLock = true;
    public bool doctorInFlight = false;

    private void Start()
    {
        if (Players.GetCameraOffset().Equals(new Vector3(0, 0, 0)))
        {
            offset = transform.position - target.position;
            Players.SetCameraOffset(offset);
        }
        else
        {
            offset = Players.GetCameraOffset();
        }
            
        turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
    }

    private void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        if (doctorInFlight)
            GetComponent<Camera>().orthographicSize = 5;
        else
            GetComponent<Camera>().orthographicSize = 10;

        // Repository the camera to the actual player (Next player)
        if (repositionCam)
        {
            if (cameraLock)
            {
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

                if (target.position.x < transform.position.x && target.position.y < transform.position.y)
                {
                    clicked = false;
                    repositionCam = false;
                }
            }
        } 
        else
        {
            if (cameraLock) { 
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
                if (target.position.x < transform.position.x && target.position.y < transform.position.y)
                {
                    clicked = false;
                   // repositionCam = false;
                }
            }
        } 
    }

    /*void Update()
    {
        Move();
    }*/

    public void SetTarget(GameObject target)
    {
        this.target = target.transform;
        repositionCam = true;
    }

    public void SetRepositionCamera(bool repositionCamera)
    {
        this.repositionCam = repositionCamera;
    }

    /*void Move()
    {
        if (!turnHandler.GetActivePlayer().GetComponentInChildren<PlayerMovement>().PlayerReachedTarget())
            return;

        if (Input.mousePosition.x < 30)
        {
            cameraLock = false;
            transform.position += Vector3.left * 20 * Time.deltaTime;
        }
        if (Input.mousePosition.x > Screen.width - 30)
        {
            cameraLock = false;
            transform.position += Vector3.right * 20 * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - 30)
        {
            cameraLock = false;
            transform.position += Vector3.up * 20 * Time.deltaTime;
        }
        if (Input.mousePosition.y < 30)
        {
            cameraLock = false;
            transform.position += Vector3.down * 20 * Time.deltaTime;
        }
        if (clicked)
        {
            cameraLock = true;
        }
    }*/

    public void SetDoctorInFlight(bool inFlight)
    {
        this.doctorInFlight = inFlight;
    }

    public Vector3 GetOffset()
    {
        return offset;
    }

    private void PreventClipping()
    {
        /*var relativePos = transform.position - (target.position);
        var hit = RaycastHit;
        if (Physics.Raycast(target.position, relativePos, hit, distance + 0.5))
        {
            Debug.DrawLine(target.position, hit.point);
            distanceOffset = distance - hit.distance + 0.8;
            distanceOffset = Mathf.Clamp(distanceOffset, 0, distance);
        }
        else
        {
            distanceOffset = 0;
        }*/
    }
}