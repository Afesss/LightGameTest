using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardRenderer;

    public event System.Action<Card> CardOpen;

    private bool _cardOpened;
    public byte ID { get; private set; }
    private IBlockInput _blockInput;
    public void Initialize(byte id, Sprite image, IBlockInput blockInput)
    {
        _blockInput = blockInput;
        cardRenderer.sprite = image;
        ID = id;
    }

    public void OpenCard()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        _cardOpened = true;
    }
    public void CloseCard()
    {
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f);
        _cardOpened = false;
    }

    private void OnMouseDown()
    {
        if (_cardOpened) return;

        SmoothOpenCard();
    }

    private void SmoothOpenCard()
    {
        if (_blockInput.BlockInput) return;
        _cardOpened = true;
        transform.DORotateQuaternion(Quaternion.Euler(0, 180, 0), 0.5f);
        CardOpen?.Invoke(this);
    }
}
