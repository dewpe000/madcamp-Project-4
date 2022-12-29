using UnityEngine;
using System.Collections;

public abstract class LaserSensor : MonoBehaviour
{
	public abstract void OnHitLaser(LaserBeam laser);

	public abstract void OnHitLaserWithMirror(LaserBeam laser, int intensity);
}

	