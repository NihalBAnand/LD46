using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DashController : MonoBehaviour
{
    //constants for no hardcoding lol
    private const float HourLength = 3f; // CHANGE TO 30 FOR FINAL DEPLOY
    private const int MaxMoney = 999;

    //Stats
    public int money = 0;
    public float percentDecrease = 2.5702f;
    public int percent = 0;
    public int maxRads = 250;
    public int radiation = 0;
    public int temp = 150;
    public int population = 100;
    public int popIncRate = 10;
    public int popIncFreq = 3;
    public int dailyPowerUse;
    public int waterLevel = 90;
    public int MaxWater = 100;
    public int controlRods = 6;
    public int rodInUse = 5;
    public int excessStorage = 0;
    public int priceOfPower = 1;
    public int storageCapacity = 300;
    public int strike = 0;
    public int powerGain = 0;
    public int efficiency = 1;
    public int KwH = 0;
    public int numEvents = 4;

    //time
    public int time = 0;
    public int day = 0;

    //flush
    public int flushState = 0; // 0-3


    //flow control
    public bool paused = false;

    /*
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
    */

    /*
    //Clock sprites
    public GameObject ClockDigit1;
    public GameObject ClockDigit2;
    private SpriteRenderer CD1;
    private SpriteRenderer CD2;
    
    
    //Money Sprites
    public GameObject MoneyDigit1;
    public GameObject MoneyDigit2;
    public GameObject MoneyDigit3;
    private SpriteRenderer MD1;
    private SpriteRenderer MD2;
    private SpriteRenderer MD3;

    //Temperature Sprites
    public GameObject TempDigit1;
    public GameObject TempDigit2;
    public GameObject TempDigit3;
    private SpriteRenderer TD1;
    private SpriteRenderer TD2;
    private SpriteRenderer TD3;

    //Kilowatt-Hours Sprites
    public GameObject KwDigit1;
    public GameObject KwDigit2;
    public GameObject KwDigit3;
    private SpriteRenderer Kw1;
    private SpriteRenderer Kw2;
    private SpriteRenderer Kw3;

    //Percent Sprites
    public GameObject PercentDigit1;
    public GameObject PercentDigit2;
    public GameObject PercentDigit3;
    private SpriteRenderer PD1;
    private SpriteRenderer PD2;
    private SpriteRenderer PD3;
    */
    public Text rods;
    public Text KwHval;
    public Text PercentVal;

    public Text tempVal;
    public Text timeVal;
    public Text KwHTodayVAl;

    public Text moneyVal;

    public GameObject radDial;
    public float radRotation = 90;

    public Image water;

    public Image battery;

    //Reactor Sprites
    public Image reactor;
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
        /*
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
        */

        //init <reactor states> list
        states.Add(state0);
        states.Add(state1);
        states.Add(state2);
        states.Add(state3);
        states.Add(state4);
        states.Add(state5);

        /*<--Start UI inits

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

        
        

        */
        dailyPowerUse = population * 24;

        rods.text = rodInUse.ToString();

        tempVal.text = temp.ToString();
        KwHTodayVAl.text = dailyPowerUse.ToString();

        PercentVal.text = percent.ToString();
        timeVal.text = time.ToString();

        //radDial.transform.rotation

        



        //DO LAST NO MATTER WHAT
        //I MEAN IT
        StartCoroutine(Hour());
    }

    private float dispKwh;
    
    // Update is called once per frame
    void Update()
    {
        rods.text = rodInUse.ToString();
        moneyVal.text = money.ToString();

        tempVal.text = temp.ToString();
        KwHTodayVAl.text = dailyPowerUse.ToString();

        PercentVal.text = percent.ToString();
        timeVal.text = time.ToString();

        radRotation = 180 * ((float)radiation / (float)maxRads);
        if (radRotation >= 90)
        {
            radRotation = -radRotation + 90;
        }
        else
        {
            radRotation = 90 - radRotation;
        }
        radDial.transform.rotation = Quaternion.Euler(0, 0, radRotation);

        water.rectTransform.sizeDelta = new Vector2(100, 100 *((float)waterLevel / (float)MaxWater));

        battery.rectTransform.sizeDelta = new Vector2(100, 400 * ((float)excessStorage / (float)storageCapacity));

        /*
        dispKwh = KwH;
        dispKwh =(float) Math.Round((double)(dispKwh / 1000), 3);
        KwHval.text = dispKwh.ToString();
        <--Start reactor debug
        if (Input.GetKeyDown(KeyCode.RightArrow))
            state += 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            state -= 1;

        if (state < 0)
            state = 0;
        if (state > 5)
            state = 5;
        //End reactor debug -->*/

        if (time >= 24)
        {
            dayend();
        }

        if (temp > 470)
            state = 4;
        if (temp > 400 && temp <= 470)
            state = 3;
        if (temp > 250 && temp <= 400)
            state = 2;
        if (radiation > 225)
            state = 5;
        if (radiation > 100 && radiation <=225)
            state = 1;
        if (radiation <= 100 && temp <= 250)
            state = 0;
        

        if (money > MaxMoney)
            money = MaxMoney;

        if (percent > 100)
            percent = 100;
        reactor.sprite = states[state];
        if (waterLevel < 0)
            waterLevel = 0;

        /*<-- Start UI display logic

        //clock
        CD1.sprite = nums[Mathf.RoundToInt(time / 10)];
        CD2.sprite = nums[time % 10];

        //reactor
        

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

        //End UI display logic -->*/
    }

    public void Flush()
    {
        if (flushState < 1)
            flushState = 1;
    }

    private void dayend()
    {
        paused = true;
        moneyfrompower();
        if (day % popIncFreq == 0)
        {
            population += popIncRate;
            dailyPowerUse = population * 24;
        }
        waterLevel = MaxWater - 10;
        dailyPowerUse = population * 24;
        time = 0;
        day += 1;
        KwHTodayVAl.text = dailyPowerUse.ToString();
        updateEvent();
        paused = false;
        
    }

    private void updateEvent()
    {
        
    }

    private void moneyfrompower()
    {
        if(dailyPowerUse<KwH)
        {
            excessStorage += (KwH - dailyPowerUse);
            if (excessStorage > storageCapacity)
            {
                excessStorage = storageCapacity;
                money += (priceOfPower) * (dailyPowerUse);
            }
            money += 10;
        }
        else if(dailyPowerUse>KwH)
        {
            strike += 1;
            money += (priceOfPower + 10) * (KwH);
        }
        else
        {
            money += (priceOfPower) * (KwH);
            money += 10;
        }
        money /= 1000;
    }

    IEnumerator Hour() 
    {
        //Every hour logic
        yield return new WaitForSeconds(HourLength);
        if (!paused)
        {
            endHour();
            GameOver();
 
        }

        StartCoroutine(Hour());
    }

    private void GameOver()
    {
        if (UnityEngine.Random.Range(0f, 100f) <= percent)
        {
            Debug.Log("LMAO you lost - meltdown");
        }
        else if(temp > 499)
        {
            Debug.Log("LMAO you lost - temp");
        }
        else if (radiation >= maxRads)
        {
            Debug.Log("LMAO you lost - rads");
        }

    }

    private void endHour()
    {
        time++;

        timeVal.text = time.ToString();
        if (flushState == 1)
        {
            radiation = 0;
        }

        if (waterLevel < 90 && waterLevel > 0)
            temp += Mathf.RoundToInt((MaxWater - waterLevel) / 3);
        else
            temp -= 5;

        //efficiency = Mathf.RoundToInt((float)Math.Pow(controlRods - rodInUse, 2));

        //percent = Mathf.RoundToInt((float)Math.Pow(2.15,(controlRods - rodInUse)));
        switch (flushState)
        {
            case 1:
                radiation = 0;
                percent += 10;
                waterLevel = 100;
                if (percent > 100) percent = 100;
                flushState = 2;
                break;


            case 2:
                flushState = 3;
                break;

            case 3:
                percent -= 10;
                if (percent < 0) percent = 0;
                flushState = 0;
                break;
        }
    


        efficiency = Mathf.RoundToInt((float)Math.Pow(controlRods - rodInUse, 2));
        percent = Mathf.RoundToInt((float)Math.Pow((controlRods - rodInUse), percentDecrease));
        
        if (efficiency <= 1)
            efficiency = 1;
        if (temp - 100>=0)
        {
            waterLevel -= Mathf.RoundToInt((temp - 100) / 10);
            powerGain = (temp - 100) * 3;
            powerGain *= efficiency;
        }
        else
            waterLevel -= 2;
        KwH += powerGain;
        if (flushState < 1)
            radiation += 5;
        //TextUpdates
        dispKwh = KwH;
        dispKwh = (float)Math.Round(((double)dispKwh / 1000), 1);
        KwHval.text = dispKwh.ToString();
        PercentVal.text = percent.ToString();
        tempVal.text = temp.ToString();

    }

    public void AddControlRods()
    {
        if (rodInUse < controlRods)
            rodInUse += 1;
    }
    public void RemoveControlRods()
    {
        if (rodInUse > 0)
            rodInUse -= 1;
    }

    public void AddWater()
    {
        if (waterLevel + 10 <= MaxWater)
        {
            waterLevel += 10;
            temp -= 10;
        }
    }

}
