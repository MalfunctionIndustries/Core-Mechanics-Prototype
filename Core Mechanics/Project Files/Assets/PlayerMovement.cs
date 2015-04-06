using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public KeyCode[] left;
    public KeyCode[] right;
    public KeyCode[] jump;

    public float speed;
    public float jumpHeight;

    private Rigidbody rigidbody;

    enum Direction
    {
        left,
        right
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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

	}

    void MoveChar (Direction direction) {
        if (direction == Direction.left)
            rigidbody.AddForce(Vector3.left * speed);
        else
            rigidbody.AddForce(Vector3.right * speed);
    }

    void Jump () {
        rigidbody.AddForce(Vector3.up * jumpHeight);
    }

}
