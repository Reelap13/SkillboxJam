using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardSpawner : MonoBehaviour, IResetable
{
    [SerializeField] List<Card> cardsSetup;
    [SerializeField] List<ButtonScript> buttons;
    [SerializeField] int countOfCardToChoose;
    int cardLeftToChoose;
    void Start()
    {
        GameCondition.winGamenEvent += spawnCard;
    }
    public void resetObject()
    {
        cardLeftToChoose = countOfCardToChoose;
        foreach (var button in buttons)
        {
            if (button == null)
            {
                continue;
            }
            button.gameObject.SetActive(false);

        }
    }
    void spawnCard()
    {
        foreach (var button in buttons)
        {
            if (button == null)
            {
                continue;
            }
            button.gameObject.SetActive(true);
            var x = cardsSetup[Random.Range(0, cardsSetup.Count)];
            
            button.GetComponentInChildren<TextMeshProUGUI>().text = x.description;


            button.buttonPressEvent.RemoveAllListeners();
            button.buttonPressEvent.AddListener(x.choose);
            button.buttonPressEvent.AddListener(onCardChoose);


        }
    }
    void onCardChoose()
    {
        cardLeftToChoose--;
        if (cardLeftToChoose <= 0)
        {
            SceneController.Instance.loadNextScene();
        }
    }
    
}
