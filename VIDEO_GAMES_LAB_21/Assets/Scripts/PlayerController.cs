// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]         // be sure we have CharacterController
public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void onMove(InputAction.CallbackContext context){
        movementInput = context.ReadValue<Vector2>();
    }

    public void onJump(InputAction.CallbackContext context){
        jumped = context.action.triggered;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            //gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}