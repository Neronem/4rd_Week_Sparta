using UnityEngine;


public abstract class BaseItem : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;

    // protected AnimatorController animator;
    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
