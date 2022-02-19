using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeSystem : MonoBehaviour
{   
    TimeSystem timeSystem;

    [Header("Time Varibles")]
    private int _day;
    private int _hour;
    private int _minute;
    private float realTimeToMinute = 1; //1 second = 1 minute in game
    private float timer;

    [Header("Day and Night Setup")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color dayColorLight; // = new Color(255, 255, 255);
    [SerializeField] private Color nightColorLight; // = new Color(126, 126, 126);
    private Color currentColor;

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
        hour = 0;
        minute = 0;
        timer = realTimeToMinute;

        //need some check before set this current color
        currentColor = nightColorLight;
    }

    void Update()
    {
        TimeCalculation();
        // DayAndNightController();
        MucNewLightController();
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
            currentColor = nightColorLight;
        if (globalLight.color == nightColorLight)
            currentColor = dayColorLight;

        globalLight.color = Color.Lerp(globalLight.color, currentColor, Time.deltaTime / 60 * 24 * realTimeToMinute);
    }
}
