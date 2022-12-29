using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserBeam {

	public Vector3 pos, dir;

	public GameObject laserObj;
	public LineRenderer laser;
	Color color;
	public int intensity;
	public ShootLaser parent;
	private bool isClone = false;
	private GameObject target;
	private bool isHit = false;

	List<Vector3> laserIndices = new List<Vector3>();

	Dictionary<string, float> refractiveMaterials = new Dictionary<string, float>() {
		{"Air", 1.0f},
		{"Glass", 1.5f},
		{ "Water", 1.3f}
	};

	public LaserBeam(Vector3 pos, Vector3 dir, float width, Color color, Material material, string medium, int intensity, ShootLaser parent, bool isClone) {
		this.parent = parent;
		this.laser = new LineRenderer();
		this.laserObj = new GameObject();
		this.laserObj.name = isClone ? "Clone Laser" : "Laser Beam";
		this.laserObj.layer = LayerMask.NameToLayer("Laser");
		this.isClone = isClone;

		this.color = color;
		this.intensity = intensity;

		this.pos = pos;
		this.dir = dir;

		this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
		this.laser.startWidth = width;
		this.laser.endWidth = width;

		this.laser.material = material;
		this.laser.startColor = color;
		this.laser.endColor = color;
		this.laser.material.color = color;
	}

	public void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, string medium) {
		laserIndices.Add(pos);

		Ray ray = new Ray(pos, dir);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100, -1)) {
			CheckHit(hit, dir, laser, medium, ray);
		} else {
			laserIndices.Add(ray.GetPoint(100));
			UpdateLaser();
		}
	}

	public bool CheckRayCastForTarget(GameObject gameObject) {
		target = gameObject;
		CastRay(this.pos, this.dir, this.laser, this.parent.medium);
		GameObject.Destroy(this.laserObj);
		return isHit;
	}

	void UpdateLaser() {
		int count = 0;
		laser.positionCount = laserIndices.Count;

		foreach (Vector3 idx in laserIndices) {
			laser.SetPosition(count, idx);
			count++;
		}

	}

	void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, string medium, Ray ray) {
		if (hitInfo.collider.gameObject.tag == "Mirror") {
			if (hitInfo.collider.gameObject.GetComponents<LaserSensor>() != null) {
				if (isClone && target == hitInfo.collider.gameObject) {
					isHit = true;
				} else if (!isClone) {
					LaserSensor[] laserSensorArr = hitInfo.collider.gameObject.GetComponents<LaserSensor>();
					foreach (LaserSensor sensor in laserSensorArr) {
						sensor.OnHitLaserWithMirror(this, 1);
					}
				}
			}
			Vector3 pos = hitInfo.point;
			Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
			

			CastRay(pos, dir, laser, medium);
		} else if (refractiveMaterials.ContainsKey(hitInfo.collider.gameObject.tag)) {
			Vector3 pos = hitInfo.point;
			laserIndices.Add(pos);

			Vector3 newPos1 = new Vector3(
				Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.001f + pos.x,
				Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.001f + pos.y,
				Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.001f + pos.z);

			float n1 = refractiveMaterials[medium];
			float n2 = refractiveMaterials[hitInfo.collider.gameObject.tag];

			Vector3 norm = hitInfo.normal;
			Vector3 incident = direction;

			Vector3 refractedVector = Refract(n1, n2, norm, incident);

			CastRay(newPos1, refractedVector, laser, medium);
		} else if (hitInfo.collider.gameObject.GetComponents<LaserSensor>() != null) {
			laserIndices.Add(hitInfo.point);
			UpdateLaser();

			if (isClone && target == hitInfo.collider.gameObject) {
				isHit = true;
			} else if (!isClone) {
				LaserSensor[] laserSensorArr = hitInfo.collider.gameObject.GetComponents<LaserSensor>();
				foreach(LaserSensor sensor in laserSensorArr) {
					sensor.OnHitLaser(this);
				}
			}
		
		} else {
			if(hitInfo.collider.gameObject.tag == "Arrow") {
				laserIndices.Add(ray.GetPoint(100));
				CastRay(hitInfo.point + 2 * (direction), direction, laser, medium);
			}
			else {
				laserIndices.Add(hitInfo.point);
				UpdateLaser();
			}
		}
	}

	public static Vector3 Refract(float RI1, float RI2, Vector3 surfNorm, Vector3 incident) {
		surfNorm.Normalize(); //should already be normalized, but normalize just to be sure
		incident.Normalize();

		return (RI1 / RI2 * Vector3.Cross(surfNorm,
			Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(surfNorm, incident) * (RI1 / RI2 * RI1 / RI2),
			Vector3.Cross(surfNorm, incident)))).normalized;
	}

}
