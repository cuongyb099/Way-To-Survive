using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunMiniUI : MonoBehaviour
{
    public TextMeshProUGUI GunName;
    public Image GunImage;
    public Button GunButton { get; private set; }
    public GunBase GunHolder { get;private set; }

    private void Awake()
    {
        GunButton = GetComponent<Button>();
    }

    public void Initialize(GunBase gun)
    {
        if (gun == null)
        {
            GunHolder = null;
            GunName.text = "";
            GunImage.sprite = null;
            GunImage.color = new Color(0, 0, 0, 0);
            return;
        }
        GunHolder = gun;
        GunName.text = gun.GunData.GunName;
        GunImage.sprite = gun.GunData.Icon;
        GunImage.color = new Color(1, 1, 1, 1);
    }
}
