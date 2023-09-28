using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float lookSensitivity = 3.0f;

    private float minAngle = -90f;
    private float maxAngle = 90f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        //Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * xMov;
        Vector3 _movVertical = transform.forward * zMov;

        //Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3D vector (turning around)
        float _yRot = Mathf.Clamp(Input.GetAxisRaw("Mouse X"), minAngle, maxAngle);

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);
        
        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Mathf.Clamp(Input.GetAxisRaw("Mouse Y"), minAngle, maxAngle);

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotation);
    }
}
