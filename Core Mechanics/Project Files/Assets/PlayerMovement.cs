using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public KeyCode[] left;
    public KeyCode[] right;
    public KeyCode[] jump;

    public float speed;
    public float jumpHeight;

    private Rigidbody rigidbody;
    private Quaternion desiredRotation;

    private bool onGround;

    enum Direction
    {
        left,
        right
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        onGround = false;
    }

	void Update () {

        for (int i = 0; i < left.Length; i++ ) {
            if (Input.GetKey(left[i])){
                // Move left
                MoveChar(Direction.left);
            }
        }

        for (int i = 0; i < right.Length; i++) {
            if (Input.GetKey(right[i])) {
                // Move right
                MoveChar(Direction.right);
            }
        }

        for (int i = 0; i < jump.Length; i++) {
            if (Input.GetKeyDown(jump[i])) {
                // Jump
                Jump();
            }
        }

        if (!onGround)
        {
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                Debug.DrawLine(transform.position, hit.point);
                transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * 5f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 20f);
        }

	}

    void MoveChar (Direction direction) {
        Vector3 left = transform.TransformDirection(Vector3.left);
        if (direction == Direction.left)
            rigidbody.AddForce(left * speed);
        else
            rigidbody.AddForce(left * -speed);
    }

    void Jump () {
        if(onGround)
            rigidbody.AddForce(transform.TransformDirection(Vector3.up) * jumpHeight);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        desiredRotation = Quaternion.FromToRotation(Vector3.up, collisionInfo.contacts[0].normal);
        onGround = true;
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        onGround = false;
    }

}
