using UnityEngine;
using System.Collections;
//[@RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 3f;
	RaycastHit2D hit;
	BoxCollider2D boxCollider;
	
	void Start()
	{
		//		boxCollider = GetComponent<BoxCollider2D>();
	}
	
	void Update()
	{
		
		var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * walkSpeed * Time.deltaTime;
		//		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, direction.y), Mathf.Abs(direction.y));
		//		if (hit.collider == null)
		//		{
		transform.Translate(0, direction.y, 0);
		//		}
		//		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(direction.x,0), Mathf.Abs(direction.x));
		//		if (hit.collider == null)
		//		{
		transform.Translate(direction.x, 0, 0);
		//		}
		
		//Get the mouse input direction
//		transform.Rotate ();
	}
}