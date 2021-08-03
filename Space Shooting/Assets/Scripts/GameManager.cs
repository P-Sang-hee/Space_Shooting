using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 게임 전체 관리.
// 게임 화면 UI, 적 게임 오브젝트 생성, 게임 스코어 (현재, 최고 점수 등) 관리.

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    // 게임 화면 관리 (플레이 전, 플레이 중, 게임 오버)
    public GameObject MainScreen;
    public GameObject PlayScreen;
    public GameObject OverScreen;

    // 적 게임 오브젝트. 리스폰을 위해 선언
    public GameObject Enemy1;
    public GameObject Enemy2;

    // BGM 관리 (메인 브금)
    public AudioSource Main_BGM;
    public AudioClip MainBGM;

    // 게임 화면 관리를 위한 체크
    public bool GameStart;
    public bool GameOver;
    public bool GameRestart;

    // 적 게임 오브젝트 관리 (리스폰 시간, 리스폰 위치)
    public float Enemy1_Respon; // 적 게임 오브젝트 1 리스폰 시간.
    public float Enemy2_Respon; // 적 게임 오브젝트 2 리스폰 시간.
    public float E_RandomX;
    public float E_StaticY;
    public float E_StaticZ;

    // 게임 난이도.
    public float E1_R_Speed; // 적 게임 오브젝트 1 리스폰 간격 (속도)
    public float E2_R_Speed; // 적 게임 오브젝트 2 리스폰 간격 (속도)
    public float GameTime; // 게임 플레이 타임 체크용 변수

    // 점수 관리
    public int Score;
    public int HighScore;

    // 스코어 출력
    public Text GameScore; // 게임 플레이 중의 게임 점수 출력 텍스트
    public Text LastGameScore; // 게임 오버 후 해당 판의 게임 점수 출력 텍스트
    public Text HighGameScore; // 게임 오버 후 최고 점수 출력 텍스트

    // Item 관리 (아이템 스폰 위치, 스폰 간격, 아이템 획득 여부 확인)
    public GameObject HP_Item;
    public float Item_X;
    public float Item_Y;
    public float Item_Z;
    public float Item_Time;
    public bool Item_Get;

    void Start()
    {
        // 게임 시작 시, 메인 화면을 보이게 하고 나머지 화면은 꺼둠
        MainScreen.SetActive(true);
        PlayScreen.SetActive(false);
        OverScreen.SetActive(false);

        // 게임 시작 전, 전체 값 false 지정 및 게임 오브젝트가 움직이지 않도록 timeScale을 0으로 조정.
        GameStart = false;
        GameOver = false;
        GameRestart = false;
        Time.timeScale = 0f;

        // 적 게임 오브젝트의 리스폰 타임 리셋.
        Enemy1_Respon = 0f;
        Enemy2_Respon = 0f;

        // 적 게임 오브젝트의 고정된 리스폰 위치 (Y, Z) 지정
        E_StaticY = 6.5f;
        E_StaticZ = -4f;

        // 적 게임 오브젝트 리스폰 간격 시간 첫 선언
        E1_R_Speed = 3.5f;
        E2_R_Speed = 4f;
        GameTime = 0f;

        // 아이템 스폰 간격, 아이템 Get 여부, 아이템 Z 값 (0 이상일 시 화면에 보이지 않음)
        Item_Time = 0f;
        Item_Get = false;
        Item_Z = -1;
    }

    void Update()
    {
        // 키보드의 Enter를 누르면 게임이 시작하도록.
        if (GameRestart==false && GameOver==false && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        // 게임 시작 후, 플레이 화면
        if (GameStart == true && GameOver==false)
        {
            GameRestart = false;

            Time.timeScale = 1.0f;
            MainScreen.SetActive(false);
            PlayScreen.SetActive(true);
            if (!Main_BGM.isPlaying)
            {
                Main_BGM.PlayOneShot(MainBGM);
            }
        }

        // 게임 오버 후, 게임 오버 화면
        if (GameStart == true && GameOver == true)
        {
            GameStart = false;
            
            // 하이스코어 기록
            if (Score > PlayerPrefs.GetInt("H_Score"))
            {
                HighScore = Score;
                PlayerPrefs.SetInt("H_Score",HighScore);
                PlayerPrefs.Save();
            }
            
            // 게임 스코어 출력
            HighGameScore.text = "Score: " + PlayerPrefs.GetInt("H_Score");
            LastGameScore.text = "Score: " + Score;

            // 게임 정지 후, 플레이 화면을 끄고 게임 오버 화면 출력. BGM 뮤트.
            Time.timeScale = 0f;
            PlayScreen.SetActive(false);
            OverScreen.SetActive(true);
            if (Main_BGM.isPlaying)
            {
                Main_BGM.mute = true;
            }
        }

        // 게임 재시작
        if(GameStart ==false && GameOver==true && Input.GetKeyDown(KeyCode.Return))
        {
            ReloadGame();
        }

        // 적 게임 오브젝트 리스폰
        Enemy1_Respon += Time.deltaTime;
        Enemy2_Respon += Time.deltaTime;

        // GameTime 변수에 게임 플레이 시간 삽입.
        GameTime += Time.deltaTime;

        // 게임 플레이 시간이 30초가 넘을 때 마다 리스폰 간격(속도)를 줄여 난이도 상승.
        if (GameTime >= 30 && E1_R_Speed >=1 && E2_R_Speed >=1.5)
        {
            E1_R_Speed -= 0.5f;
            E2_R_Speed -= 0.5f;
            GameTime = 0;
        }

        // 적1 게임 오브젝트 리스폰 후, 리스폰 타임 0으로 리셋.
        if (Enemy1_Respon >= E1_R_Speed)
        {
            E_RandomX = UnityEngine.Random.Range(-3f, 3f);
            Instantiate(Enemy1, new Vector3(E_RandomX, E_StaticY, E_StaticZ), transform.rotation);
            Enemy1_Respon = 0;
        }
        // 적2 게임 오브젝트 리스폰 후, 적2 리스폰 타임 0으로 리셋
        if (Enemy2_Respon >= E2_R_Speed)
        {
            E_RandomX = UnityEngine.Random.Range(-3f, 3f);
            Instantiate(Enemy2, new Vector3(E_RandomX, E_StaticY, E_StaticZ), transform.rotation);
            Enemy2_Respon = 0;
        }

        // 스코어 출력
        GameScore.text = "Score: " + Score;

        // 플레이어의 라이프가 3일 시, 특정 시간마다 아이템 생성
        Item_Time += Time.deltaTime;

        Item_X = UnityEngine.Random.Range(-2.9f, 2.9f);
        Item_Y = UnityEngine.Random.Range(-3.8f, 3.8f);

        if (GameObject.Find("GamePlaying").GetComponent<PlayerLife>().Lifes <= 2
            && Item_Time >= 15f && Item_Get == false)
        {
            Instantiate(HP_Item, new Vector3(Item_X, Item_Y, Item_Z), transform.rotation);
            Item_Time = 0f;
        }
    }

    // 게임 시작 시 게임 오버와 게임 시작의 bool 값 지정.
    public void StartGame()
    {
        GameOver = false;
        GameStart = true;
    }

    // 게임 재시작. (씬 리로드)
    public void ReloadGame()
    {
        SceneManager.LoadScene("001.MainScene");
    }
}
