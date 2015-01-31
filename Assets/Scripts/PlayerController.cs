using UnityEngine;
using System.Collections;
[@RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 3f;
	RaycastHit2D hit;
	BoxCollider2D boxCollider;
	private Vector3 moveDirection;
	
	void Start()
	{
		boxCollider = GetComponent<BoxCollider2D>();
	}
	
	void Update()
	{
		var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * walkSpeed * Time.deltaTime;

		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(direction.x, 0), Mathf.Abs(direction.x));
		if (hit.collider == null)
		{
			transform.position += new Vector3(direction.x, 0, 0);
		}
		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, direction.y), Mathf.Abs(direction.y));

		if (hit.collider == null)
		{
			transform.position += new Vector3(0, direction.y, 0);
		}

		//Get the mouse input direction and look at it
		Vector3 mouseDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDirection.y - transform.position.y, mouseDirection.x - transform.position.x) * Mathf.Rad2Deg - 180);
	}
}