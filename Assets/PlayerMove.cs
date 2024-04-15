using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading;

public class PlayerMove : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Capsule;
    public GameObject Player;
    public GameObject ObstacleBar;
    public GameObject MovingObstacle;
    public TextMeshProUGUI ScoreTextPro;
    public TextMeshProUGUI TimeTextPro;
    public float speed = 0.05f;
    public float difficulty = 1f;
    public System.Random rand = new System.Random();
    public System.Diagnostics.Stopwatch stopWatch;
    int z = 10;
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(Floor, new Vector3(0, 0, i * 10), Quaternion.Euler(0, 90, 0));
        }

        transform.position = new Vector3(-1f, 1f, -4.5f);

        Instantiate(Capsule, new Vector3(rand.Next(-7, 7), 1, 2 + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));
        Instantiate(Capsule, new Vector3(rand.Next(-7, 7), 1, 2 + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));

        Instantiate(ObstacleBar, new Vector3(rand.Next(-7, 7), 1, 10 + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));
        Instantiate(MovingObstacle, new Vector3(rand.Next(-7, 7), 1, 10 + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));
        
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 0.5f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, 0.5f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.left, 0.5f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.right, 0.5f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.up * 0.06f;
        }

        TimeTextPro.text = "Time: " + (stopWatch.ElapsedMilliseconds/1000);

        if(z > 30 && (stopWatch.ElapsedMilliseconds/1000) > .75*transform.position.z)
        {
            TotalScore.finalMessage = "Game Over! Too Slow!\nFinal Score: " + (TotalScore.score);
            SceneManager.LoadScene(1);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Capsule")
        {
            z += 10;
            Instantiate(Floor, new Vector3(0, 0, z), Quaternion.Euler(0, 90, 0));
            Destroy(collision.gameObject);
            TotalScore.score++;

            Instantiate(MovingObstacle, new Vector3(rand.Next(-7, 7), 1, z + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));

            if(TotalScore.score % 5 == 0)
            {
                speed += 0.01f;
                difficulty += 0.25f;
            }

            for(int i = 0; i < difficulty; i++)
            {
                Instantiate(Capsule, new Vector3(rand.Next(-7, 7), 1, z + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));
                Instantiate(ObstacleBar, new Vector3(rand.Next(-7, 7), 1, z + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));
            }

            ScoreTextPro.text = "Score: " + (TotalScore.score);
        }

        if(collision.gameObject.tag == "Bar")
        {
            Destroy(collision.gameObject);
            TotalScore.score--;

            Instantiate(Floor, new Vector3(0, 0, z), Quaternion.Euler(0, 90, 0));
            Instantiate(ObstacleBar, new Vector3(rand.Next(-7, 7), 1, z + rand.Next(-6, 6)), Quaternion.Euler(0, 90, 0));

            ScoreTextPro.text = "Score: " + (TotalScore.score);
        }

        if(collision.gameObject.tag == "MovingObstacle")
        {
            TotalScore.finalMessage = "Game Over!\nFinal Score: " + (TotalScore.score);
            SceneManager.LoadScene(1);
        }
    }
}