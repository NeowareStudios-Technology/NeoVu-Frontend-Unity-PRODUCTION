using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MatchingGame : MonoBehaviour
{
    public GameObject[] Cards;
    public List<int> cardNumbers;
    public Material[] mats;
    public MatchCard card1;
    public MatchCard card2;
    public int cardCount = 0;
    public int matched;
    public int targetCount;
    private bool win;
    public GameObject resetButton;
    public VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(cardCount >= 2)
        {
            Compare();
        }
        if(matched == targetCount && win == false)
        {
            Debug.LogWarning("You win");
            win = true;
            resetButton.SetActive(true);
            video.gameObject.SetActive(true);
            video.Play();
        }
        
    }

     public void Compare()
    {
        if(card1.Value == card2.Value)
        {
            card1.gameObject.SetActive(false);
            card2.gameObject.SetActive(false);
            matched = matched + 2;
            card1 = null;
            card2 = null;
            cardCount = 0;
            Debug.LogWarning("Match");
        }
        else
        {
        }
    }
    public void revert()
    {
        Debug.LogWarning("Revert Called");
        card1.flip();
        card2.flip();
        card1.GetComponent<BoxCollider>().enabled = true;
        card2.GetComponent<BoxCollider>().enabled = true;
        card1 = null;
        card2 = null;
        cardCount = 0;
    }
    public void reset()
    {
        List<int> cardClone = new List<int>();
        cardClone = cardNumbers;

        for (int i = 0; i < Cards.Length; i++)
        {
            int num = Random.Range(0, cardClone.Count);
            Cards[i].GetComponent<MatchCard>().Value = cardClone[num];
            cardClone.RemoveAt(num);
            Cards[i].GetComponent<Renderer>().enabled = true;
            Cards[i].GetComponent<Renderer>().material = mats[Cards[i].GetComponent<MatchCard>().Value];
            Cards[i].GetComponent<Renderer>().enabled = false;
        }
        resetButton.SetActive(false);
    }

}
