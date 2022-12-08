using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Image redCardImage;
    [SerializeField] private Image greenCardImage;
    [SerializeField] private Image blueCardImage;

    [SerializeField] private GameObject redPortalOne;
    [SerializeField] private GameObject redPortalTwo;

    [SerializeField] private GameObject greenPortalOne;
    [SerializeField] private GameObject greenPortalTwo;

    [SerializeField] private GameObject bluePortalOne;
    [SerializeField] private GameObject bluePortalTwo;

    [SerializeField] GameObject victoryScreen;

    private bool hasRedCard;
    private bool hasGreenCard;
    private bool hasBlueCard;

    private bool redCardComplete;
    private bool greenCardComplete;
    private bool blueCardComplete;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Card pickup
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

        //Portal logic
        if(redCardComplete)
        {
            if (collision.gameObject.CompareTag("Red Portal 1"))
            {
                transform.position = redPortalTwo.transform.position + Vector3.one;
            }
            else if (collision.gameObject.CompareTag("Red Portal 2"))
            {
                transform.position = redPortalOne.transform.position + Vector3.one;
            }
        }

        if (greenCardComplete)
        {
            if (collision.gameObject.CompareTag("Green Portal 1"))
            {
                transform.position = greenPortalTwo.transform.position + Vector3.one;
            }
            else if (collision.gameObject.CompareTag("Green Portal 2"))
            {
                transform.position = greenPortalOne.transform.position + Vector3.one;
            }
        }

        if (blueCardComplete)
        {
            if (collision.gameObject.CompareTag("Blue Portal 1"))
            {
                transform.position = bluePortalTwo.transform.position + Vector3.one;
            }
            else if (collision.gameObject.CompareTag("Blue Portal 2"))
            {
                transform.position = bluePortalOne.transform.position + Vector3.one;
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Generator activation
        if (collision.gameObject.CompareTag("Red Generator") && hasRedCard)
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            
            redCardComplete = true;
            redCardImage.transform.GetChild(0).gameObject.SetActive(true);

            redPortalOne.transform.GetChild(0).gameObject.SetActive(true);
            redPortalTwo.transform.GetChild(0).gameObject.SetActive(true);


        }
        else if (collision.gameObject.CompareTag("Green Generator") && hasGreenCard)
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);

            greenCardComplete = true;
            greenCardImage.transform.GetChild(0).gameObject.SetActive(true);

            greenPortalOne.transform.GetChild(0).gameObject.SetActive(true);
            greenPortalTwo.transform.GetChild(0).gameObject.SetActive(true);


        }
        else if (collision.gameObject.CompareTag("Blue Generator") && hasBlueCard)
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);

            blueCardComplete = true;
            blueCardImage.transform.GetChild(0).gameObject.SetActive(true);

            bluePortalOne.transform.GetChild(0).gameObject.SetActive(true);
            bluePortalTwo.transform.GetChild(0).gameObject.SetActive(true);

        }
    }

    private void Update()
    {
        if(redCardComplete && greenCardComplete && blueCardComplete)
            victoryScreen.SetActive(true);

    }

}
