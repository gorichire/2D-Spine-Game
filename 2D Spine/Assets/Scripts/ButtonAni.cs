using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAni : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleUp = 1.1f;
    public float duration = 0.2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.UIMouse, this.transform.position);
        transform.DOScale(originalScale * scaleUp, duration).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, duration).SetEase(Ease.OutBack).SetUpdate(true);
    }

    void OnDestroy()
    {
        transform.DOKill(); 
    }
}
