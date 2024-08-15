using ResilientCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        RotatePlayer();
    }
    public void RotatePlayer()
    {
        Vector2 rotateInput = PlayerInput.Instance.RotationInput;
        if (rotateInput == Vector2.zero) return;
        float atan = Mathf.Atan2(rotateInput.x,rotateInput.y)*Mathf.Rad2Deg;
        Rigidbody.rotation = Quaternion.Euler(0,atan,0);
        Debug.Log(atan *Mathf.Rad2Deg); 
    }
}
