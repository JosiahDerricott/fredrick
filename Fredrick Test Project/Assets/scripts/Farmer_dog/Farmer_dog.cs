using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer_dog : MonoBehaviour
{
    /** DOCUMENTATION
     * p_(var name) indictates a private non viewed variable within the Inspector
     * _(var name) indicates a variable that can both be viewed and changed within the Inspector
     * m_(var name) indicates components it is refrencing to in the script
     * 
     */

    //body comps for farmer
    Rigidbody m_rb;
    NavMeshAgent m_agent;

    //player transform for position 
    Transform m_player;

    //Ai Hearing
    Vector3 m_noisePos;
    private bool p_playerHeard = false;
    public float _noiseDis = 50f;
    public float _noiseSpinSpd = 3f;
    private bool p_turnToPlayer = false;
    private float p_isSpinning;
    public float _spinTime = 3f;

    //Ai patrol/follow
    public LayerMask m_viewMask;
    public Light m_spotLight;
    public float _moveSpd = 20f;
    public float _waitTime = .5f;
    public float _turnSpd = 180f;
    public float _viewDist;
    float p_viewAngle;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
        m_rb = GetComponent<Rigidbody>();

        p_viewAngle = m_spotLight.spotAngle;
    }
    // Update is called once per frame
    void Update()
    {
        //TODO eventually add the levels of detection, and add a call dog 
        if (playerInSight())
        {
            Debug.Log("Player is visable");
        }
        else
        {
            //Debug.Log("Player is not visable");
        }
    }
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(m_player.position, transform.position);
        if (playerInSight())
        {
            //TODO more testing, find if there is a better method to have dog target player
            m_agent.SetDestination(m_player.position);
            m_agent.speed = 20;
        }
        else
        {
            //TODO find nearest path node, and restart followpoint from nearest point
        }
    }

    bool playerInSight()
    {
        if (Vector3.Distance(transform.position, m_player.position) < _viewDist)
        {
            Vector3 p_playerAngle = (m_player.position - transform.position).normalized;
            float p_angleBetweenFarmer_Player = Vector3.Angle(transform.forward, p_playerAngle);
            if (p_angleBetweenFarmer_Player < p_viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, m_player.position, m_viewMask))
                {
                    return true;
                }

            }

        }
        return false;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * _viewDist);
    }
}
