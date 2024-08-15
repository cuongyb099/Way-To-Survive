using ResilientCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    public float Speed = 5.0f;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody.velocity = new Vector3(0f,Rigidbody.velocity.y,0f);
        Rigidbody.AddForce(PlayerInput.Instance.MovementInput* Speed,ForceMode.VelocityChange);
        Debug.Log(PlayerInput.Instance.MovementInput);
    }
}
