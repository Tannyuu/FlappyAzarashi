using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzarashiController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;//ジャンプの高さの上限
    public float flapVelocity;
    public float relativeVelocityX;//3の速度で飛んでいる
    public GameObject sprite;

    public bool IsDead()
    {
        return isDead;
    }

    void Awake()//new → Awake →　Start  自分のことができる
    {
        rb2d = GetComponent<Rigidbody2D>();//自分のコンポーネントを取得
        animator = sprite.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")&& transform.position.y < maxHeight)
        {
            Flap();
        }

        ApplyAngle();

        animator.SetBool("flap", angle >= 0.0f && !isDead);
    }

    public void Flap()
    {
        if (isDead)
        {
            return;
        }

        rb2d.velocity = new Vector2(0.0f, flapVelocity);
    }

    void ApplyAngle()
    {
        float targetAngle;

        if (isDead)
        {
            targetAngle = 180.0f;
        }
        else
        {
            targetAngle = Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;//アークタンジェント 2辺の比から角度を教えてくれる ragean to degry ラジアントゥーデグリー
        }
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);//スムーズの処理 徐々に動く　第一引数、第二引数の間を第三引数の割合で返す

        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);//アザラシのz軸に値をいれてアザラシを回転させる
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
    }
}
