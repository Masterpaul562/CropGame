using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum TurnState { START, ACTION,EVILENDSTEP, ENDSTEP, SHOP};

public class TurnManager : MonoBehaviour
{
    public int money;
    public int numberOfActions;
    public TurnState state;
    [SerializeField] private GameObject wheatSack;
    [SerializeField] private TextMeshProUGUI numActionDisplay;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI moneyDisplay;
    [SerializeField] private GameObject shadow;

    void Start()
    {
        shadow.SetActive(false);    
        state = TurnState.START;
        if (Game_Manager.Instance.hasStarted == false)
        {
            Game_Manager.Instance.hasStarted = true;
            StartCoroutine(Setup());
        }
        else
        {
            StartCoroutine(Action());
            money = Game_Manager.Instance.money;
        }
    }

   
    void Update()
    {
        
        numActionDisplay.text = "Actions Left: " + numberOfActions+ "/3";
        moneyDisplay.text = "Money: " + money;
        money = Game_Manager.Instance.money;
    }

      IEnumerator Setup()
    {
        wheatSack.GetComponent<InteractableBase>().seedCount = 4;
        Game_Manager.Instance.money = 5;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Action());
    }
    IEnumerator Action()
    {
        state = TurnState.ACTION;
        numberOfActions = 3;
        DisplayText("Action Phase");
        yield return new WaitForSeconds(1.5f);
        TurnTextOff();
        while (numberOfActions > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine (EvilEndstep());
    }
    IEnumerator EvilEndstep()
    {
        state = TurnState.EVILENDSTEP;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Endstep());
    }
    IEnumerator Endstep()
    {
        DisplayText("End Of Action Phase");
        state = TurnState.ENDSTEP;
        yield return new WaitForSeconds(1f);
        TurnTextOff();
        yield return new WaitForSeconds(4f);
        StartCoroutine(Shop());
    }
   IEnumerator Shop()
    {
        state = TurnState.SHOP;
        yield return null;
        SceneManager.LoadScene("Shop");
    }
    private void DisplayText ( string text)
    {
        shadow.SetActive(true);
        turnText.text = text;
    }
    private void TurnTextOff()
    {
        shadow.SetActive(false);
        turnText.text = string.Empty;
    }

}
