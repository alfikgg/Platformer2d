using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _targetScale;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = _targetScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = _startScale;
    }
}
