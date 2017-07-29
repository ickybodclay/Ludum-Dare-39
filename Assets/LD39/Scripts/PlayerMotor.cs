using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour {
    private Rigidbody2D rb2d;

    [SerializeField] private float m_MaxSpeedX = 10f;
    [SerializeField] private float m_MaxSpeedY = 6f;
    [SerializeField] private int m_MaxHealth = 20;
    private int health;

    private bool m_IsFacingRight = true;
    private bool m_IsAttacking = false;
    [SerializeField] private float m_DashSpeed = 50f;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        Reset();
    }

    public void Reset() {
        health = m_MaxHealth;
    }

    public void Move(float moveX, float moveY) {
        if (m_IsAttacking) return;

        rb2d.velocity = new Vector2(moveX * m_MaxSpeedX, moveY * m_MaxSpeedY);

        if (moveX > 0 && !m_IsFacingRight) {
            Flip();
        }
        else if (moveX < 0 && m_IsFacingRight) {
            Flip();
        }
    }

    private void Flip() {
        m_IsFacingRight = !m_IsFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Attack() {
        if (m_IsAttacking) return;

        m_IsAttacking = true;
        rb2d.velocity = (m_IsFacingRight ? Vector3.right : Vector3.left) * m_DashSpeed;

        Invoke("ResetCooldown", .05f);
    }

    private void ResetCooldown() {
        m_IsAttacking = false;
    }
}
