using UnityEngine;

public class Wall : MonoBehaviour
{
    //public GameObject block;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            //Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            //Instantiate(block, transform.GetChild(2).position, Quaternion.identity);
            //Instantiate(block, transform.GetChild(3).position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
