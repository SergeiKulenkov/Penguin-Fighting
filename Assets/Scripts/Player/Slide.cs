using UnityEngine;

public class Slide : MonoBehaviour
{
    private bool isHitRegistered;

    private void Awake()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void SetSlideCollider(bool isSliding)
    {
        if (isSliding)
        {
            isHitRegistered = false;
            GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isHitRegistered && collider.TryGetComponent<PlayerBase>(out PlayerBase target))
        {
            isHitRegistered = true;
            transform.GetComponentInParent<PlayerBase>().DealDamage(target);
        }
    }
}
