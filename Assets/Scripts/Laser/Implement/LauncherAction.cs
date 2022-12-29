using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAction : LaserSensor {

    private ShootLaser laserLauncher;
	
	private List<ShootLaser> laserPointers;

	public int targetIntensity;

	public bool isRequiredRed;
	public bool isRequiredGreen;
	public bool isRequiredBlue;

	private bool isOn {
		get {
			int intensity = 0;
			int color = 0;
			for (int i = 0; i < laserPointers.Count; i++) {
				var obj = laserPointers[i].MakeCloneLaserBeam();
				if (obj.CheckRayCastForTarget(gameObject)) {
					intensity += laserPointers[i].intensity;
					color += ColorHelper.getColorValue(laserPointers[i].color);
				}
			}
			return this.targetIntensity <= intensity;
				
		}
	}

	void Start() {
        laserPointers = new List<ShootLaser>();
        laserLauncher = GetComponent<ShootLaser>();
        laserLauncher.enabled = false;
    }

    void Update() {
        if(isOn) {
            laserLauncher.enabled = true;
        } else {
            laserLauncher.DestroyBeam();
            laserLauncher.enabled = false;
        }
    }

    public override void OnHitLaser(LaserBeam laser) {
		if (laserPointers.Contains(laser.parent) == false) {
			laserPointers.Add(laser.parent);
		}
    }

	public override void OnHitLaserWithMirror(LaserBeam laser, int intensity) {
		throw new System.NotImplementedException();
	}
}
