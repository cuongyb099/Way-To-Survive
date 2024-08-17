using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform GunHoldPoint;
    public GunBase StartGun;
    public Rigidbody Rigidbody { get; private set; }
    public GunBase Gun { get; private set; }

    public float Speed = 5.0f;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        EquipGun(StartGun);
    }
  
    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    public void EquipGun(GunBase gun)
    {
        Gun = Instantiate(gun,GunHoldPoint.transform);
        Gun.gameObject.layer = this.gameObject.layer;
    }
    private void MovePlayer()
    {
        Rigidbody.velocity = new Vector3(0f, Rigidbody.velocity.y, 0f);
        Rigidbody.AddForce(PlayerInput.Instance.MovementInput * Speed, ForceMode.VelocityChange);
    }

    public void RotatePlayer()
    {
        Vector2 rotateInput = PlayerInput.Instance.RotationInput;
        if (rotateInput == Vector2.zero) return;
        float atan = Mathf.Atan2(rotateInput.x,rotateInput.y)*Mathf.Rad2Deg;
        Rigidbody.rotation = Quaternion.Euler(0,atan,0);
    }
}
