using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkTitle : MonoBehaviour {
    float time;

    void Update() {
        if (time < 0.5f)
        {
            GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1 - time * 1.5f);
        }
        else
        {
            GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, time) ;
            if(time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
        
    }
}
