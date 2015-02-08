//using UnityEngine;
//using System.Collections;
////[@RequireComponent(typeof(BoxCollider2D))]
//
//public class BackupPlayerController : MonoBehaviour
//{
//	public float walkSpeed;
//	RaycastHit2D hit;
//	BoxCollider2D boxCollider;
//	//	public float turnSpeed;
//	private Vector3 moveDirection;
//	
//	void Start()
//	{
//		//To do keyvohn, collider
//		//		boxCollider = GetComponent<BoxCollider2D>();
//	}
//	
//	void Update()
//	{
//		//To do keyvohn, collider
//		var direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * walkSpeed * Time.deltaTime;
//		
//		
//		//		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, direction.y), Mathf.Abs(direction.y));
//		//		if (hit.collider == null)
//		//		{
//		transform.position += new Vector3(direction.x, direction.y, 0);
//		//		}
//		//		hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(direction.x,0), Mathf.Abs(direction.x));
//		//		if (hit.collider == null)
//		//		{
//		//		transform.Translate(direction.x, 0, 0);
//		//		}
//		
//		//Get the mouse input direction;
//		Vector3 currentPosition = transform.position;
//		Vector3 mouseDirection = Camera.main.ScreenToWorldPoint( Input.mousePosition );
//		
//		transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouseDirection.y - transform.position.y, mouseDirection.x - transform.position.x) * Mathf.Rad2Deg - 180);
//	}
//}