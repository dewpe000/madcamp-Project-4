using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class FontColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private TextMeshProUGUI textUI;
    private Color originColor;

    void Start() {
        textUI = GetComponentInChildren<TextMeshProUGUI>();
        originColor = textUI.color;
    }

    private void Update() {


    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        textUI.color = Color.white;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        textUI.color = originColor;
    }
}
