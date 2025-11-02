using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextSequence : MonoBehaviour
{
    Sequence m_sequence = null;

    void Start()
    {
        m_sequence = DOTween.Sequence()
            .OnStart(() => {
                transform.localScale = Vector3.zero;
                GetComponent<CanvasGroup>().alpha = 0;
            })
            .Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
            .Join(GetComponent<CanvasGroup>().DOFade(1, 1))
            .SetDelay(0.5f);
    }
}