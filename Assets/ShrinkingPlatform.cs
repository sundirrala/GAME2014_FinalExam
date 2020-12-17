using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    private Vector3 startPos, startScale;
    private bool isMoving, isShrinking;
    public bool isGrounded;
    public Vector3 groundLevel;
    public RaycastHit2D hit;
    [SerializeField]
    private AudioClip shrinkClip, growClip;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        startScale = this.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isMoving)
        {
            if (this.transform.position.y < startPos.y + 0.1f)
            {
                isMoving = true;
                this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f * Time.deltaTime, transform.position.z);
            }
        }
        else
        {
            if (this.transform.position.y < startPos.y - 0.1f)
            {
                isMoving = false;
                this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f * Time.deltaTime, transform.position.z);
            }
        }
        if (isShrinking)
        {
            if (this.transform.localScale.x > 0)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - 0.3f * Time.deltaTime, this.transform.localScale.y - 0.3f * Time.deltaTime, this.transform.localScale.z);
            }
            if (this.transform.localScale.x <= 0)
            {
                isShrinking = false;
            }
        }
        else
        {
            if(this.transform.localScale.x < startScale.x)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.3f * Time.deltaTime, this.transform.localScale.y + 0.3f * Time.deltaTime, this.transform.localScale.z);
            }
        }

        if (!isGrounded)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, 5.0f);
            if (hit)
            {
                groundLevel = hit.point + Vector2.up;
                isGrounded = true;
            }
        }

        _toggle();

    }
    private void _toggle()
    {
        if (isGrounded)
        {
            transform.position = new Vector3(transform.position.x,
                groundLevel.y + Mathf.PingPong(Time.time, 1), 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isShrinking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isShrinking = false;
        }
    }




}
