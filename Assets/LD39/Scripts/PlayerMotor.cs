using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMotor : MonoBehaviour {
    private Rigidbody2D rb2d;
    private AudioSource audioSource;

    [SerializeField] private float m_MinSpeedX = 5f;
    [SerializeField] private float m_MaxSpeedX = 10f;
    [SerializeField] private float m_MaxSpeedY = 6f;
    [SerializeField] private int m_MaxHealth = 20;
    private int health;

    private bool m_IsFacingRight = true;
    private bool m_IsAttacking = false;
    [SerializeField] private float m_DashSpeed = 50f;

    [SerializeField] private AudioClip hitSfx;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        Reset();
    }

    public void Reset() {
        health = m_MaxHealth;
    }

    public void Move(float moveX, float moveY) {
        rb2d.AddForce(new Vector2(
            (moveX * m_MaxSpeedX) +
            ((m_IsFacingRight ? 1f : -1f) * m_MinSpeedX), moveY * m_MaxSpeedY) * Time.deltaTime, ForceMode2D.Impulse);

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
        rb2d.AddForce((m_IsFacingRight ? Vector3.right : Vector3.left) * m_DashSpeed, ForceMode2D.Impulse);

        Invoke("ResetCooldown", .5f);
    }

    private void ResetCooldown() {
        m_IsAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.tag == "Enemy") {
            Debug.Log("Hit!  Impact speed = " + other.relativeVelocity.magnitude);
            audioSource.clip = hitSfx;
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.Play();
        }
    }
}
