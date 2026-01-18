using UnityEngine;
using TMPro;

public class InformationViewer : MonoBehaviour, IListener
{
    [SerializeField] private TextMeshProUGUI _text;

    private string _subText;

    private void Awake()
    {
        _subText = _text.text;
    }

    public void UpdateView(float data)
    {
        string text = _subText + string.Format("{0:F0}", data);

        if (_text == null)
            return;

        _text.text = text;
    }

    public void UpdateView(int data)
    {
        string text = _subText + string.Format("{0:F0}", data);

        if (_text == null)
            return;

        _text.text = text;
    }
}