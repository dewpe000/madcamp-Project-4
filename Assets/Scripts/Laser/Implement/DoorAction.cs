using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorAction : LaserSensor
{
	public GameObject targetDoor;

	public float movingDegree;
	private Vector3 startPos;
	private Vector3 destPos;
	private bool isBack;

	private Vector3 vel = Vector3.zero;
	public float smoothTime = 0.7f;

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
			return this.targetIntensity <= intensity &&
				ColorHelper.getTargetColor(isRequiredRed, isRequiredGreen, isRequiredBlue) == color;
		}
	}

	

	void Start() {
		laserPointers = new List<ShootLaser>();
		startPos = targetDoor.transform.position;
		destPos = startPos + targetDoor.transform.right * movingDegree;
	}

	void Update() {
		if (isOn) {
			Move();
		} else {
			BackMove();
		}
	}

	private void Move() {
		if (Vector3.Distance(targetDoor.transform.position, destPos) > 0.1f) {
			targetDoor.transform.position = Vector3.SmoothDamp(targetDoor.transform.position, destPos, ref vel, smoothTime);

		}
	}

	private void BackMove() {
		if (Vector3.Distance(targetDoor.transform.position, startPos) > 0.1f) {
			targetDoor.transform.position = Vector3.SmoothDamp(targetDoor.transform.position, startPos, ref vel, smoothTime);
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

