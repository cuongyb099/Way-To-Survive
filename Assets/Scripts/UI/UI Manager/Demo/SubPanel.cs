using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResilientCore.UI_Manager.Demo
{
    public class SubPanel : FadePanel
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Button hide;
        protected override void OnAwake()
        {
            textMesh = GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = "This Is Sub Panel";
            hide = GetComponentInChildren<Button>();
            hide.onClick.AddListener(Hide);
        }
    }
}