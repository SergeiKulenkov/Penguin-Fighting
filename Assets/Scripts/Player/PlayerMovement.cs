using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float jumpForce;
    private float targetSpeed;
    private Vector2 speedDiff = new Vector2();
    private Rigidbody2D rigidBody;

    private const float JUMP_OFF_Y_MULTIPLIER = 0.5f;
    private const float JUMP_OFF_X_MULTIPLIER = 2.2f;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Walk(float movement)
    {
        targetSpeed = movement * speed;
        speedDiff.x = targetSpeed - rigidBody.velocity.x;
        float accelRate = (Mathf.Abs(movement) > 0.01f) ? acceleration : decceleration;
        rigidBody.AddForce(speedDiff * accelRate);
    }

    public void Jump()
    {
        rigidBody.velocity = Vector2.up * jumpForce;
    }

    public void JumpOffTarget(float direction)
    {
        rigidBody.velocity = new Vector2(direction * JUMP_OFF_X_MULTIPLIER, 1) * (jumpForce * JUMP_OFF_Y_MULTIPLIER);
    }
}
