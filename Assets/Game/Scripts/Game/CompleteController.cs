using CarGo.UI.Button;
using UnityEngine;

public class CompleteController : MonoBehaviour
{
    private Canvas canvas;

    public Button ContinueButton;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        canvas.enabled = true;
    }
}
