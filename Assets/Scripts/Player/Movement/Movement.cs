using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private int speed = 5;
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private Animator animator;
    //Used in other classes as 1d interpration of a 2d direction 1 = right, 2 = left, 3 = up etc
    private int moveDirection1DInterpreter = 1;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private Rigidbody2D rb;
    


    
    // Start is called before the first frame update
    void Start()
    {
        
        moveDirection = movementAction.action.ReadValue<Vector2>();
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (UIControl.Singleton.GetGameState() == false)
        {

            moveDirection = movementAction.action.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

            //This sets the idle blend tree values as the player's last inputed direction
            if (moveDirection != Vector2.zero)
            {
                lastMoveDirection = moveDirection;
                if (moveDirection.x == -1)
                {
                    moveDirection1DInterpreter = 1;
                }
                if (moveDirection.x == 1)
                {
                    moveDirection1DInterpreter = 2;
                }
                if (moveDirection.y == -1)
                {
                    moveDirection1DInterpreter = 3;
                }
                if (moveDirection.y == 1)
                {
                    moveDirection1DInterpreter = 4;
                }

                //Debug.Log(moveDirection1DInterpreter);
                animator.SetFloat("SavedX", lastMoveDirection.x);
                animator.SetFloat("SavedY", lastMoveDirection.y);
            }           
            animator.SetFloat("XInput", moveDirection.x);
            animator.SetFloat("YInput", moveDirection.y);
        }        
    }

    //Returns an 1D version of the 2D movement i.e: 1 = left, 2 = right, 3 = down, 4 = up
    public int GetMovementDirection()
    {
        return moveDirection1DInterpreter;
    }
}
