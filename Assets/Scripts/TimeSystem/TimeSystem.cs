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
    [SerializeField] private Color whiteColorLight; // = new Color(255, 255, 255);
    [SerializeField] private Color yellowColorLight; // = new Color(242, 248, 174);
    [SerializeField] private Color orangeColorLight; // = new Color(240, 217, 163);
    [SerializeField] private Color softNightColorLight; // = new Color(186, 186, 186);
    [SerializeField] private Color darkNightColorLight; // = new Color(126, 126, 126);

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
    }

    void Update()
    {
        TimeCalculation();
        DayAndNightController();
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
        if (0 <= hour && hour < 4)
        {
            globalLight.color = darkNightColorLight;
        }
        else if(4 <= hour && hour < 5)
        {
            globalLight.color = softNightColorLight;
        }
        else if(5 <= hour && hour < 12)
        {
            globalLight.color = whiteColorLight;
        }
        else if(12 <= hour && hour < 16)
        {
            globalLight.color = yellowColorLight;
        }
        else if(16 <= hour && hour < 18)
        {
            globalLight.color = orangeColorLight;
        }
        else if(18 <= hour && hour < 24)
        {
            globalLight.color = darkNightColorLight;
        }
    }
}
