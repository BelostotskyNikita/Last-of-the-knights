using UnityEngine;

public class MCamera : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector3 tmp = transform.position;
        tmp.x = player.position.x;
        tmp.y = player.position.y;
        transform.position = tmp;
    }
}
