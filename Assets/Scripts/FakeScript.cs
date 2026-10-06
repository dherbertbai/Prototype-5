using UnityEngine;
using UnityEngine.InputSystem;

public class FakeScript : MonoBehaviour
{
    public GameScript game;
    public GameObject player;
    public InputActionReference action;

    public Rigidbody2D rb;

    public int randX;
    public int randY;

    public bool canUp;
    public bool canDown;
    public bool canLeft;
    public bool canRight;
    
    public float Speed;

    public float time;
    public float count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        randX = Random.Range(0,6);
        randY = Random.Range(0,6);
        count = Random.Range(1,3);
        canUp = true;
        canDown = true;
        canLeft = true;
        canRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        game = GameObject.Find("Game Manager").GetComponent<GameScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        time += Time.deltaTime;

        if (time > count)
        {
            randX = Random.Range(0,6);
            randY = Random.Range(0,6);
            count = Random.Range(0.5f,3);
            time = 0;
        }
        Vector2 vel = Vector2.zero;
        if (randX == 0)
        {
            if (canRight)
            {
                vel.x = Speed;
            }
            else
            {
                randX = Random.Range(0,6);
            }
        }
        else if (randX == 1)
        {
            if (canLeft)
            {
                vel.x = -Speed;
            }
            else
            {
                randX = Random.Range(0,6);
            }
        }

        if (randY == 0)
        {
            if (canUp)
            {
                vel.y = Speed;
            }
            else
            {
                randY = Random.Range(0,6);
            }
        }
        else if (randY == 1)
        {
            if (canDown)
            {
                vel.y = -Speed;
            }
            else
            {
                randY = Random.Range(0,6);
            }
        }
        rb.linearVelocity = vel;

        if (transform.position.x >= 7.5f)
        {
            canRight = false;
        }
        else
        {
            canRight = true;
        }
        if (transform.position.x <= -7.5f)
        {
            canLeft = false;
        }
        else
        {
            canLeft = true;
        }

        if (transform.position.y >= 4.5f)
        {
            canUp = false;
        }
        else
        {
            canUp = true;
        }

        if (transform.position.y <= -4.5f)
        {
            canDown = false;
        }
        else
        {
            canDown = true;
        }

        if (game.phase == GamePhase.ending)
        {
            Destroy(gameObject);
        }

        if (player == null && game.phase == GamePhase.started)
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        action.action.started += Guess;
    }
    private void OnDisable()
    {
        action.action.started -= Guess;
    }

    private void Guess(InputAction.CallbackContext obj)
    {
        Destroy(gameObject);
    }
    
}
