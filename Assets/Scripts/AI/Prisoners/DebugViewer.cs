using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugViewer : MonoBehaviour
{
    private Image _stateColor;
    private TMP_Text _idText;
    [SerializeField]private Color _cardState;
    [SerializeField]private Color _clothState;
    [SerializeField]private Color _foodState;
    
    void Awake()
    {
        _stateColor = GetComponentInChildren<Image>();
        _idText = GetComponentInChildren<TMP_Text>();
        _stateColor.color = Color.black;
    }

    public void SetID(int id)
    {
        _idText.text = id.ToString();
    }

    public void SetFoodState()
    {
        _stateColor.color = _foodState;
    }

    public void SetClothState()
    {
        _stateColor.color = _clothState;
    }

    public void SetCardState()
    {
        _stateColor.color = _cardState;
    }
}
