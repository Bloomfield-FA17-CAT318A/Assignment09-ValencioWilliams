using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

	public NavMeshAgent navMesh;

	public Transform[] listOfPatrolPoints;

	public Transform currentPatrolPoint;

	public GameObject player;

	public Color baseColor;

	[SerializeField]
	private Renderer enemyRender;

	public enum EnemyStateMachine
	{
		Patrolling,
		Chasing,
		Attacking
	};

	[SerializeField]
	private EnemyStateMachine newState;

	public int dmg = 1;

	void Start ()
	{
		if (currentPatrolPoint == null)
		{
			currentPatrolPoint = listOfPatrolPoints [Random.Range (0, listOfPatrolPoints.Length)];
		}
			
		baseColor = enemyRender.material.color;

	}
	
	void Update ()
	{
		StateMachineController ();

	}

	void StateMachineController ()
	{
		switch (newState)
		{
			case EnemyStateMachine.Patrolling:
				{
					Patrolling ();
					enemyRender.material.color = baseColor;
					break;
				}
			case EnemyStateMachine.Chasing:
				{
					Chasing ();
					enemyRender.material.color = Color.yellow;

					break;
				}
			case EnemyStateMachine.Attacking:
				{
					Attacking ();
					enemyRender.material.color = Color.green;
					break;
				}
		}
	}

	#region Triggers/Collisions
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			newState = EnemyStateMachine.Chasing;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			newState = EnemyStateMachine.Patrolling;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			newState = EnemyStateMachine.Attacking;
			other.gameObject.GetComponent<Player> ().TakeDamage (dmg);
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			newState = EnemyStateMachine.Chasing;
		}
	}
	#endregion

	#region State Machine Methods
	void Patrolling ()
	{
		Debug.Log ("PATROLLING");

		navMesh.SetDestination (currentPatrolPoint.transform.position);
		float dist = Vector3.Distance (transform.position, currentPatrolPoint.transform.position);

		if (dist <= navMesh.stoppingDistance)
		{
			currentPatrolPoint = listOfPatrolPoints [Random.Range (0, listOfPatrolPoints.Length)];
		}
	}
		  
	void Chasing()
	{
		Debug.Log ("CHASE");

		navMesh.SetDestination (player.transform.position);	

	}

	void Attacking()
	{
		Debug.Log ("ATTACK");

		navMesh.speed = 40f;

	}

	#endregion
		
}