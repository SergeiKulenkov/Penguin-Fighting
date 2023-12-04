using UnityEngine;
using System;

public class PlayerTwo : PlayerBase
{
    public static event Action<float> OnHealthChanged;
    public static event Action OnDefeated;

    private const int OTHER_PLAYER_LAYER = 1 << 7;


    protected override void Awake()
    {
        otherPlayerLayerMask = OTHER_PLAYER_LAYER;
        base.Awake();
    }

    protected override void Update()
    {
        movement = InputManager.GetMovementInput(false);
        base.Update();

        if (InputManager.IsAttackPressed(false))
        {
            Attack();
        }

        if (InputManager.IsJumpPressed(false))
        {
            Jump();
        }
        
        if (InputManager.IsSlidePressed(false))
        {
            Slide(true);
        }
        else
        {
            Slide(false);
        }
    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged?.Invoke(health);
        if (health <= 0) OnDefeated?.Invoke();
    }
}
