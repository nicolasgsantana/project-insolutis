using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;

    public static Vector2 mousePos;


    [SerializeField] private Camera cam;

    [SerializeField] private Image redCardImage;
    [SerializeField] private Image greenCardImage;
    [SerializeField] private Image blueCardImage;

    private Image redCardCheckIcon;
    private Image greenCardCheckIcon;
    private Image blueCardCheckIcon;

    [SerializeField] private bool hasRedCard;
    [SerializeField] private bool hasGreenCard;
    [SerializeField] private bool hasBlueCard;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        redCardCheckIcon = redCardImage.gameObject.GetComponentInChildren<Image>();
        greenCardCheckIcon = greenCardImage.gameObject.GetComponentInChildren<Image>();
        blueCardCheckIcon = blueCardImage.gameObject.GetComponentInChildren<Image>();

    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Red Card"))
        {
            hasRedCard = true;

            Color newCardColor = new Color(redCardImage.color.r, redCardImage.color.g, redCardImage.color.b, 1f);
            redCardImage.color = newCardColor;

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Blue Card"))
        {
            hasBlueCard = true;

            Color newCardColor = new Color(blueCardImage.color.r, blueCardImage.color.g, blueCardImage.color.b, 1f);
            blueCardImage.color = newCardColor;

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Green Card"))
        {
            hasGreenCard = true;

            Color newCardColor = new Color(greenCardImage.color.r, greenCardImage.color.g, greenCardImage.color.b, 1f);
            greenCardImage.color = newCardColor;

            Destroy(collision.gameObject);
        }
    }
}
