using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class ArrowMovement : MonoBehaviour
{
	// Token: 0x06000264 RID: 612 RVA: 0x0000CB9C File Offset: 0x0000AD9C
	private void Start()
	{
		this.spawnTime = Time.time;
		this.lastUpdateTime = Time.time;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
	public void Init(float arrowSpeed, global::ItemRepresentation itemRep, global::IBowWeaponItem itemInstance, bool firedLocal)
	{
		this.speedPerSec = arrowSpeed;
		if (itemRep != null && itemInstance != null)
		{
			this._myBow = itemRep;
			this._myItemInstance = itemInstance;
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
	private void OnDestroy()
	{
		if (!this.impacted)
		{
			this.TryReportMiss();
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
	private void Update()
	{
		if (this.impacted)
		{
			return;
		}
		float num = Time.time - this.lastUpdateTime;
		this.lastUpdateTime = Time.time;
		RaycastHit raycastHit = default(RaycastHit);
		RaycastHit2 invalid = RaycastHit2.invalid;
		base.transform.Rotate(Vector3.right, this.dropDegreesPerSec * num);
		Ray ray;
		ray..ctor(base.transform.position, base.transform.forward);
		float num2 = this.speedPerSec * num;
		bool flag = true;
		if (Physics2.Raycast2(ray, ref invalid, this.speedPerSec * num, this.layerMask))
		{
			Vector3 normal;
			Vector3 point;
			GameObject gameObject;
			Rigidbody rigidbody;
			if (flag)
			{
				normal = invalid.normal;
				point = invalid.point;
				gameObject = invalid.gameObject;
				rigidbody = invalid.rigidbody;
			}
			else
			{
				normal = raycastHit.normal;
				point = raycastHit.point;
				gameObject = raycastHit.collider.gameObject;
				rigidbody = raycastHit.rigidbody;
			}
			Quaternion quaternion = Quaternion.LookRotation(normal);
			Vector3 vector = Vector3.zero;
			int layer = gameObject.layer;
			if (rigidbody && !rigidbody.isKinematic && !rigidbody.CompareTag("Door"))
			{
				rigidbody.AddForceAtPosition(Vector3.up * 200f, point);
				rigidbody.AddForceAtPosition(ray.direction * 1000f, point);
			}
			if (layer != 17 && layer != 18 && layer != 27 && layer != 21)
			{
				vector = point + normal * 0.01f;
			}
			this.impacted = true;
			base.transform.position = point;
			this.TryReportHit(gameObject);
			base.transform.parent = gameObject.transform;
			TrailRenderer component = base.GetComponent<TrailRenderer>();
			if (component)
			{
				component.enabled = false;
			}
			base.audio.enabled = false;
			if (gameObject)
			{
				global::SurfaceInfoObject surfaceInfoFor = global::SurfaceInfo.GetSurfaceInfoFor(gameObject, point);
				surfaceInfoFor.GetImpactEffect(global::SurfaceInfoObject.ImpactType.Bullet);
				Object @object = Object.Instantiate(surfaceInfoFor.GetImpactEffect(global::SurfaceInfoObject.ImpactType.Bullet), point, quaternion);
				Object.Destroy(@object, 1.5f);
				this.TryReportMiss();
			}
			Object.Destroy(base.gameObject, 20f);
		}
		else
		{
			base.transform.position += base.transform.forward * this.speedPerSec * num;
			this.distance += this.speedPerSec * num;
		}
		if (this.distance > this.maxRange || Time.time - this.spawnTime > this.maxLifeTime)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000CECC File Offset: 0x0000B0CC
	public void TryReportMiss()
	{
		if (this._myItemInstance != null && !this.reported)
		{
			this.reported = true;
			this._myItemInstance.ArrowReportMiss(this);
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
	public void TryReportHit(GameObject hitGameObject)
	{
		if (this._myItemInstance != null && !this.reported)
		{
			this.reported = true;
			IDMain main = IDBase.GetMain(hitGameObject);
			this._myItemInstance.ArrowReportHit(main, this);
		}
	}

	// Token: 0x04000191 RID: 401
	public bool impacted;

	// Token: 0x04000192 RID: 402
	public float speedPerSec = 80f;

	// Token: 0x04000193 RID: 403
	public float maxRange = 1000f;

	// Token: 0x04000194 RID: 404
	private float maxLifeTime = 4f;

	// Token: 0x04000195 RID: 405
	public float lastUpdateTime;

	// Token: 0x04000196 RID: 406
	public float spawnTime;

	// Token: 0x04000197 RID: 407
	private int layerMask = 406721553;

	// Token: 0x04000198 RID: 408
	private float distance;

	// Token: 0x04000199 RID: 409
	public float dropDegreesPerSec = 5f;

	// Token: 0x0400019A RID: 410
	private bool reported;

	// Token: 0x0400019B RID: 411
	public global::ItemRepresentation _myBow;

	// Token: 0x0400019C RID: 412
	public global::IBowWeaponItem _myItemInstance;
}
