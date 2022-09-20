using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System;

public class GameManager : MonoBehaviour, IBlockInput
{
    [SerializeField] private LoadingWindow loadingWindow;
    [SerializeField] private WinWindow winWindow;
    [SerializeField] private Counter counter;

    [SerializeField] private List<Card> cards;

    [Inject] private ImageLoader imageLoader;

    private byte _cardOpenCount;

    private Card firstOpenCard;
    private List<Card> tmpCards;

    public bool BlockInput { get; private set; }

    private void Start()
    {
        _cardOpenCount = 0;
        counter.SetCounter(_cardOpenCount);
        firstOpenCard = null;
        tmpCards = new List<Card>(cards);
        loadingWindow.Loaded += LoadingWindow_Loaded;

        foreach(var card in cards)
        {
            card.CardOpen += Card_CardOpen;
            card.OpenCard();
        }
    }

    private void Card_CardOpen(Card card)
    {
        if(firstOpenCard == null)
        {
            firstOpenCard = card;
            return;
        }


        if (firstOpenCard.ID == card.ID)
        {
            
            firstOpenCard = null;
            _cardOpenCount++;
            counter.SetCounter(_cardOpenCount);
            if (_cardOpenCount >= 3)
            {
                winWindow.Activate();
            }

            return;
        }
        BlockInput = true;
        Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(t =>
        {
            BlockInput = false;
            firstOpenCard.CloseCard();
            firstOpenCard = null;
            card.CloseCard();
        });
    }

    private void LoadingWindow_Loaded()
    {
        loadingWindow.Loaded -= LoadingWindow_Loaded;
        InitializeCard(0);
        InitializeCard(0);
        InitializeCard(1);
        InitializeCard(1);
        InitializeCard(2);
        InitializeCard(2);

        Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(t =>
        {
            foreach (var card in cards)
            {
                card.CloseCard();
            }
        });
    }

    private void InitializeCard(byte id)
    {
        var num = UnityEngine.Random.Range(0, tmpCards.Count);
        tmpCards[num].Initialize(id, imageLoader.Sprites[id], this);
        tmpCards.RemoveAt(num);
    }
}
