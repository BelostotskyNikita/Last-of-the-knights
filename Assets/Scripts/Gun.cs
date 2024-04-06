using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    public float startTimeBtwShots;
    public Animator anim;
    public float attackRange;
    public LayerMask mask;
    public Joystick Joystick;
    public int damage;
    public float timeBtwShots;

    //private Quaternion rotation;
    private void Update()
    {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timeBtwShots <= 0)
        {
            timeBtwShots = startTimeBtwShots;
            //Instantiate(bullet, shotPoint.position, rotation);
            anim.SetTrigger("attack");            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(shotPoint.position, attackRange);
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(shotPoint.position, attackRange, mask);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
