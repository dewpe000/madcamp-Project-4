using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GaugeAction : LaserSensor {

    public Slider slider;
    private List<ShootLaser> laserPointers;

    // Start is called before the first frame update
    void Start() {
        laserPointers = new List<ShootLaser>();
    }

    // Update is called once per frame
    void Update() {
        int intensity = 0;
		for (int i = 0; i < laserPointers.Count; i++) {
			var obj = laserPointers[i].MakeCloneLaserBeam();
			if (obj.CheckRayCastForTarget(gameObject)) 
				intensity += laserPointers[i].intensity;
		}
        slider.value = intensity;

        if(slider.value >= 4) {
            Debug.Log("ending");
            if (Input.GetKeyDown(KeyCode.Y))
                SceneManager.LoadScene("EndingScene");
        }
    }

    public override void OnHitLaser(LaserBeam laser) {
        if (laserPointers.Contains(laser.parent) == false) {
            laserPointers.Add(laser.parent);
        }
    }

	public override void OnHitLaserWithMirror(LaserBeam laser, int intensity) {
        slider.value += intensity;

        if (slider.value >= 4) {
            Debug.Log("ending");
            if (Input.GetKeyDown(KeyCode.Y))
                SceneManager.LoadScene("EndingScene");
        }
    }
}
