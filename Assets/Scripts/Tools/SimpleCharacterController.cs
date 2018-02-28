using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCharacterController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float walkSpeed = 8f;
    [SerializeField]
    private float jumpSpeed = 8f;
    [SerializeField]
    private float jumpTime = 1f;
    [SerializeField]
    private float gravity = -2f;
    [SerializeField]
    private float groundRaycastOffset = -0.5f;
    [SerializeField]
    private float groundRaycastLength = 0.5f;
    [SerializeField]
    private LayerMask groundLayers;

    private Vector2 inputAxis;
    private bool inputJump;
    private bool inputJumpReset = false;
    private float lastJump;
    private Vector3 moveDir;
    new private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void Update()
    {

        // cache all input data in update loop
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        // cache jump combined with a reset bool, to not miss any jump input
        if(inputJumpReset == false){
            inputJump = Input.GetKeyDown(KeyCode.Space);
            if(inputJump) inputJumpReset = true;
        }

    }

    private void FixedUpdate()
    {

        // check if character is grounded
        bool isGrounded = IsGrounded();

        // apply input data to moveDir
        moveDir.x = inputAxis.x * walkSpeed;
        moveDir.z = inputAxis.y * walkSpeed;

        // apply y gravity
        if(isGrounded) {
            moveDir.y = 0f;
        } else {
            moveDir.y = gravity;
        }

        // save timestamp of last jump input
        if(IsJumping()) {
            lastJump = Time.time + jumpTime;
        }

        // override movedir.y if character is still within jumpTime
        if(lastJump > Time.time) {
            moveDir.y = jumpSpeed;
        }

        rigidbody.velocity = moveDir;
    }

    private bool IsJumping(){
        
        if(inputJump){
            inputJumpReset = false;
            return true;
        }

        return false;
    }

    private bool IsGrounded(){

        Vector3 origin = rigidbody.position + (Vector3.up * groundRaycastOffset);
        Vector3 dir = Vector3.down * groundRaycastLength;

        Debug.DrawRay(origin, dir, Color.red, Time.deltaTime);

        if(Physics.Raycast(origin, dir, groundRaycastLength, groundLayers))
        {
            return true;
        }

        return false;
    }

}



