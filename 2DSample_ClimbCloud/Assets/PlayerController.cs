using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f; //臨界點(判斷)--手機使用

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 跳躍
        // 手機使用
        //if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0){
        // 電腦使用
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0) {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 左右移動
        int dir = 0;
        // 手機使用
        //if (Input.acceleration.x > -this.threshold) dir = 1;
        //if (Input.acceleration.x < this.threshold) dir = -1;
        // 電腦使用
        if (Input.GetKey(KeyCode.RightArrow)) dir = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) dir = -1;

        // 遊戲角色的速度
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 速度限制
        if (speedx < this.maxWalkSpeed) {
            this.rigid2D.AddForce(transform.right * dir * this.walkForce);
        }

        // 依照行進方向旋轉圖片
        if (dir != 0) {
            transform.localScale = new Vector3(dir, 1, 1);
        }

        // 依遊戲角色的速度改變動畫的速度
        if (this.rigid2D.velocity.y == 0)
        {
            // 行走
            this.animator.speed = speedx / 2.0f;
        } else {
            // 跳躍
            this.animator.speed = 1.0f;
        }

        // 跑出遊戲畫面時就回到初始畫面
        if (transform.position.y < -10) {
            SceneManager.LoadScene("GameScene");
        }
    }

    // 抵達終點
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("抵達終點！");
        SceneManager.LoadScene("ClearScene");
    }
}
