using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPhysicsHandler : MonoBehaviour
{
    Rigidbody2D rb;

    bool isMoving;

    Vector2 direction;

    Sprite[] belts;

    SpriteRenderer beltSpriteRenderer;

    public float speed = 0.01f;

    float diagonalOffset = Mathf.Sqrt(2);

    Vector2 startingPosition;
    Vector2 currentPosition;
    Vector2 pivotPosition;

    private void Start()
    {
        rb = transform.GetComponentInParent<Rigidbody2D>();

        belts = BuildingSystem._belts;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Phisics" && collision.transform.parent.CompareTag("Belt"))
        {
            isMoving = false;

            beltSpriteRenderer = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Phisics" && collision.transform.parent.CompareTag("Belt"))
        {
            isMoving = true;

            beltSpriteRenderer = collision.transform.GetComponentInParent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Phisics" && collision.transform.parent.CompareTag("Belt"))
        {
            startingPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            currentPosition = transform.position;

            #region Set Direction
            if (beltSpriteRenderer.sprite == belts[0] || beltSpriteRenderer.sprite == belts[10] || beltSpriteRenderer.sprite == belts[11] || beltSpriteRenderer.sprite == belts[14])
            {
                direction = new Vector2(0f, 90f);
            }

            else if (beltSpriteRenderer.sprite == belts[2] || beltSpriteRenderer.sprite == belts[18] || beltSpriteRenderer.sprite == belts[19] || beltSpriteRenderer.sprite == belts[20])
            {
                direction = new Vector2(90f, 0f);
            }

            else if (beltSpriteRenderer.sprite == belts[1] || beltSpriteRenderer.sprite == belts[15] || beltSpriteRenderer.sprite == belts[16] || beltSpriteRenderer.sprite == belts[17])
            {
                direction = new Vector2(0f, -90f);
            }

            else if (beltSpriteRenderer.sprite == belts[3] || beltSpriteRenderer.sprite == belts[21] || beltSpriteRenderer.sprite == belts[22] || beltSpriteRenderer.sprite == belts[23])
            {
                direction = new Vector2(-90f, 0f);
            }

            else if (beltSpriteRenderer.sprite == belts[4] || beltSpriteRenderer.sprite == belts[24])
            {
                pivotPosition = new Vector2(currentPosition.x % 1 < 0.5f ? Mathf.Ceil(currentPosition.x) + 0.5f : Mathf.Floor(currentPosition.x) + 0.5f, Mathf.Ceil(currentPosition.y) - 0.5f);

                if (currentPosition.y - pivotPosition.y < pivotPosition.x - startingPosition.x)
                {
                    direction = new Vector2(90f / diagonalOffset, 90f / diagonalOffset);
                }

                else direction = new Vector2(90f, 0f);
            }

            else if(beltSpriteRenderer.sprite == belts[13] || beltSpriteRenderer.sprite == belts[29])
            {
                pivotPosition = new Vector2(Mathf.Ceil(currentPosition.x) - 0.5f, currentPosition.y % 1 < 0.5f ? Mathf.Ceil(currentPosition.y) + 0.5f : Mathf.Floor(currentPosition.y) + 0.5f);

                if (pivotPosition.x - currentPosition.x < pivotPosition.y - startingPosition.y)
                {
                    direction = new Vector2(90f / diagonalOffset, 90f / diagonalOffset);
                }

                else direction = new Vector2(0f, 90f);
            }

            //ho lasciato qui mamma

            else if (beltSpriteRenderer.sprite == belts[5] || beltSpriteRenderer.sprite == belts[25])
            {
                pivotPosition = new Vector2(currentPosition.x % 1 < 0.5f ? Mathf.Ceil(currentPosition.x) - 0.5f : Mathf.Floor(currentPosition.x) - 0.5f, Mathf.Ceil(currentPosition.y) - 0.5f);

                if (currentPosition.y - pivotPosition.y < startingPosition.x - pivotPosition.x)
                {
                    direction = new Vector2(-90f / diagonalOffset, 90f / diagonalOffset);
                }

                else direction = new Vector2(-90f, 0f);
            }

            else if (beltSpriteRenderer.sprite == belts[12] || beltSpriteRenderer.sprite == belts[28])
            {
                pivotPosition = new Vector2(Mathf.Ceil(currentPosition.x) - 0.5f, currentPosition.y % 1 < 0.5f ? Mathf.Ceil(currentPosition.y) + 0.5f : Mathf.Floor(currentPosition.y) + 0.5f);

                if (pivotPosition.x - currentPosition.x < pivotPosition.y - startingPosition.y)
                {
                    direction = new Vector2(-90f / diagonalOffset, 90f / diagonalOffset);
                }

                else direction = new Vector2(0f, 90f);
            }

            else if (beltSpriteRenderer.sprite == belts[6] || beltSpriteRenderer.sprite == belts[26])
            {
                pivotPosition = new Vector2(currentPosition.x % 1 < 0.5f ? Mathf.Ceil(currentPosition.x) + 0.5f : Mathf.Floor(currentPosition.x) + 0.5f, Mathf.Ceil(currentPosition.y) - 0.5f);

                if (pivotPosition.x - currentPosition.x < startingPosition.y - pivotPosition.y)
                {
                    direction = new Vector2(-90f / diagonalOffset, -90f / diagonalOffset);
                }

                else direction = new Vector2(0f, -90f);
            }

            else if (beltSpriteRenderer.sprite == belts[9] || beltSpriteRenderer.sprite == belts[31])
            {
                pivotPosition = new Vector2(currentPosition.x % 1 < 0.5f ? Mathf.Ceil(currentPosition.x) - 0.5f : Mathf.Floor(currentPosition.y) - 0.5f, Mathf.Ceil(currentPosition.y) - 0.5f);

                if (pivotPosition.y - currentPosition.y < startingPosition.x - pivotPosition.x)
                {
                    direction = new Vector2(-90f / diagonalOffset, -90f / diagonalOffset);
                }

                else direction = new Vector2(-90f, 0f);
            }

            else if (beltSpriteRenderer.sprite == belts[7] || beltSpriteRenderer.sprite == belts[27])
            {
                pivotPosition = new Vector2(Mathf.Ceil(currentPosition.x) - 0.5f, currentPosition.y % 1 < 0.5f ? Mathf.Ceil(currentPosition.y) - 0.5f : Mathf.Floor(currentPosition.y) - 0.5f);

                if (currentPosition.x - pivotPosition.x < startingPosition.y - pivotPosition.y)
                {
                    direction = new Vector2(90f / diagonalOffset, -90f / diagonalOffset);
                }

                else direction = new Vector2(0f, -90f);
            }

            else if (beltSpriteRenderer.sprite == belts[8] || beltSpriteRenderer.sprite == belts[30])
            {
                pivotPosition = new Vector2(Mathf.Ceil(currentPosition.x) - 0.5f, currentPosition.y % 1 < 0.5f ? Mathf.Ceil(currentPosition.y) + 0.5f : Mathf.Floor(currentPosition.y) + 0.5f);

                if (pivotPosition.y - currentPosition.y < pivotPosition.x - currentPosition.x)
                {
                    direction = new Vector2(90f / diagonalOffset, -90f / diagonalOffset);
                }

                else direction = new Vector2(90f, 0f);
            }
            #endregion

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}
