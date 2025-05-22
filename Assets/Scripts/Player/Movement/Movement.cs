using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private Rigidbody2D rb;
    [SerializeField] private int speed = 5;
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private Animator animator;

    
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
                animator.SetFloat("SavedX", lastMoveDirection.x);
                animator.SetFloat("SavedY", lastMoveDirection.y);
            }           
            animator.SetFloat("XInput", moveDirection.x);
            animator.SetFloat("YInput", moveDirection.y);
        }        
    }
}
