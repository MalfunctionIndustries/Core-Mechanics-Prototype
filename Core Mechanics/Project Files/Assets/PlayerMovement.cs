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

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 20f);

	}

    void MoveChar (Direction direction) {
        if (direction == Direction.left)
            rigidbody.AddForce(Vector3.left * speed);
        else
            rigidbody.AddForce(Vector3.right * speed);
    }

    void Jump () {
        if(onGround)
            rigidbody.AddForce(Vector3.up * jumpHeight);
        onGround = false;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        desiredRotation = Quaternion.FromToRotation(Vector3.up, collisionInfo.contacts[0].normal);
        onGround = true;
    }

}
