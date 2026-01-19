using UnityEngine;
using TMPro;

public abstract class TextViewer : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI Text;

    protected string SubText;

    private void Awake()
    {
        SubText = Text.text;
    }
}