using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("Player Input, configuracion de inputs del Player")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("RectTransform")]
    [Tooltip("Referencia al RectTransform de lo que será el cursor")]
    [SerializeField] private RectTransform _cursorTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _canvasTransform;

    [Header("Speed")]
    [Tooltip("Velocidad de desplazamiento del cursor")]
    [SerializeField] private float _cursorSpeed = 1000f;

    [Header("Virtual Mouse")]
    [Tooltip("Referencia virtual al Mouse")]
    [SerializeField] private Mouse _virtualMouse;
    [SerializeField] private Camera _mainCamera;

    [SerializeField]private bool _previousMouseState;

    private void OnEnable()
    {
        _mainCamera = Camera.main;

        if (_virtualMouse == null)
        {
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!_virtualMouse.added)
        {
            InputSystem.AddDevice(_virtualMouse);
        }

        //
        InputUser.PerformPairingWithDevice(_virtualMouse, _playerInput.user);

        if (_cursorTransform != null)
        {
            Vector2 position = _cursorTransform.anchoredPosition;

            InputState.Change(_virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if (_virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        Vector2 _deltaValue = Gamepad.current.leftStick.ReadValue();
        _deltaValue *= _cursorSpeed * Time.deltaTime;

        Vector2 _curPosition = _virtualMouse.position.ReadValue();
        Vector2 _newPosition = _curPosition + _deltaValue;

        _newPosition.x = Mathf.Clamp(_newPosition.x, 0, Screen.width);
        _newPosition.y = Mathf.Clamp(_newPosition.y, 0, Screen.height);

        InputState.Change(_virtualMouse.position, _newPosition);
        InputState.Change(_virtualMouse.delta, _curPosition);


        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (_previousMouseState != Gamepad.current.aButton.isPressed)
        {
            _virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, Gamepad.current.aButton.IsPressed());
            InputState.Change(_virtualMouse, mouseState);
            _previousMouseState = aButtonIsPressed;

        }

        AnchorCursor(_newPosition);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 _anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasTransform, position, _canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : _mainCamera, out _anchoredPosition);
        _cursorTransform.anchoredPosition = _anchoredPosition;
    }
}
