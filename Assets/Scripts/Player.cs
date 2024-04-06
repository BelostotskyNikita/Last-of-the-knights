using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public Joystick Joystick;
    public float health;
    public float defence;
    public HealthBar _healthBar;
    public DefenceBar _defenceBar;

    private float _maxHealth;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private int maxDefence;
    private float defTime = 5f;
    private float startDefTime = 5f;
    private Animator anim;
    private bool facingRight = true;
    private bool alive = true;
    void Start()
    {
        _maxHealth = health;
        maxDefence = (int)defence;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (alive)
        {
            moveInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);
            moveVelocity = moveInput.normalized * speed;
            if (defTime > 0)
            {
                defTime -= Time.deltaTime;
            }
            else if (defence < maxDefence)
            {
                defence++;
                _defenceBar.SetDefence((int)defence);
                defTime = startDefTime;
            }
            if (health <= 0)
            {
                alive = false;
                anim.SetTrigger("death");
            }
            if (moveInput.x == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }
            if (!facingRight && moveInput.x > 0)
            {
                Flip();
            }
            else if (facingRight && moveInput.x < 0)
            {
                Flip();
            }
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangeHP(int hp)
    {
        if (hp < 0 && _maxHealth == health)
        {
            return;
        }
        else if (hp < 0 && -hp > _maxHealth - health)
        {
            health = _maxHealth;
        }
        health -= hp;
        _healthBar.SetHealth((int)health);
    }
    public void ChangeDEF(int def)
    {
        defence -= def;
        _defenceBar.SetDefence((int)defence);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AidKid"))
        {
            ChangeHP(-1);
            Destroy(collision.gameObject);
        }
    }
}
