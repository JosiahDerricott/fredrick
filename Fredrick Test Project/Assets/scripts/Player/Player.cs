using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /** DOCUMENTATION 
     * p_(var name) indictates a private non viewed variable within the Inspector
     * _(var name) indicates a variable that can both be viewed and changed within the Inspector
     * m_(var name) indicates components it is refrencing to in the script
     * 
     */
    public float _moveSpd = 21f;
    public float _smoothMoveTime = .1f;
    public float _turnSpd = 8f;

    float p_smoothInputMag;
    float p_smoothMoveVel;
    float p_angle;
    Vector3 p_velocity;

    Rigidbody m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p_inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float p_inputMag = p_inputDir.magnitude;
        p_smoothInputMag = Mathf.SmoothDamp(p_smoothInputMag, p_inputMag, ref p_smoothMoveVel, _smoothMoveTime);

        float p_targetAngle = Mathf.Atan2(p_inputDir.x, p_inputDir.z) * Mathf.Rad2Deg;
        p_angle = Mathf.LerpAngle(p_angle, p_targetAngle, Time.deltaTime * _turnSpd * p_inputMag);

        p_velocity = transform.forward * _moveSpd * p_smoothInputMag;
    }

    void FixedUpdate() {
        m_rb.MoveRotation(Quaternion.Euler(Vector3.up * p_angle));
        m_rb.MovePosition(m_rb.position + p_velocity * Time.deltaTime);
    }
}
