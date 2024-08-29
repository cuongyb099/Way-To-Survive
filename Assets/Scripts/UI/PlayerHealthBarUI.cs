using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResilientCore
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI PlayerHealthtext { get; private set; }
        [field: SerializeField] public Slider PlayerHealthSlider { get; private set; }
        PlayerController controller;
        // Start is called before the first frame update
        void Start()
        {
            controller = GameManager.Instance.Player;
            controller.OnDamaged += ChangeHealthBarValue;
            ChangeHealthBarValue();
        }
        private void OnDestroy()
        {
            controller.OnDamaged -= ChangeHealthBarValue;

        }
        public void ChangeHealthBarValue()
        {
            PlayerHealthtext.text = controller.HP + " / " + controller.Stats.StatsMap[EStatType.HP].Value;
            PlayerHealthSlider.value = controller.HP / controller.Stats.StatsMap[EStatType.HP].Value;
        }
    }
}
