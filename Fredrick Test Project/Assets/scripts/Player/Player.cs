using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //player ui Elements 
    public float _stamina = 20f;
    private float _maxStam = 20f;
    public float _health = 10f;
    public float _sneakFactor = 10f;
    private float p_staminaDropRate;
    private float p_staminaRegenRate;
    public float _staminaDropMult;
    public float _staminaRegenMult;
    bool _running;
    bool _sneaking;
    public Slider staminaBar;

    // Start is called before the first frame update
    void Start()
    {

        m_rb = GetComponent<Rigidbody>();

        _stamina = 20f;
        _health = 10f;
        _sneakFactor = 10f;

        _running = false;
        _sneaking = false;
        p_staminaDropRate = 1f;
        p_staminaRegenRate = 1f;
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

        if (_stamina > 20f)
            _stamina = 20f;

        _maxStam = Mathf.Clamp01(_maxStam);

        staminaBar.value = calcStam();
    }

    void FixedUpdate() {
        if (Sprint())
        {
            reduceStamina();
            if (_stamina > 0f)
            {
                _running = true;
            }
        } else if (Sneak())
        {
            reduceStamina();
            if (_stamina > 0f)
            {
                _sneaking = true;
            }
        } else
        {
            _sneaking = false;
            _running = false;
            Invoke("regenStamina", 4);
        }

        _stamina = Mathf.Clamp01(_stamina);

        if (_running)
        {
            m_rb.MoveRotation(Quaternion.Euler(Vector3.up * p_angle));
            m_rb.MovePosition(m_rb.position + p_velocity * 2 * Time.deltaTime);
        }
        else if (_sneaking)
        {
            m_rb.MoveRotation(Quaternion.Euler(Vector3.up * p_angle));
            m_rb.MovePosition(m_rb.position + p_velocity / 2 * Time.deltaTime);
        } else
        {
            m_rb.MoveRotation(Quaternion.Euler(Vector3.up * p_angle));
            m_rb.MovePosition(m_rb.position + p_velocity * Time.deltaTime);
        }

    }

    bool Sprint()
    {
        if(_stamina > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        return false;
    }

    bool Sneak()
    {
        if (_stamina > 0 && Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }
        return false; 
    }
    void reduceStamina()
    {
        _stamina -= Time.deltaTime / p_staminaDropRate;
    }
    void regenStamina()
    {
        _stamina += Time.deltaTime / p_staminaRegenRate;
    }
    float calcStam()
    {
        return _stamina / _maxStam;
    }
}
