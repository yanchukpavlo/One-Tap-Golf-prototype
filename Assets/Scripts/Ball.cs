using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] float drag = 3;
    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Push(float force)
    {
        rb2D.AddForce(new Vector2(1, 1) * force, ForceMode2D.Impulse);
        StartCoroutine(CheckCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.drag = drag;
    }

    IEnumerator CheckCoroutine()
    {
        yield return new WaitForSeconds(0.25f);

        if (rb2D.velocity.magnitude < 0.01f)
        {
            Destroy(gameObject);
            EventsManager.instance.ChangeStateTrigger(EventsManager.GameState.Menu);
        }
        else StartCoroutine(CheckCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Win"))
        {
            Destroy(gameObject);
            EventsManager.instance.ChangeStateTrigger(EventsManager.GameState.Win);
        }
        else if (other.gameObject.CompareTag("Lose"))
        {
            Destroy(gameObject);
            EventsManager.instance.ChangeStateTrigger(EventsManager.GameState.Menu);
        }
    }
}
