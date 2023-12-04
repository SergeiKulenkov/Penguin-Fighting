using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    private Transform attackPoint;
    private Collider2D bodyCollider;
    private Collider2D attackCollider;

    [SerializeField] private int damage;
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform defeatedSprite;

    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private bool hasJumpStarted;
    private bool isAirborne;
    private bool hasJumpedOnHead;
    private float attackTimer;
    private float slideTimer;
    private Slide slide;
    private RaycastHit2D feetRaycast;
    private Vector3 scale;
    private bool isSliding;

    [SerializeField] protected float health;
    protected float movement;
    protected int otherPlayerLayerMask;

    private const string ATTACK_POINT_PATH = "AttackPoint";
    private const string SLIDE_ATTACK_POINT_PATH = "SlideAttackPoint";

    private const float GROUND_CHECK_DISTANCE = 0.1f;
    private const float PLAYER_DOWN_CHECK_DISTANCE = 0.3f;
    private const int LAYER_GROUND = 1 << 6;

    private const float ATTACK_COOLDOWN = 0.2f;
    private const float SLIDE_COOLDOWN = 0.2f;
    private const int MIN_DAMAGE_CHANGE = -5;
    private const int MAX_DAMAGE_CHANGE = 5;


    protected virtual void Awake()
    {
        attackPoint = transform.Find(ATTACK_POINT_PATH);
        slide = transform.Find(SLIDE_ATTACK_POINT_PATH).GetComponent<Slide>();
        bodyCollider = GetComponent<Collider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        scale = transform.localScale;
    }

    protected virtual void Update()
    {
        if (attackTimer > 0) attackTimer -= Time.deltaTime;
        if (slideTimer > 0) slideTimer -= Time.deltaTime;

        if ((scale.x == -1) && (movement > 0))
        {
            scale.x = 1;
            transform.localScale = scale;
        }
        else if ((scale.x == 1) && (movement < 0))
        {
            scale.x = -1;
            transform.localScale = scale;
        }

        GetFeetRaycast(GROUND_CHECK_DISTANCE, LAYER_GROUND);
        isAirborne = (feetRaycast.collider == null);
    }

    private void FixedUpdate()
    {
        playerMovement.Walk(movement);

        if (hasJumpStarted)
        {
            playerMovement.Jump();
            hasJumpStarted = false;
            isAirborne = true;
            hasJumpedOnHead = false;
        }
        if (isAirborne && !hasJumpedOnHead)
        {
            GetFeetRaycast(PLAYER_DOWN_CHECK_DISTANCE, otherPlayerLayerMask);
            if ((feetRaycast.collider != null) &&
                feetRaycast.collider.TryGetComponent<PlayerBase>(out PlayerBase target))
            {
                hasJumpedOnHead = true;
                playerMovement.JumpOffTarget(scale.x);
                DealDamage(target);
            }
        }
    }
    
    private void GetFeetRaycast(float distance, int layerMask)
    {
        feetRaycast = Physics2D.BoxCast(bodyCollider.bounds.center, bodyCollider.bounds.size, 0f,
                                    Vector2.down, distance, layerMask);
    }

    protected void Attack()
    {
        if (attackTimer <= 0)
        {
            movement = 0;
            playerAnimation.PlayAttackAnimation();
            attackCollider = Physics2D.OverlapCircle(attackPoint.position, attackRadius);
            if ((attackCollider != null) &&
                attackCollider.TryGetComponent<PlayerBase>(out PlayerBase target))
            {
                DealDamage(target);
            }
            attackTimer = ATTACK_COOLDOWN;
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Attack);
        }
    }

    protected void Jump()
    {
        if (!isAirborne)
        {
            playerAnimation.PlayJumpAnimation();
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Jump);
            hasJumpStarted = true;
        }
    }

    protected void Slide(bool slidePressed)
    {
        if ((movement != 0) && slidePressed)
        {
            if (slideTimer <= 0)
            {
                if (!isSliding)
                {
                    isSliding = true;
                    playerAnimation.ChangeWalkingAnimation(movement);
                    playerAnimation.ChangeSlidingAnimation(isSliding);
                    SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Slide);
                }
                slideTimer = SLIDE_COOLDOWN;
                slide.SetSlideCollider(isSliding);
            }
        }
        else
        {
            if (isSliding)
            {
                isSliding = false;
                playerAnimation.ChangeSlidingAnimation(isSliding);
                slide.SetSlideCollider(isSliding);
                SoundManager.Instance.StopPlaying();
            }
            playerAnimation.ChangeWalkingAnimation(movement);
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        health -= damage;
        SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Hit);

        if (health <= 0)
        {
            Transform defeated = Instantiate(defeatedSprite, transform.position, Quaternion.identity);
            defeated.localScale = scale;
            SpriteRenderer spriteThis = transform.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteDefeated = defeated.GetComponent<SpriteRenderer>();
            spriteDefeated.color = spriteThis.color;
            spriteDefeated.sortingOrder = spriteThis.sortingOrder;
            Destroy(gameObject);
        }
    }

    public void DealDamage(PlayerBase target)
    {
        int random = Random.Range(MIN_DAMAGE_CHANGE, MAX_DAMAGE_CHANGE + 1);
        target.TakeDamage(damage + random);
    }
}
