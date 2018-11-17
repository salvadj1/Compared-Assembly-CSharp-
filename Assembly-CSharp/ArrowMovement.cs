using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class ArrowMovement : MonoBehaviour
{
	// Token: 0x060001F2 RID: 498 RVA: 0x0000B5F4 File Offset: 0x000097F4
	private void Start()
	{
		this.spawnTime = Time.time;
		this.lastUpdateTime = Time.time;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000B60C File Offset: 0x0000980C
	public void Init(float arrowSpeed, ItemRepresentation itemRep, IBowWeaponItem itemInstance, bool firedLocal)
	{
		this.speedPerSec = arrowSpeed;
		if (itemRep != null && itemInstance != null)
		{
			this._myBow = itemRep;
			this._myItemInstance = itemInstance;
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000B638 File Offset: 0x00009838
	private void OnDestroy()
	{
		if (!this.impacted)
		{
			this.TryReportMiss();
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000B64C File Offset: 0x0000984C
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
				SurfaceInfoObject surfaceInfoFor = SurfaceInfo.GetSurfaceInfoFor(gameObject, point);
				surfaceInfoFor.GetImpactEffect(SurfaceInfoObject.ImpactType.Bullet);
				Object @object = Object.Instantiate(surfaceInfoFor.GetImpactEffect(SurfaceInfoObject.ImpactType.Bullet), point, quaternion);
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

	// Token: 0x060001F6 RID: 502 RVA: 0x0000B924 File Offset: 0x00009B24
	public void TryReportMiss()
	{
		if (this._myItemInstance != null && !this.reported)
		{
			this.reported = true;
			this._myItemInstance.ArrowReportMiss(this);
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0000B950 File Offset: 0x00009B50
	public void TryReportHit(GameObject hitGameObject)
	{
		if (this._myItemInstance != null && !this.reported)
		{
			this.reported = true;
			IDMain main = IDBase.GetMain(hitGameObject);
			this._myItemInstance.ArrowReportHit(main, this);
		}
	}

	// Token: 0x0400012F RID: 303
	public bool impacted;

	// Token: 0x04000130 RID: 304
	public float speedPerSec = 80f;

	// Token: 0x04000131 RID: 305
	public float maxRange = 1000f;

	// Token: 0x04000132 RID: 306
	private float maxLifeTime = 4f;

	// Token: 0x04000133 RID: 307
	public float lastUpdateTime;

	// Token: 0x04000134 RID: 308
	public float spawnTime;

	// Token: 0x04000135 RID: 309
	private int layerMask = 406721553;

	// Token: 0x04000136 RID: 310
	private float distance;

	// Token: 0x04000137 RID: 311
	public float dropDegreesPerSec = 5f;

	// Token: 0x04000138 RID: 312
	private bool reported;

	// Token: 0x04000139 RID: 313
	public ItemRepresentation _myBow;

	// Token: 0x0400013A RID: 314
	public IBowWeaponItem _myItemInstance;
}
