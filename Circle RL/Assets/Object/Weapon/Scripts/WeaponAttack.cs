using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponAttack : MonoBehaviour
{
    [field: SerializeField]
    public MouseButton MouseButton { get; private set; }
    [SerializeField] private float timeBetweenShoots;

    [field: SerializeField]
    public Weapon Weapon { get; private set; }

    [HideInInspector] public UnityEvent OnAttackStarted = new UnityEvent();
    [HideInInspector] public UnityEvent OnMakeAttack = new UnityEvent();
    [HideInInspector] public UnityEvent OnAttackEnded = new UnityEvent();
    [HideInInspector] public UnityEvent OnAttackTick = new UnityEvent();
    [HideInInspector] public UnityEvent OnEmptyClip = new UnityEvent();



    private bool AccessToAttack => Weapon.IsAccessToAttack;
    protected int ShotCost => Weapon.WeaponPreset.shotCost;
    protected int Ammo => Weapon.WeaponAmmunition.Ammo;
    protected float Damage => Weapon.WeaponPreset.damage;
    protected TagEnum Team => Weapon.WeaponPreset.Team;


    private bool attackInProcess = false;
    private float timeAfterShoot = 0;

    private void Update()
    {
        timeAfterShoot += Time.deltaTime;

        if (Input.GetMouseButtonDown((int)MouseButton) && AccessToAttack)
            StartAttack();

        if (Input.GetMouseButtonUp((int)MouseButton) && attackInProcess)
            EndAttack();

        if (attackInProcess)
            InAttackTick();
    }

    private void StartAttack()
    {
        if (timeBetweenShoots > timeAfterShoot)
            return;

        if (Ammo >= ShotCost)
        {
            timeAfterShoot = 0;
            attackInProcess = true;
            Weapon.SpendAmmo(ShotCost);
            PerformAttack();
            OnAttackStarted.Invoke();
            OnMakeAttack.Invoke();
        }
        else
        {
            OnEmptyClip.Invoke();
        }
    }

    private void EndAttack()
    {
        attackInProcess = false;

        DisposeAttack();
        OnAttackEnded.Invoke();
    }

    private void InAttackTick()
    {
        if (timeBetweenShoots <= timeAfterShoot)
        {
            if (Ammo >= ShotCost)
            {
                timeAfterShoot = 0;
                Weapon.SpendAmmo(ShotCost);
                PerformAttack();
                OnMakeAttack.Invoke();
            }
            else
            {
                OnEmptyClip.Invoke();
            }
        }
        OnAttackTick.Invoke();
    }

    public void SetMouseButton(MouseButton mouseButton) => MouseButton = mouseButton;

    protected abstract void PerformAttack();
    protected abstract void DisposeAttack();
}


