using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    public GameScript game;
    public new Rigidbody2D rigidbody;
    
    public float timer;
    public bool countDown;
    
    public GamePhase phase;
    public float speed;
    public Vector2 moveDirection;
    public InputActionReference action;
    public InputActionReference guess;

    public float death;

    public bool dying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = 5;
        countDown = false;
        death = 3;
        dying = false;
    }
    

    void Update()
    {
        game = GameObject.Find("Game Manager").GetComponent<GameScript>();
        moveDirection = action.action.ReadValue<Vector2>();
        if (GameScript.instance.CanMove())
        {
            rigidbody.linearVelocity = new Vector2(moveDirection.x*speed, moveDirection.y*speed);
        }

        if (transform.position.x > 8.5 || transform.position.x < -8.5 || transform.position.y > 5.5 || transform.position.y < -5.5)
        {
            countDown = true;
        }
        else
        {
            countDown = false;
        }
        
        if(countDown)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 5;
        }
        
        if (timer <= 0)
        {
            GameScript.instance.OnPlayerLeft(this);
            End();
        }

        if (dying)
        {
            death -= Time.deltaTime;
        }
        if (death <= 0)
        {
            Destroy(gameObject);
            dying = false;
            death = 3;
        }
        
    }
    
    private void OnEnable()
    {
        guess.action.started += Guess;
    }
    private void OnDisable()
    {
        guess.action.started -= Guess;
    }

    private void Guess(InputAction.CallbackContext obj)
    {
        rigidbody.linearVelocity = Vector2.zero;
        dying = true;
    }

    private void End()
    {
        //rigidbody.linearVelocity = Vector2.zero;
        //dying = true;
        transform.position = Vector2.zero;
    }
}
