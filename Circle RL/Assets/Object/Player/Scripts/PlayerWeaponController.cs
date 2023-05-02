using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private GameObject FirstWeapon;
    [SerializeField] private GameObject SecondWeapon;
    [SerializeField] private GameObject ThirdWeapon;
    [SerializeField] private GameObject FourthWeapon;
    [SerializeField] private GameObject FifthWeapon;

    private int numberOfCurrentWeapon;

    private void Awake()
    {
        SwapWeapon(1);
    }

    public void SwapWeapon(int number)
    {
        if (numberOfCurrentWeapon == number)
            return;

        TurnOffAllWeapons();
        TurnOnWeapon(number);
    }

    private void TurnOffAllWeapons()
    {
        FirstWeapon?.SetActive(false);
        SecondWeapon?.SetActive(false);
        ThirdWeapon?.SetActive(false);
        FourthWeapon?.SetActive(false);
        FifthWeapon?.SetActive(false);
    }

    private void TurnOnWeapon(int number)
    {
        numberOfCurrentWeapon = number;
        switch (number)
        {
            case 1: 
                FirstWeapon?.SetActive(true);
                break;
            case 2:
                SecondWeapon?.SetActive(true);
                break;
            case 3:
                ThirdWeapon?.SetActive(true);
                break;
            case 4:
                FourthWeapon?.SetActive(true);
                break;
            case 5:
                FifthWeapon?.SetActive(true);
                break;
        }
    }
}
