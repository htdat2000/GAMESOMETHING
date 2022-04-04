using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeSystem : MonoBehaviour
{   
    public static TimeSystem timeSystem;

    [Header("Time Variables")]
    private int _day;
    private int _hour;
    private int _minute;
    private float realTimeToMinute = 1f; //"1" second = one minute in game
    private float timer;

    [Header("Day and Night Setup")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color dayColorLight; // = new Color(255, 255, 255);
    [SerializeField] private Color nightColorLight; // = new Color(126, 126, 126);

    [SerializeField] private Color[] lightsInDay; 
    [SerializeField] private Text timerTxt; //Debug Object
    private Color nextColor;
    public UnityEvent on17h;
    public GameEvent on17hEvent;
    private bool wasRaidInDay = false;

    #region Time Properties
    public int day {get {return _day;} private set {_day = value;}}
    public int hour {get {return _hour;} private set {_hour = value;}}
    public int minute {get {return _minute;} private set {_minute = value;}}
    #endregion

    void Awake()
    {
        if(timeSystem != null)
        {
            Debug.LogError("More Than 1 TimeSystem In Scene");
            return;
        }
        
        timeSystem = this;          
    }
    void Start()
    {
        day = 1;
        hour = 6;
        minute = 55;
        timer = realTimeToMinute;

        //need some check before set this current color
        nextColor = nightColorLight;
        ImmediatelySetLight();

        if (on17h == null)
            on17h = new UnityEvent();
    }

    void Update()
    {
        TimeCalculation();
        // DayAndNightController();
        MucNewLightController();
        timerTxt.text = hour.ToString() + " : " + minute.ToString();
    }

    void TimeCalculation()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            minute++;
            timer = realTimeToMinute;
        }
        if(minute >= 60)
        {
            hour++;
            minute = 0;
        }
        if(hour >= 24)
        {
            day++;
            hour = 0;
        }
    }

    public void DayAndNightController()
    {
        if (hour <= 17 && hour >= 6)
        {
            globalLight.color = dayColorLight;
        }
        else
        {
            globalLight.color = nightColorLight;
        }
    }

    public void MucNewLightController()
    {
        if (globalLight.color == dayColorLight)
            nextColor = nightColorLight;
        if (globalLight.color == nightColorLight)
            nextColor = dayColorLight;

        // 6 - 15, 15 - 17, 17 - 6
        if (hour > 6 && hour <= 15)
        {
            nextColor = lightsInDay[0];
        }
        if (hour > 15 && hour <= 18)
        {
            nextColor = lightsInDay[1];
            if(!wasRaidInDay && hour == 17)
            {
                //on17h?.Invoke();
                on17hEvent.Invoke();
                wasRaidInDay = true;
            }
        }
        if (hour > 18 || hour <= 6)
        {
            nextColor = lightsInDay[2];
            if(wasRaidInDay)
            {
                wasRaidInDay = false;
            }
        }

        globalLight.color = Color.Lerp(globalLight.color, nextColor, Time.deltaTime / 1f);
    }
    public void ImmediatelySetLight()
    {
        if (hour > 6 && hour <= 15)
        {
            globalLight.color = lightsInDay[0];
        }
        if (hour > 15 && hour <= 17)
        {
            globalLight.color = lightsInDay[1];
        }
        if (hour > 17 || hour <= 6)
        {
            globalLight.color = lightsInDay[2];
        }
    }
}
