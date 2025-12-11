using UnityEngine;

public class diChuyen : MonoBehaviour
{
    public Rigidbody2D rb;

    public int tocDo = 4;

    public float traiPhai;

    public bool isFacingRight = true;

    public Animator anim;

    public int doCao;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        //move
        traiPhai = Input.GetAxisRaw("Horizontal"); // A=-1,0,D=1
        rb.linearVelocity = new Vector2(tocDo * traiPhai, rb.linearVelocity.y);

        if (isFacingRight == true && traiPhai == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
        if (isFacingRight == false && traiPhai == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }
        anim.SetFloat("dichuyen", Mathf.Abs(traiPhai));

        //attack
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("tancong");
        }

        // jump mượt
        // chỉ nhảy khi đang đứng dưới đất (velocity.y == 0)
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && Mathf.Abs(rb.linearVelocity.y) < 0.05f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, doCao);
        }

        // làm mượt khi rơi
        if (rb.linearVelocity.y < 0)   // rơi xuống
        {
            rb.gravityScale = 10f;   // rơi nhanh hơn -> tự nhiên
        }
        else
        {
            rb.gravityScale = 5f; // nhảy lên nhẹ hơn
        }

    }

} 
