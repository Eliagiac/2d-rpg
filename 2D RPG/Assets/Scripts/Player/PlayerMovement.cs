using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Character Variables
    public GameObject downCharacter;
    public GameObject upCharacter;
    public GameObject leftCharacter;
    public GameObject rightCharacter;

    string directionX = "";
    string directionY = "";

    float diagonalSpeedReduction = Mathf.Sqrt(2);
    #endregion

    #region Generic Variables
    public Rigidbody2D rb;

    public float movementSpeed = 5f;

    Vector2 movement;

    public Animator animator;
    #endregion

    void Update()
    {
        #region Get Movement Input
        movement.x = Input.GetAxisRaw("Horizontal");

        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 && movement.y != 0)
        {
            movement.x = movement.x / diagonalSpeedReduction;
            movement.y = movement.y / diagonalSpeedReduction;
        }
        #endregion

        #region Animate
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.y < 0 && movement.x == 0)
        {
            directionY = "down";

            directionX = "";
        }

        else if (movement.y > 0 && movement.x == 0)
        {
            directionY = "up";

            directionX = "";
        }

        if (movement.x < 0)
        {
            directionX = "left";

            directionY = "";
        }

        else if (movement.x > 0)
        {
            directionX = "right";

            directionY = "";
        }

        if (directionX != "")
        {
            SetCharacter(directionX);
        }

        else if (directionY != "")
        {
            SetCharacter(directionY);
        }
        #endregion
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHandler>();
    } // Get reference to the player

    private void FixedUpdate()
    {
        if (MenuHandler.isMenuOpen == false)
        {
            Move();
        }
    } // Move

    void Move()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    void SetCharacter(string dir)
    {
        switch (dir)
        {
            case "down":

                downCharacter.SetActive(true);
                upCharacter.SetActive(false);
                leftCharacter.SetActive(false);
                rightCharacter.SetActive(false);

                animator.SetFloat("Idle Vertical", -1f);

                break;

            case "up":

                downCharacter.SetActive(false);
                upCharacter.SetActive(true);
                leftCharacter.SetActive(false);
                rightCharacter.SetActive(false);

                animator.SetFloat("Idle Vertical", 1f);

                break;

            case "left":

                downCharacter.SetActive(false);
                upCharacter.SetActive(false);
                leftCharacter.SetActive(true);
                rightCharacter.SetActive(false);

                animator.SetFloat("Idle Horizontal", -1f);

                break;

            case "right":

                downCharacter.SetActive(false);
                upCharacter.SetActive(false);
                leftCharacter.SetActive(false);
                rightCharacter.SetActive(true);

                animator.SetFloat("Idle Horizontal", 1f);

                break;
        }
    }
}