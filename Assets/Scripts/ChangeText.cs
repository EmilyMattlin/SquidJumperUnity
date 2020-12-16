using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    private float countdownTime;
    public Text countdown;

    // Start is called before the first frame update
    void Start()
    {
        countdownTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTime == 3f && Time.timeSinceLevelLoad > 1.6f && Time.timeSinceLevelLoad < 3.2f)
        {
            CountDown(2f);
        }
        else if (countdownTime == 2f && Time.timeSinceLevelLoad > 3.2f && Time.timeSinceLevelLoad < 5f)
        {
            CountDown(1f);
        }
        else if (Time.timeSinceLevelLoad > 5f)
        {
            Destroy(countdown);
        }
    }

    void CountDown(float num)
    {
        countdown.text = "" + num;
        countdownTime -= 1f;
    }
}
