using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D RB;
    Vector2 input;
    [SerializeField] float movementSpeed = 15;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        RB.AddForce(input.normalized * movementSpeed);
    }
}
