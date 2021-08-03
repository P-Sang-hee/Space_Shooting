using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ���� ��ü ����.
// ���� ȭ�� UI, �� ���� ������Ʈ ����, ���� ���ھ� (����, �ְ� ���� ��) ����.

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    // ���� ȭ�� ���� (�÷��� ��, �÷��� ��, ���� ����)
    public GameObject MainScreen;
    public GameObject PlayScreen;
    public GameObject OverScreen;

    // �� ���� ������Ʈ. �������� ���� ����
    public GameObject Enemy1;
    public GameObject Enemy2;

    // BGM ���� (���� ���)
    public AudioSource Main_BGM;
    public AudioClip MainBGM;

    // ���� ȭ�� ������ ���� üũ
    public bool GameStart;
    public bool GameOver;
    public bool GameRestart;

    // �� ���� ������Ʈ ���� (������ �ð�, ������ ��ġ)
    public float Enemy1_Respon; // �� ���� ������Ʈ 1 ������ �ð�.
    public float Enemy2_Respon; // �� ���� ������Ʈ 2 ������ �ð�.
    public float E_RandomX;
    public float E_StaticY;
    public float E_StaticZ;

    // ���� ���̵�.
    public float E1_R_Speed; // �� ���� ������Ʈ 1 ������ ���� (�ӵ�)
    public float E2_R_Speed; // �� ���� ������Ʈ 2 ������ ���� (�ӵ�)
    public float GameTime; // ���� �÷��� Ÿ�� üũ�� ����

    // ���� ����
    public int Score;
    public int HighScore;

    // ���ھ� ���
    public Text GameScore; // ���� �÷��� ���� ���� ���� ��� �ؽ�Ʈ
    public Text LastGameScore; // ���� ���� �� �ش� ���� ���� ���� ��� �ؽ�Ʈ
    public Text HighGameScore; // ���� ���� �� �ְ� ���� ��� �ؽ�Ʈ

    // Item ���� (������ ���� ��ġ, ���� ����, ������ ȹ�� ���� Ȯ��)
    public GameObject HP_Item;
    public float Item_X;
    public float Item_Y;
    public float Item_Z;
    public float Item_Time;
    public bool Item_Get;

    void Start()
    {
        // ���� ���� ��, ���� ȭ���� ���̰� �ϰ� ������ ȭ���� ����
        MainScreen.SetActive(true);
        PlayScreen.SetActive(false);
        OverScreen.SetActive(false);

        // ���� ���� ��, ��ü �� false ���� �� ���� ������Ʈ�� �������� �ʵ��� timeScale�� 0���� ����.
        GameStart = false;
        GameOver = false;
        GameRestart = false;
        Time.timeScale = 0f;

        // �� ���� ������Ʈ�� ������ Ÿ�� ����.
        Enemy1_Respon = 0f;
        Enemy2_Respon = 0f;

        // �� ���� ������Ʈ�� ������ ������ ��ġ (Y, Z) ����
        E_StaticY = 6.5f;
        E_StaticZ = -4f;

        // �� ���� ������Ʈ ������ ���� �ð� ù ����
        E1_R_Speed = 3.5f;
        E2_R_Speed = 4f;
        GameTime = 0f;

        // ������ ���� ����, ������ Get ����, ������ Z �� (0 �̻��� �� ȭ�鿡 ������ ����)
        Item_Time = 0f;
        Item_Get = false;
        Item_Z = -1;
    }

    void Update()
    {
        // Ű������ Enter�� ������ ������ �����ϵ���.
        if (GameRestart==false && GameOver==false && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        // ���� ���� ��, �÷��� ȭ��
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

        // ���� ���� ��, ���� ���� ȭ��
        if (GameStart == true && GameOver == true)
        {
            GameStart = false;
            
            // ���̽��ھ� ���
            if (Score > PlayerPrefs.GetInt("H_Score"))
            {
                HighScore = Score;
                PlayerPrefs.SetInt("H_Score",HighScore);
                PlayerPrefs.Save();
            }
            
            // ���� ���ھ� ���
            HighGameScore.text = "Score: " + PlayerPrefs.GetInt("H_Score");
            LastGameScore.text = "Score: " + Score;

            // ���� ���� ��, �÷��� ȭ���� ���� ���� ���� ȭ�� ���. BGM ��Ʈ.
            Time.timeScale = 0f;
            PlayScreen.SetActive(false);
            OverScreen.SetActive(true);
            if (Main_BGM.isPlaying)
            {
                Main_BGM.mute = true;
            }
        }

        // ���� �����
        if(GameStart ==false && GameOver==true && Input.GetKeyDown(KeyCode.Return))
        {
            ReloadGame();
        }

        // �� ���� ������Ʈ ������
        Enemy1_Respon += Time.deltaTime;
        Enemy2_Respon += Time.deltaTime;

        // GameTime ������ ���� �÷��� �ð� ����.
        GameTime += Time.deltaTime;

        // ���� �÷��� �ð��� 30�ʰ� ���� �� ���� ������ ����(�ӵ�)�� �ٿ� ���̵� ���.
        if (GameTime >= 30 && E1_R_Speed >=1 && E2_R_Speed >=1.5)
        {
            E1_R_Speed -= 0.5f;
            E2_R_Speed -= 0.5f;
            GameTime = 0;
        }

        // ��1 ���� ������Ʈ ������ ��, ������ Ÿ�� 0���� ����.
        if (Enemy1_Respon >= E1_R_Speed)
        {
            E_RandomX = UnityEngine.Random.Range(-3f, 3f);
            Instantiate(Enemy1, new Vector3(E_RandomX, E_StaticY, E_StaticZ), transform.rotation);
            Enemy1_Respon = 0;
        }
        // ��2 ���� ������Ʈ ������ ��, ��2 ������ Ÿ�� 0���� ����
        if (Enemy2_Respon >= E2_R_Speed)
        {
            E_RandomX = UnityEngine.Random.Range(-3f, 3f);
            Instantiate(Enemy2, new Vector3(E_RandomX, E_StaticY, E_StaticZ), transform.rotation);
            Enemy2_Respon = 0;
        }

        // ���ھ� ���
        GameScore.text = "Score: " + Score;

        // �÷��̾��� �������� 3�� ��, Ư�� �ð����� ������ ����
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

    // ���� ���� �� ���� ������ ���� ������ bool �� ����.
    public void StartGame()
    {
        GameOver = false;
        GameStart = true;
    }

    // ���� �����. (�� ���ε�)
    public void ReloadGame()
    {
        SceneManager.LoadScene("001.MainScene");
    }
}
