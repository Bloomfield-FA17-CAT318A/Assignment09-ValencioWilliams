using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

	public float speed = 5f;
	public float rotationSpeed = 5f;

	public Text healthText;

	public int health = 3;

	public Transform startpos;

	// Use this for initialization
	void Start ()
	{
		health = 3;
	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerInput ();
		Death ();
	}

	void FixedUpdate()
	{
		UpdateText ();
	}

	void PlayerInput ()
	{
		float move = Input.GetAxis ("Horizontal");

		if (Input.GetKey (KeyCode.W))
		{
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}

		transform.Rotate (new Vector3 (0, 1, 0) * rotationSpeed * move * Time.deltaTime);
	}

	void UpdateText()
	{
		healthText.text = "Health: " + health;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Endpoint")
		{
			Debug.Log ("YOU WIN YAY");
			GameController.instance.Win ();
		}
	}

	public void TakeDamage(int dmg)
	{
		health -= dmg;
	}

	void Death()
	{
		if (health <= 0)
		{
			health = 3;
			transform.position = startpos.position;
			transform.rotation = startpos.rotation;
		}
	}
}