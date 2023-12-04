using UnityEngine;
using System;

public class PlayerOne : PlayerBase
{
    public static event Action<float> OnHealthChanged;
    public static event Action OnDefeated;

    private const int OTHER_PLAYER_LAYER = 1 << 8;


    protected override void Awake()
    {
        otherPlayerLayerMask = OTHER_PLAYER_LAYER;
        base.Awake();
    }

    protected override void Update()
    {
        movement = InputManager.GetMovementInput(true);
        base.Update();

        if (InputManager.IsAttackPressed(true))
        {
            Attack();
        }

        if (InputManager.IsJumpPressed(true))
        {
            Jump();
        }
        
        if (InputManager.IsSlidePressed(true))
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
