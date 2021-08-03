using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적 게임 오브젝트 2 관리. 이동 속도 및 플레이어가 Destroy 시 점수 추가.
// 적 게임 오브젝트 2의 경우, 일정 시간마다 플레이어의 Life를 감소시키는 불릿 발사.
public class Enemy2Controll : MonoBehaviour
{
    public GameManager GM;

    public int Enemy2Life;
    public float MoveX;
    public float MoveY;
    public float EnemyMoveSpeed_X;
    public float EnemyMoveSpeed_Y;
    public int Score;
    public float E_ShootTime;
    public float E_ShootDelay;

    public GameObject bomb;
    public GameObject E_Bullet;
    public Text GameScore;

    public AudioSource E_Shoot_BGM;
    public AudioClip E_ShootBGM;

    void Start()
    {
        Enemy2Life = 2;
        EnemyMoveSpeed_X = 0.2f;
        EnemyMoveSpeed_Y = 2.8f;
        E_ShootTime = 0f;
        E_ShootDelay = 1f;
    }

    void Update()
    {
        if(this.transform.position.x <= 0)
        {
            MoveX = EnemyMoveSpeed_X * -Time.deltaTime;
        } else if (this.transform.position.x >= 0)
        {
            MoveX = EnemyMoveSpeed_X * Time.deltaTime;
        }
        MoveY = EnemyMoveSpeed_Y * -Time.deltaTime;
        transform.Translate(MoveX, MoveY, 0);
        GameScore = GameObject.Find("GameManager").GetComponent<GameManager>().GameScore;

        Enemy2_Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Enemy2Life -= 1;
            if (Enemy2Life == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Score += 20;

                Instantiate(bomb, transform.position, transform.rotation);
                Destroy(gameObject, 0.01f);
            }
        }
        if (collision.gameObject.tag == "DestroyB")
        {
            Destroy(gameObject);
        }
    }
    public void Enemy2_Shoot()
    {
        if (E_ShootTime > E_ShootDelay)
        {
            Instantiate(E_Bullet, transform.position, E_Bullet.transform.rotation);
            if (!E_Shoot_BGM.isPlaying)
            {
                E_Shoot_BGM.PlayOneShot(E_ShootBGM);
            }
            E_ShootTime = 0;
        }
        E_ShootTime += Time.deltaTime;
    }
}
