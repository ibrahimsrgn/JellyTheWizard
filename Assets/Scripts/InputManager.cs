using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("PlayerMovements")]
    public Vector2 OnMoveInput;
    public Vector2 OnLookInput;

    void OnMove(InputValue Value)
    {
        OnMoveInput = Value.Get<Vector2>();
    }

    void OnLook(InputValue Value)
    {
        OnLookInput = Value.Get<Vector2>();
    }

    void OnDeadLockToEnemy(InputValue Value)
    {
        DeadLockEnemy.Instance.DeadLocker();
    }
    void OnDeadLockOff(InputValue Value)
    {
        DeadLockEnemy.Instance.DeadUnlocker();
    }
    void OnSkillHotBar(InputValue inputValue)
    {
        int.TryParse(GetComponent<PlayerInput>().actions["SkillHotBar"].activeControl.name, out int skill);
        SkillManager.Instance.ChangeSkillTree(skill);
    }
    void OnAttack() { }

    void OnInteract() { }

    void OnCrouch() { }

    void OnJump() { }

    void OnPrevious() { }

    void OnNext() { }

    void OnSprint() { }

    void OnNavigate() { }

    void OnSubmit() { }

    void OnCancel() { }

    void OnPoint() { }

    void OnClick() { }

    void OnRightClick() { }

    void OnMiddleClick() { }

    void OnScrollWheel() { }

    void OnTrackedDevicePosition() { }

    void OnTrackedDeviceOrientation() { }
}
