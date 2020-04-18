using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DashController : MonoBehaviour
{
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


    public GameObject ClockDigit1;
    public GameObject ClockDigit2;
    private SpriteRenderer CD1;
    private SpriteRenderer CD2;
    public int time = 0;

    public GameObject MoneyDigit1;
    public GameObject MoneyDigit2;
    public GameObject MoneyDigit3;
    private SpriteRenderer MD1;
    private SpriteRenderer MD2;
    private SpriteRenderer MD3;
    public int money = 0;

    public GameObject TempDigit1;
    public GameObject TempDigit2;
    public GameObject TempDigit3;
    private SpriteRenderer TD1;
    private SpriteRenderer TD2;
    private SpriteRenderer TD3;
    public int temp = 0;

    //reactor stuff
    //Random

    // Start is called before the first frame update
    void Start()
    {
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

        CD1 = ClockDigit1.GetComponent<SpriteRenderer>();
        CD2 = ClockDigit2.GetComponent<SpriteRenderer>();

        MD1 = MoneyDigit1.GetComponent<SpriteRenderer>();
        MD2 = MoneyDigit2.GetComponent<SpriteRenderer>();
        MD3 = MoneyDigit3.GetComponent<SpriteRenderer>();

        TD1 = TempDigit1.GetComponent<SpriteRenderer>();
        TD2 = TempDigit2.GetComponent<SpriteRenderer>();
        TD3 = TempDigit3.GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        CD1.sprite = nums[Mathf.RoundToInt(time / 10)];
        CD2.sprite = nums[time % 10];

        if (money > 999)
            money = 999;

        MD1.sprite = nums[Mathf.RoundToInt(money / 100)];
        MD2.sprite = nums[Mathf.RoundToInt((money % 100) / 10)];
        MD3.sprite = nums[money % 10];

        TD1.sprite = nums[Mathf.RoundToInt(temp / 100)];
        TD2.sprite = nums[Mathf.RoundToInt((temp % 100) / 10)];
        TD3.sprite = nums[temp % 10];
    }
}
