using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ZombieCharacterControl : MonoBehaviour, IDamageable
{
    public float health = 150f;

    public int playerdamage = 10;
    public float distanceFromPlayer = 2f;
    public bool isBoss = false;

    public void TakeDamage(float damage)
    {
        health -= damage;
        MoveBack();
        if (health <= 0)
        {
            m_animator.SetBool("Dead", true);
            m_currentV = 0;
            m_currentH = 0;
            m_moveSpeed = 0;
            if (isBoss)
            {
                SceneManager.LoadScene(5);
            }
            Debug.Log("Died");
        }
    }

    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;


    private NavMeshAgent agent = null;
    Vector3 vectorTarget;
    float minX, maxX, minZ, maxZ;

    [SerializeField]
    private Transform Chartarget;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;

    private Vector3 m_currentDirection = Vector3.zero;

    private float timeOfLastAttack = 0;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
        GetReference();
        GetGroundSize();
        vectorTarget = GetNewPosiotion();
        UpdateDest(vectorTarget);
    }

    private void GetReference()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (!m_animator.GetBool("Dead"))
        {
            if (Vector3.Distance(transform.position, Chartarget.transform.position) < 15)
            {
                m_moveSpeed = 2f;
                //Debug.Log("Char Target");
                UpdateDest(Chartarget.transform.position);
            }
            else
            {
                m_moveSpeed = 0.5f;
                if (vectorTarget == null)
                {
                    vectorTarget = GetNewPosiotion();
                }
                //Debug.Log(Vector3.Distance(transform.position, vectorTarget));
                if (Vector3.Distance(transform.position, vectorTarget) < 10)
                {
                    vectorTarget = GetNewPosiotion();
                    UpdateDest(vectorTarget);
                    //Debug.Log("Random Target");   
                }
            }

            //Debug.Log(agent.steeringTarget);
            DirectUpdate();
        }
    }

    void UpdateDest(Vector3 dest)
    {
        //Debug.Log(dest);
        agent.SetDestination(dest);
    }

    private void GetGroundSize()
    {
        GameObject ground = GameObject.FindWithTag("Ground");
        Renderer groundSize = ground.GetComponent<Renderer>();
        minX = -50;
        maxX = 50;
        minZ = -50;
        maxZ = 50;
        Debug.Log(minX);
        Debug.Log(maxX);
        Debug.Log("minz " + minZ);
        Debug.Log("maxz " + maxZ);
    }

    Vector3 GetNewPosiotion()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomZ = UnityEngine.Random.Range(minZ, maxZ);
        Vector3 newPosition = new Vector3(randomX, transform.position.y, randomZ);
        return newPosition;
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);

            float distanceFromCharacter = Vector3.Distance(Chartarget.transform.position, transform.position);
            Debug.Log(distanceFromCharacter);
            if (distanceFromCharacter < distanceFromPlayer)
            {
                AttackTarget();
            }
        }
    }


    private void AttackTarget()
    {
        if (Time.time >= timeOfLastAttack + 2f)
        {
            timeOfLastAttack = Time.time;
            m_animator.SetTrigger("Attack");
            PlayerHealth.damagePlayer(this.playerdamage);
        }
    }
    public void MoveBack()
    {
        if (!m_animator.GetBool("Dead"))
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Transform camera = Camera.main.transform;

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);


            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * -1f;

                m_animator.SetFloat("MoveSpeed", 0);

            }
        }
    }
}
