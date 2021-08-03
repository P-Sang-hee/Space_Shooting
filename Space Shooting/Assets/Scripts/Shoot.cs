using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 슈팅 관리. (슈팅 간격)
public class Shoot : MonoBehaviour
{
    public GameManager GM;

    public float shootDelay;
    public float shootTime;

    public GameObject Bullet;

    public AudioSource Shoot_BGM;
    public AudioClip ShootBGM;

    void Start()
    {
        shootDelay = 0.2f;
        shootTime = 0;
    }

    void Update()
    {
        if (GM.GameStart == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ShootControl();
            }
        }
        
    }
    void ShootControl()
    {
        if (shootTime > shootDelay)
        {
            Instantiate(Bullet, transform.position, Bullet.transform.rotation);
            Shoot_BGM.PlayOneShot(ShootBGM);
            shootTime = 0;
        }
        shootTime += Time.deltaTime;
    }
}
