using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    #region
    [Header("References")]
    public InputManager InputManager;
    public CharacterController Controller;
    public Transform CameraPose;

    [Header("Variables")]
    public float PlayerSpeed;
    public float VelocityOfPlayer;
    public Vector3 MoveDirection;

    #endregion

    private void Update()
    {
        PlayerWASD();
        PlayerRotate();
    }

    private void PlayerWASD()
    {
        MoveDirection = transform.right * InputManager.OnMoveInput.x + transform.forward * InputManager.OnMoveInput.y;
        Controller.Move(MoveDirection * Time.deltaTime * PlayerSpeed);
    }

    private void Gravity()
    {

    }
    
    private void PlayerRotate()
    {
        transform.eulerAngles = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z).eulerAngles;
    }
}
