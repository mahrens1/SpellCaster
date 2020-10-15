using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public float m_Speed = 130f;   
    public float m_Lifespan = 3f; 

    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_Rigidbody.AddForce(transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
    }
}
