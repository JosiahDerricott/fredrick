using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer : MonoBehaviour
{
    /** DOCUMENTATION
     * p_(var name) indictates a private non viewed variable within the Inspector
     * _(var name) indicates a variable that can both be viewed and changed within the Inspector
     * m_(var name) indicates components it is refrencing to in the script
     * 
     */

    public Transform m_path;
    public Light m_spotLight;
    public Transform m_player;
    public LayerMask m_viewMask;
    public Rigidbody m_rb;
    public NavMeshAgent m_agent;

    public float _moveSpd = 20f;
    public float _waitTime = .5f;
    public float _turnSpd = 180f;
    public float _viewDist;


    float _viewAngle;

    void Start() {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
        m_rb = GetComponent<Rigidbody>();

        _viewAngle = m_spotLight.spotAngle;

        Vector3[] p_pathPoints = new Vector3[m_path.childCount];
        for(int i = 0; i < p_pathPoints.Length; i++) {
            p_pathPoints[i] = m_path.GetChild(i).position;
            p_pathPoints[i] = new Vector3(p_pathPoints[i].x, transform.position.y, p_pathPoints[i].z);
        }
        StartCoroutine(followPath(p_pathPoints));
    }
    // Update is called once per frame
    void Update() {
        //TODO eventually add the levels of detection, and add a call dog 
        if(playerInSight()) {
            Debug.Log("Player is visable");
        } else
        {
            Debug.Log("Player is not visable");
        }
    }
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(m_player.position, transform.position);
        if (playerInSight())      
        {
            //TODO more testing, find if there is a better method to have farmer target player
            StopAllCoroutines();
            m_agent.SetDestination(m_player.position);
            m_agent.speed = 20;
        }
        else
        {
            //TODO find nearest path node, and restart followpoint from nearest point
        }
    }

    IEnumerator followPath(Vector3[] p_pathPoints) {

        transform.position = p_pathPoints[0];

        int p_nextPointIndex = 1;
        Vector3 p_targetPoint = p_pathPoints[p_nextPointIndex];
        transform.LookAt(p_targetPoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, p_targetPoint, _moveSpd * Time.deltaTime);
            if (transform.position == p_targetPoint)
            {
                p_nextPointIndex = (p_nextPointIndex + 1) % p_pathPoints.Length;
                p_targetPoint = p_pathPoints[p_nextPointIndex];
                yield return new WaitForSeconds(_waitTime);
                yield return StartCoroutine(turnForward(p_targetPoint));
            }
            yield return null;
        }
    }
    IEnumerator turnForward(Vector3 p_lookTarget) {

        Vector3 p_dirToLookTarget = (p_lookTarget - transform.position).normalized;
        float p_targetAngle = 90 - Mathf.Atan2(p_dirToLookTarget.z, p_dirToLookTarget.x) * Mathf.Rad2Deg;

        while(Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, p_targetAngle)) > 0.05f) {
            float p_angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, p_targetAngle, _turnSpd * Time.deltaTime);
            transform.eulerAngles = Vector3.up * p_angle;
            yield return null;
        }
    }

    bool playerInSight() {  
        if (Vector3.Distance(transform.position, m_player.position) < _viewDist) {
            Vector3 p_playerAngle = (m_player.position - transform.position).normalized;
            float p_angleBetweenFarmer_Player = Vector3.Angle(transform.forward, p_playerAngle);
            if(p_angleBetweenFarmer_Player < _viewAngle/2f) { 
                if(!Physics.Linecast(transform.position, m_player.position, m_viewMask)) {
                    return true;
                }

            }

        }
        return false;
    }

    

    private void OnDrawGizmos() {
        Vector3 p_startPos = m_path.GetChild(0).position;
        Vector3 p_previousPos = p_startPos;
        foreach (Transform p_pathPoint in m_path) {
            Gizmos.DrawSphere(p_pathPoint.position, 1f);
            Gizmos.DrawLine(p_previousPos, p_pathPoint.position);
            p_previousPos = p_pathPoint.position;
        }
        Gizmos.DrawLine(p_previousPos, p_startPos);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay (transform.position, transform.forward * _viewDist);
    }
}
