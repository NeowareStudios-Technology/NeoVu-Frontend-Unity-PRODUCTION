using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCard : MonoBehaviour
{
    public int Value;
    public MatchingGame manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<MatchingGame>();
        
    }

    // Update is called once per frame  
    void Update()
    {
        
    }

    public void sendValue()
    {
        if (manager.cardCount >= 2)
        {
            manager.revert();
            manager.card1 = this;
            manager.cardCount++;
        }
        else if(manager.cardCount == 0)
        {
            manager.card1 = this;
            manager.cardCount++;

        }
        else
        {
            manager.card2 = this;
            manager.cardCount++;
        }

    }
    public void flip()
    {
        this.transform.Rotate(180, 0, 180);
    }
}
