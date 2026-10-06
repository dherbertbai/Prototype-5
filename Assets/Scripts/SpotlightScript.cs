using UnityEngine;
using UnityEngine.InputSystem;

public class SpotlightScript : MonoBehaviour
{
    public InputActionReference action;
    public bool correct;
    //public GameScript game;
    public SpriteRenderer sprite;
    

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
        if (correct)
        {
            Debug.Log("Win!");
            sprite.color = Color.green;
        }
        else
        {
            Debug.Log("Lose!");
            sprite.color = Color.red;
        }
        GameScript.instance.OnPlayerGuessed(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            correct = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            correct = false;
        }
    }
}
