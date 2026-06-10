using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrenActiveWeapon { get; private set; }

    private PlayerControls playerControls;
    private float timeBetweenAttacks;

    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.Combat.Attack.started += OnAttackStarted;
        playerControls.Combat.Attack.canceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        playerControls.Combat.Attack.started -= OnAttackStarted;
        playerControls.Combat.Attack.canceled -= OnAttackCanceled;

        playerControls.Disable();
    }

    private void Start()
    {
        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapom)
    {
        CurrenActiveWeapon = newWeapom;

        AttackCooldown();

        IWeapon weapon = CurrenActiveWeapon as IWeapon;

        if (weapon != null)
        {
            timeBetweenAttacks =
                weapon.GetWeaponInfo().weaponCooldown;
        }
    }
    public void WeaponNull()
    {
        CurrenActiveWeapon = null;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    private void OnAttackStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        StartAttacking();
    }

    private void OnAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        StopAttacking();
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrenActiveWeapon)
        {
            AttackCooldown();
            (CurrenActiveWeapon as IWeapon).Attack();
        }
    }
}
