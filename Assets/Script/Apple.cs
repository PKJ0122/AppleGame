using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Apple : MonoBehaviour
{
    bool _select;
    public bool Select
    {
        get { return _select; }
        set 
        { 
            _select = value;
            _outLine.enabled = value;
        }
    }

    int _amount;

    public int Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            _text.text = value.ToString();
        }
    }

    Outline _outLine;
    TMP_Text _text;


    void Awake()
    {
        _outLine = GetComponent<Outline>();
        _text = transform.Find("Text (TMP) - Amount").GetComponent<TMP_Text>();
    }
}
