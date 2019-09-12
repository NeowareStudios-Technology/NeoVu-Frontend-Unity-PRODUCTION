using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingGame : MonoBehaviour
{
    public GameObject[] Cards;
    public List<int> cardNumbers;
    public Material[] mats;
    public MatchCard card1;
    public MatchCard card2;
    public int cardCount = 0;
    private int matched;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < Cards.Length; i++)
        {
            int num = Random.Range(0, cardNumbers.Count);
            Cards[i].GetComponent<MatchCard>().Value = cardNumbers[num];
            cardNumbers.RemoveAt(num);
            Cards[i].GetComponent<Renderer>().enabled = true;
            Cards[i].GetComponent<Renderer>().material = mats[Cards[i].GetComponent<MatchCard>().Value];
            Cards[i].GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(cardCount >= 2)
        {
            Compare();
        }
        
    }

     public void Compare()
    {
        if(card1.Value == card2.Value)
        {
            Destroy(card1.gameObject);
            Destroy(card2.gameObject);
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
        card1 = null;
        card2 = null;
        cardCount = 0;
    }
}
