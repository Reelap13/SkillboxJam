using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private Weapon weapon;

    private void Awake()
    { 
        weapon.GetComponent<Weapon>();

        weapon.WeaponAttack.OnMakeAttack.AddListener(MakeSound);
    }

    public void MakeSound()
    {
        source.Play();
    }


}
