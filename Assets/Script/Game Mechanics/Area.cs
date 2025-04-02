using UnityEngine;

public class Area : MonoBehaviour
{
    public GameObject child;
    public Animator wall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wall.Play("FadeOut");
            child.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wall.Play("FadeIn");
            child.SetActive(false);
        }
    }
}
