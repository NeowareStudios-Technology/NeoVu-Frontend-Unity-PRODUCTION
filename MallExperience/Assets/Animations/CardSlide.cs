using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlide : MonoBehaviour
{
    public GameObject card;
    public Animation[] anims;
    public GameObject bigCard;
    // Start is called before the first frame update
    void Start()
    {
        anims = card.GetComponents<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideUp()
    {
        card.SetActive(true);
        card.GetComponent<Animation>().Play("SlideUp");
    }
    public void SlideDown()
    {
        card.GetComponent<Animation>().Play("SlideDown");
    }
    public void BigSlideUp()
    {
        bigCard.GetComponent<Animation>().Play("BigCardSlideUp");
    }
    public void BigSlideDown()
    {
        bigCard.GetComponent<Animation>().Play("BigCardSlideDown");
    }
}
