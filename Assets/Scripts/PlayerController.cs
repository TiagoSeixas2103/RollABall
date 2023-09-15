using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI hpText;
	public TextMeshProUGUI timeText;
	public GameObject winTextObject;
	public GameObject loseTextObject;
	public GameObject playAgainButton;

    private float movementX;
    private float movementY;

	private Rigidbody rb;
	private int count;
	private int hp;
	private int currentTime;
	private bool won;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;
		currentTime = 0;
		hp = 3;
		won = false;

		SetCountText();
		SetHPText();

        winTextObject.SetActive(false);
		loseTextObject.SetActive(false);
		playAgainButton.SetActive(false);
	}

    void OnMove(InputValue moveValue)
    {
        Vector2 moveVector = moveValue.Get<Vector2>();

        movementX = moveVector.x;
        movementY = moveVector.y;
    }

    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12) 
		{
            winTextObject.SetActive(true);
			playAgainButton.SetActive(true);
			won = true;
		}
	}

	void SetHPText()
	{
		hpText.text = "HP: " + hp.ToString();

		if (hp <= 0 && won == false) 
		{
            loseTextObject.SetActive(true);
			playAgainButton.SetActive(true);
		}
	}

	void SetTimeText()
	{
		if (Time.timeSinceLevelLoad <= 120 && won == false && hp > 0)
		{
			currentTime = (int)Time.timeSinceLevelLoad;
			timeText.text = currentTime.ToString() + "s";
		}
		else if (Time.timeSinceLevelLoad > 120 && won == false && hp > 0)
		{
			timeText.text = "120s";
            loseTextObject.SetActive(true);
			playAgainButton.SetActive(true);
		}
	}

	void FixedUpdate()
	{
		if (Time.timeSinceLevelLoad <= 120 && hp > 0 && won == false) {
			Vector3 movement = new Vector3(movementX, 0.0f, movementY);

			rb.AddForce(movement * speed);
		}

		SetTimeText();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			if (hp > 0 && won == false) {
				count = count + 1;
			}

			SetCountText();
		}

		if (other.gameObject.CompareTag("Enemy"))
		{
			other.gameObject.SetActive(false);

			if (hp > 0  && won == false) {
				hp = hp - 1;
			}

			SetHPText();
		}
	}

}
