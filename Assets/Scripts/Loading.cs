using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Rotation Speed")]
    private float m_RotationSpeed = 50f;

    private Vector3 m_RotationVelocity = default;

    private void Start()
    {
        m_RotationVelocity = new Vector3(0f, 0f, m_RotationSpeed);
    }

    private void Update()
    {
        transform.Rotate(m_RotationVelocity * Time.deltaTime);
    }
}
