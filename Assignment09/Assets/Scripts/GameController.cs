using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Player player;

	public Image winImage;

	void Start()
	{
		instance = this;
		winImage.gameObject.SetActive (false);
	}

	public void Win()
	{
		winImage.gameObject.SetActive (true);
	}

	public void RestartGame()
	{
		SceneManager.LoadScene (0);
	}
}
