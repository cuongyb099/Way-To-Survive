using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : InteractableController
{
    [SerializeField] private int buyPrice;
    
    public override void Interact(PlayerController source)
    {
        if (source.Cash < buyPrice)
        {
            DamagePopUpGenerator.Instance.CreateDamagePopUp(source.transform.position, $"Không đủ tiền!!!");
            return;
        }

        source.Cash -= buyPrice;
        base.Interact(source);
    }
}
