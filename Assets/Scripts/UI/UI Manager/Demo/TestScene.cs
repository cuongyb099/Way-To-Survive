using UnityEngine;

public class TestScene : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowPanel("Main Panel");
    }

    [ContextMenu("Test")]
    public void Test()
    {
        UIManager.Instance.RemovePanel("Main Panel");
    }
}
