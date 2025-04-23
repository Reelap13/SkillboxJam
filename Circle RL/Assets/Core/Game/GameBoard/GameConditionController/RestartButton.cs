using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] PlayerHealth _ph;
    public void showOn()
    {
        if (_ph.HitPoint <= 0)
        {
            PlayerPrefs.SetInt("Score", Mathf.Max(PlayerPrefs.GetInt("Score", 0), Card.globaleScore));
            GetComponentInChildren<TextMeshProUGUI>().text = "Your score:" + Card.globaleScore + "\nRestart?";
            gameObject.SetActive(true);
        }
    }
    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
