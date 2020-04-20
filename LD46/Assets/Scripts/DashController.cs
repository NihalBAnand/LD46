using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DashController : MonoBehaviour
{
    //constants for no hardcoding lol
    public float HourLength = 5f; // CHANGE TO 5 FOR FINAL DEPLOY
    private const int MaxMoney = 999000;

    //Stats
    public int money = 0;
    public float percentDecrease = 2.5702f;
    public int percent = 0;
    public int maxRads = 250;
    public int radiation = 0;
    public int temp = 150;
    public int population = 100;
    public int popIncRate = 15;
    public int popIncFreq = 1;
    public int dailyPowerUse;
    public int waterLevel = 90;
    public int MaxWater = 100;
    public int controlRods = 6;
    public int rodInUse = 5;
    public int excessStorage = 0;
    public int priceOfPower = 1;
    public int storageCapacity = 50000;
    public int strike = 0;
    public int powerGain = 0;
    public int efficiency = 1;
    public int KwH = 0;
    public int numEvents = 4;
    public int News = 0;
    public int maxTemp = 500;
    public int dispExcessStorage = 0;


    //time
    public int time = 0;
    public int day = 1;
    public int speedstate= 1;
    public GameObject fastforwardOn;

    //flush
    public int flushState = 0; // 0-3


    //flow control
    public bool paused = false;
    public bool shopping = false;
    public bool manual = false;

    public Text PowerValue;
    public Slider Sell;
    public Text rods;
    public Text KwHval;
    public Text PercentVal;

    public Text tempVal;
    public Text timeVal;
    public Text KwHTodayVAl;
    public Text batteryVal;
    public Text moneyVal;
    public Text shopMoney;

    public Text newsText;
    public Text calText;

    public GameObject radDial;
    public float radRotation = 90;

    public GameObject manualO;

    public Image water;

    public Image battery;

    public GameObject shop;
    public Text SWater;
    public Text SBatt;
    public Text SHeat;
    public Text SRad;
    public Text SRod;

    public int waterP = 30000;
    public int battP = 30000;
    public int heatP = 30000;
    public int radP = 30000;
    public int rodP = 30000;

    //dash board sprites
    public Image strikeboard;
    public Sprite strike0;
    public Sprite strike1;
    public Sprite strike2;
    private List<Sprite> strikes = new List<Sprite>();
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
    //shop pos = 43 -5.722e-06 607.7238 398.3781
    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
        manualO.SetActive(false);
        //init <reactor states> list
        states.Add(state0);
        states.Add(state1);
        states.Add(state2);
        states.Add(state3);
        states.Add(state4);
        states.Add(state5);

        //init dashboard
        strikes.Add(strike0);
        strikes.Add(strike1);
        strikes.Add(strike2);
        dailyPowerUse = population * 24;

        rods.text = rodInUse.ToString();

        tempVal.text = temp.ToString();
        KwHTodayVAl.text = Math.Round((double)dailyPowerUse / 1000, 1).ToString();

        PercentVal.text = percent.ToString();
        timeVal.text = time.ToString();
        newsText.text = "Study proves that math is related to science";

        //DO LAST NO MATTER WHAT
        //I MEAN IT
        StartCoroutine(Hour());
    }

    private float dispKwh;
    
    // Update is called once per frame
    void Update()
    {

        if (day == 29) SceneManager.LoadScene("WinScreen");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GlobalController.Instance.manual = true;
            
        }
        if (speedstate<0)
        {
            fastforwardOn.SetActive(true);
        }
        else fastforwardOn.SetActive(false);

        //shop
        if (shopping)
        {
            shop.SetActive(true);
            paused = true;
        }
        else if (!shopping)
        {
            shop.SetActive(false);
        }
        if (GlobalController.Instance.manual)
        {
            manualO.SetActive(true);
            paused = true;
        }
        else if (!GlobalController.Instance.manual)
        {
            manualO.SetActive(false);
        }
        if (!(shopping) && !(GlobalController.Instance.manual))
        {
            paused = false;
        }
        SliderPower();
        rods.text = rodInUse.ToString();
        moneyVal.text = (money/1000).ToString();

        shopMoney.text = (money/1000).ToString() + "k";

        tempVal.text = temp.ToString();
        KwHTodayVAl.text = Math.Round((double)dailyPowerUse / 1000, 1).ToString();

        PercentVal.text = percent.ToString();
        timeVal.text = time.ToString();
        if (KwH > dailyPowerUse) dispExcessStorage = excessStorage + (KwH - dailyPowerUse);
        if (dispExcessStorage > storageCapacity) dispExcessStorage = storageCapacity;
        batteryVal.text = Math.Round(((float)dispExcessStorage / 1000), 1).ToString();
        calText.text = day.ToString();

        //SHOP
        SWater.text = (waterP/1000).ToString() + "k";
        SBatt.text = (battP/1000).ToString() + "k";
        SHeat.text = (heatP/1000).ToString() + "k";
        SRad.text = (radP/1000).ToString() + "k";
        SRod.text = (rodP/1000).ToString() + "k";


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
        Debug.Log((float)excessStorage / (float)storageCapacity);

        battery.rectTransform.sizeDelta = new Vector2(100, 400 * ((float)dispExcessStorage / (float)storageCapacity));

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

        if (radiation > maxRads) radiation = maxRads;
        if (money > MaxMoney)
            money = MaxMoney;

        if (percent > 100)
            percent = 100;
        strikeboard.sprite = strikes[strike];
        reactor.sprite = states[state];
        if (waterLevel < 0)
            waterLevel = 0;


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
            popIncRate += 3;
            population += popIncRate;
            dailyPowerUse = population * 24;
        }
        waterLevel = MaxWater - 10;
        dailyPowerUse = population * 24;
        time = 0;
        day += 1;
        KwHTodayVAl.text = dailyPowerUse.ToString();
        if (day>0) shopping = true;
        if (day % 7 == 0)
        {
            News += 1;
            UpdateNews();
        }
        KwH = 0;
        
    }

    public void DoneShopping()
    {
        shopping = false;
    }

    private void UpdateNews()
    {
        if(News == 0)
        {
            newsText.text = "Man argues with Shampoo bottles for four hours";
        }
        if(News == 1)
        {
            newsText.text = "Most Americans think Bolivia is in the Arctic Circle";
        }
        if(News == 2)
        {
            newsText.text = "Easter egg hunts relocated to video games";
        }
        if(News == 3)
        {
            newsText.text = "High blood pressure cases spike as Ludum Dare deadline approaches";
        }
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
            money += (priceOfPower+10) * (KwH);
        }
        else
        {
            money += (priceOfPower) * (KwH);
            money += 10;
        }
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
        if ((int)UnityEngine.Random.Range(1f, 100f) <= percent)
        {
            SceneManager.LoadScene("Lose_Meltdown");
        }
        else if(temp >= maxTemp)
        {
            SceneManager.LoadScene("Lose_Temperature");
        }
        else if (radiation >= maxRads)
        {
            SceneManager.LoadScene("Lose_Radiation");
        }
        else if(strike == 3)
        {
            SceneManager.LoadScene("StrikeOut");
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

        efficiency = Mathf.RoundToInt((float)Math.Pow(controlRods - rodInUse, 2));
        percent = Mathf.RoundToInt((float)Math.Pow((controlRods - rodInUse), percentDecrease));

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
                percent += 10;
                if (percent > 100) percent = 100;
                break;

            case 3:
                flushState = 0;
                break;
        }

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
        if (speedstate > 0)
        {
            HourLength = 5f;
        }
        else HourLength = 3f;

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
    public void Fastforward()
    {
        speedstate *= -1;
    }


    public void BuyWater()
    {
        if (money >= waterP)
        {
            money -= waterP;
            waterP += 20000;
            MaxWater += 20;
        }
    }

    public void BuyBatt()
    {
        if (money >= battP)
        {
            money -= battP;
            battP += 20000;
            storageCapacity += 10000;
        }
    }

    public void BuyHeat()
    {
        if (money >= heatP)
        {
            money -= heatP;
            heatP += 20000;
            maxTemp += 500;
        }
    }

    public void BuyRad()
    {
        if (money >= radP)
        {
            money -= radP;
            radP += 20000;
            maxRads += 25;
        }
    }

    public void BuyRod()
    {
        if (money >= rodP)
        {
            money -= rodP;
            rodP += 20000;
            percentDecrease -= 0.1f;
        }
    }

    public void SliderPower()
    {
        PowerValue.text = (((float)priceOfPower/10) * excessStorage* Sell.value).ToString();
    }
    public void SellPower()
    {
        money += Mathf.RoundToInt(((float)priceOfPower/10) * excessStorage * Sell.value);
        excessStorage -= Mathf.RoundToInt(excessStorage * Sell.value);
        if (excessStorage == 1) excessStorage = 0;
    }

    public void ManualBack()
    {
        GlobalController.Instance.manual = false;
    }
}
