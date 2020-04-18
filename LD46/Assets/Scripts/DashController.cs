using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DashController : MonoBehaviour
{
    //constants for no hardcoding lol
    private const float HourLength = 3f; // CHANGE TO 30 FOR FINAL DEPLOY
    private const int MaxMoney = 999;

    //Number sprites for digits
    public Sprite num0;
    public Sprite num1;
    public Sprite num2;
    public Sprite num3;
    public Sprite num4;
    public Sprite num5;
    public Sprite num6;
    public Sprite num7;
    public Sprite num8;
    public Sprite num9;
    private List<Sprite> nums = new List<Sprite>();

    //Clock sprites
    public GameObject ClockDigit1;
    public GameObject ClockDigit2;
    private SpriteRenderer CD1;
    private SpriteRenderer CD2;
    public int time = 0;
    public int day = 0;

    //Money Sprites
    public GameObject MoneyDigit1;
    public GameObject MoneyDigit2;
    public GameObject MoneyDigit3;
    private SpriteRenderer MD1;
    private SpriteRenderer MD2;
    private SpriteRenderer MD3;
    public int money = 0;

    //Temperature Sprites
    public GameObject TempDigit1;
    public GameObject TempDigit2;
    public GameObject TempDigit3;
    private SpriteRenderer TD1;
    private SpriteRenderer TD2;
    private SpriteRenderer TD3;
    public int temp = 0;

    //Kilowatt-Hours Sprites
    public GameObject KwDigit1;
    public GameObject KwDigit2;
    public GameObject KwDigit3;
    private SpriteRenderer Kw1;
    private SpriteRenderer Kw2;
    private SpriteRenderer Kw3;
    public int KwH = 0;

    //Percent Sprites
    public GameObject PercentDigit1;
    public GameObject PercentDigit2;
    public GameObject PercentDigit3;
    private SpriteRenderer PD1;
    private SpriteRenderer PD2;
    private SpriteRenderer PD3;
    public int percent = 0;

    //Reactor Sprites
    public GameObject reactor;
    private SpriteRenderer reactorSR;
    public Sprite state0;
    public Sprite state1;
    public Sprite state2;
    public Sprite state3;
    public Sprite state4;
    public Sprite state5;
    private List<Sprite> states = new List<Sprite>();
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        //init <digit sprites> list
        nums.Add(num0);
        nums.Add(num1);
        nums.Add(num2);
        nums.Add(num3);
        nums.Add(num4);
        nums.Add(num5);
        nums.Add(num6);
        nums.Add(num7);
        nums.Add(num8);
        nums.Add(num9);

        //init <reactor states> list
        states.Add(state0);
        states.Add(state1);
        states.Add(state2);
        states.Add(state3);
        states.Add(state4);
        states.Add(state5);

        //<--Start UI inits

        //clock
        CD1 = ClockDigit1.GetComponent<SpriteRenderer>();
        CD2 = ClockDigit2.GetComponent<SpriteRenderer>();

        //money
        MD1 = MoneyDigit1.GetComponent<SpriteRenderer>();
        MD2 = MoneyDigit2.GetComponent<SpriteRenderer>();
        MD3 = MoneyDigit3.GetComponent<SpriteRenderer>();

        //temperature
        TD1 = TempDigit1.GetComponent<SpriteRenderer>();
        TD2 = TempDigit2.GetComponent<SpriteRenderer>();
        TD3 = TempDigit3.GetComponent<SpriteRenderer>();

        //kilowatt-hours
        Kw1 = KwDigit1.GetComponent<SpriteRenderer>();
        Kw2 = KwDigit2.GetComponent<SpriteRenderer>();
        Kw3 = KwDigit3.GetComponent<SpriteRenderer>();

        //percent
        PD1 = PercentDigit1.GetComponent<SpriteRenderer>();
        PD2 = PercentDigit2.GetComponent<SpriteRenderer>();
        PD3 = PercentDigit3.GetComponent<SpriteRenderer>();

        //reactor
        reactorSR = reactor.GetComponent<SpriteRenderer>();

        //End UI inits -->

        //DO LAST NO MATTER WHAT
        //I MEAN IT
        StartCoroutine(Hour());
    }



    // Update is called once per frame
    void Update()
    {   
        //<--Start reactor debug
        if (Input.GetKeyDown(KeyCode.RightArrow))
            state += 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            state -= 1;

        if (state < 0)
            state = 0;
        if (state > 5)
            state = 5;
        //End reactor debug -->

        if (time >= 24)
        {
            day += 1;
            time = 0;
        }

        if (money > MaxMoney)
            money = MaxMoney;

        if (percent > 100)
            percent = 100;

        //<-- Start UI display logic

        //clock
        CD1.sprite = nums[Mathf.RoundToInt(time / 10)];
        CD2.sprite = nums[time % 10];

        //reactor
        reactorSR.sprite = states[state];

        //money
        MD1.sprite = nums[Mathf.RoundToInt(money / 100)];
        MD2.sprite = nums[Mathf.RoundToInt((money % 100) / 10)];
        MD3.sprite = nums[money % 10];

        //temperature
        TD1.sprite = nums[Mathf.RoundToInt(temp / 100)];
        TD2.sprite = nums[Mathf.RoundToInt((temp % 100) / 10)];
        TD3.sprite = nums[temp % 10];

        //kilowatt-hours
        Kw1.sprite = nums[Mathf.RoundToInt(KwH / 100)];
        Kw2.sprite = nums[Mathf.RoundToInt((KwH % 100) / 10)];
        Kw3.sprite = nums[KwH % 10];

        //percent
        PD1.sprite = nums[Mathf.RoundToInt(percent / 100)];
        PD2.sprite = nums[Mathf.RoundToInt((percent % 100) / 10)];
        PD3.sprite = nums[percent % 10];

        //End UI display logic -->
    }

    IEnumerator Hour() 
    {
        yield return new WaitForSeconds(HourLength); 
        time++;
        StartCoroutine(Hour());
    }
}
