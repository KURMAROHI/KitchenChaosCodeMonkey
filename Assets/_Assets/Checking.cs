using UnityEngine;
using UnityEngine.UI;
// using Dotwee;

public class Checking : MonoBehaviour
{
    [SerializeField] private Transform Destination;
    [SerializeField] private Transform Source;
    [SerializeField] private float Speed = 1f;
    private Vector3 Direction;

    //1,2,5,7,10


    private void Start()
    {
        // Source.DoMove(transform.position, Destination.position)
        Direction = (Vector2)(Source.position - Destination.position).normalized;
        // Direction = Mathf.Atan2(pos.y, pos.x);
        // Debug.Log
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision|" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.GetComponent<Image>().color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.GetComponent<Image>().color = Color.blue;
        }
    }

    private void Update()
    {
        if (Vector2.Distance(Source.position, Destination.position) > 5)
        {
            Source.position += Speed * Direction * Time.deltaTime;
        }
    }

}
