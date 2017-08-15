using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
    public Transform target; // target to aim for
    public GameObject camera;

    [SerializeField] private float m_MovingTurnSpeed = 360;
    [SerializeField] private float m_StationaryTurnSpeed = 180;
    [SerializeField] private float m_GroundCheckDistance = 0.1f;

    private TurnHandler turnHandler;
    private Animator m_Animator;
    private Vector3 m_GroundNormal;
    private float m_TurnAmount;
    private float m_ForwardAmount;

    public bool hasCurrentTurn = false;
    //public bool canWalk = true;

    // Use this for initialization
    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        turnHandler = GameObject.Find("Turn Handler").GetComponent<TurnHandler>();
        agent = GetComponentInChildren<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        camera = (GameObject)GameObject.Find("Main Camera");

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target != null && hasCurrentTurn)
        {
            if (PlayerReachedTarget())
            {
                m_Animator.SetBool("IsWalking", false);
            }
            else
                m_Animator.SetBool("IsWalking", true);

		//	if(canWalk=false)
            agent.SetDestination(target.position);
			//Debug.Log("A");
            // use the values to move the character
            Move(agent.desiredVelocity, false, false);
        }
        else
        {
            // We still need to call the character's move function, but we send zeroed input as the move param.
            Move(Vector3.zero, false, false);
        }
    }

    public void SetTarget(Transform inTarget)
    {
        target = inTarget;
    }

    public void Move(Vector3 move, bool crouch, bool jump)
    {
		ApplyExtraTurnRotation();
        if (move.magnitude > 1f)
            move.Normalize();

        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;
    }

    private void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public bool HasCurrentTurn()
    {
        return hasCurrentTurn;
    }

    public void SetCurrentTurn(bool turn)
    {
        hasCurrentTurn = turn;

        if(turn == false)
            m_Animator.SetBool("IsWalking", false);
    }

    public bool CanWalk()
    {
        return true;
    }

    /*public void SetCanWalk(bool canWalk)
    {
        this.canWalk = true;
    }*/

    public bool PlayerReachedTarget()
    {
        if (!agent.isActiveAndEnabled)
            return true;

        float dist = agent.remainingDistance;
        return dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < 0.3f;
    }

    public void StopWalking()
    {
        m_Animator.SetBool("IsWalking", false);
        agent.velocity = new Vector3(0f, 0f, 0f);

        GameObject targetPicker = turnHandler.GetActivePlayer().transform.Find("PlayerTarget").gameObject;
        targetPicker.transform.position = transform.position;
        targetPicker.GetComponent<TargetPicker>().goTo();

        if(agent.enabled)
            agent.SetDestination(target.position);
    }
}