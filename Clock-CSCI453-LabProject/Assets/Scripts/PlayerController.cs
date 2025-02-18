using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float sensitivity;
    [SerializeField] float sprintSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] int spareRounds;
    [SerializeField] Weapon equippedWeapon;

    private float moveFB;
    private float moveLR;
    private float rotX;
    private float rotY;

    private CharacterController cc;
    private Camera playerCam;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        playerCam = transform.GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetButtonDown("Reload"))
        {
            equippedWeapon = getCurrentWeapon();
            spareRounds = equippedWeapon.Reload(spareRounds);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            equippedWeapon = getCurrentWeapon();
            if (equippedWeapon.GetRemainingBullets() > 0)
                equippedWeapon.Shoot();
            else
                spareRounds = equippedWeapon.Reload(spareRounds);
        }
        
    }

    private void Move()
    {
        float movementSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
        }
        else
            movementSpeed = speed;


        moveFB = Input.GetAxis("Vertical") * movementSpeed;
        moveLR = Input.GetAxis("Horizontal") * movementSpeed;

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;

        rotY = Mathf.Clamp(rotY, -60f, 60f);

        Vector3 movement = new Vector3(moveLR, 0, moveFB).normalized * movementSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            cc.Move(Vector3.up * jumpForce);
        }

        if (cc.isGrounded)
        {
            movement.y = -2f;
        }
        else
        {
            movement.y -= gravity;
        }


        transform.Rotate(0, rotX, 0);

        playerCam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        movement = transform.rotation * movement;
        cc.Move(movement * Time.deltaTime);

    }

    private Weapon getCurrentWeapon()
    {
        return equippedWeapon;
    }

}
