using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] List<Card> cardsSetup;
    [SerializeField] List<ButtonScript> buttons;
    [SerializeField] int countOfCardToChose;
    void Start()
    {
        GameCondition.winGamenEvent += spawnCard;
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

            button.buttonPressEvent.AddListener(x.choose);
            button.buttonPressEvent.AddListener(onCardChoose);


        }
    }
    void onCardChoose()
    {
        countOfCardToChose--;
        if (countOfCardToChose <= 0)
        {
            SceneController.Instance.loadNextScene();
        }
    }
}
