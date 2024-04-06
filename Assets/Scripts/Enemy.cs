using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startTimeBtwShots;
    public int health;
    public float speed;
    public float startStopTime;
    public float damage;
    
    private float normalSpeed;
    private Player player;
    private Animator anim;
    private float stopTime;
    private float timeBtwShots;
    private AddRoom room;
    private bool dead = false;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        normalSpeed = speed;
        room = GetComponentInParent<AddRoom>();
    }
    private void Update()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        } else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            if (!dead)
            {
                anim.SetTrigger("enemDeath");
            }
            dead = true;
            
        }
        if (player != null && !dead)
        {
            anim.SetBool("enemIsRunning", true);
        }
        else
        {
            anim.SetBool("enemIsRunning", false);
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(timeBtwShots <= 0 && !dead)
            {
                timeBtwShots = startTimeBtwShots;
                anim.SetTrigger("enemAttack");
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack()
    {
        if (damage > player.defence)
        {
            int tmp = (int)damage;
            tmp -= (int)player.defence;
            player.ChangeHP(tmp);
            player.ChangeDEF((int)player.defence);
        }
        else
        {
            player.ChangeDEF((int)damage);
        }
    }
    public void OnDeath()
    {
        Destroy(gameObject);
        room.enemies.Remove(gameObject);
    }
}
