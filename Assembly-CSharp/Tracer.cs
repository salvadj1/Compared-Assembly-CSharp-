using System;
using UnityEngine;

// Token: 0x02000564 RID: 1380
public class Tracer : MonoBehaviour
{
	// Token: 0x06002DB2 RID: 11698 RVA: 0x000AC208 File Offset: 0x000AA408
	private void Awake()
	{
		this.startTime = Time.time;
		float num = Random.Range(0.75f, 1f);
		this.startScale = new Vector3(base.transform.localScale.x * num, base.transform.localScale.y * num, base.transform.localScale.z * Random.Range(0.5f, 1f));
		base.transform.localScale = new Vector3(0f, 0f, this.startScale.z);
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x000AC2B0 File Offset: 0x000AA4B0
	public void Init(Component component, int layerMask, float range, bool allowBlood)
	{
		this.layerMask = layerMask;
		this.colliderToHit = ((!(component is Collider)) ? null : ((Collider)component));
		this.thereIsACollider = base.collider;
		this.maxRange = range;
		this.allowBlood = allowBlood;
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x000AC304 File Offset: 0x000AA504
	private void Start()
	{
		this.lastUpdateTime = Time.time;
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x000AC314 File Offset: 0x000AA514
	private void Update()
	{
		float num = Time.time - this.lastUpdateTime;
		this.lastUpdateTime = Time.time;
		if (this.distance > this.fadeDistStart)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.startScale, Mathf.Clamp((this.distance - this.fadeDistStart) / this.fadeDistLength, 0f, 1f));
		}
		RaycastHit raycastHit = default(RaycastHit);
		RaycastHit2 invalid = RaycastHit2.invalid;
		Ray ray;
		ray..ctor(base.transform.position, base.transform.forward);
		float num2 = this.speedPerSec * num;
		bool flag = !this.thereIsACollider || !this.colliderToHit || !this.colliderToHit.enabled;
		if ((!flag) ? this.colliderToHit.Raycast(ray, ref raycastHit, num2) : Physics2.Raycast2(ray, ref invalid, this.speedPerSec * num, this.layerMask))
		{
			float num3 = Vector3.Distance(Camera.main.transform.position, base.transform.position);
			if (num3 > 75f)
			{
				Object.Destroy(base.gameObject);
				return;
			}
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
			int layer = gameObject.layer;
			GameObject gameObject2 = this.impactPrefab;
			bool flag2 = true;
			if (rigidbody && !rigidbody.isKinematic && !rigidbody.CompareTag("Door"))
			{
				rigidbody.AddForceAtPosition(Vector3.up * 200f, point);
				rigidbody.AddForceAtPosition(ray.direction * 1000f, point);
			}
			global::SurfaceInfo.DoImpact(gameObject, global::SurfaceInfoObject.ImpactType.Bullet, point + normal * 0.01f, quaternion);
			if (layer == 17 || layer == 18 || layer == 27 || layer == 21)
			{
				flag2 = false;
			}
			Object.Destroy(base.gameObject);
			if (flag2)
			{
				this.impactSounds[Random.Range(0, this.impactSounds.Length)].Play(point, 1f, 2f, 15f, 180);
				GameObject gameObject3 = Object.Instantiate(this.decalPrefab, point + normal * Random.Range(0.01f, 0.03f), quaternion * Quaternion.Euler(0f, 0f, (float)Random.Range(-30, 30))) as GameObject;
				if (gameObject)
				{
					gameObject3.transform.parent = gameObject.transform;
				}
				Object.Destroy(gameObject3, 15f);
			}
		}
		else
		{
			base.transform.position += base.transform.forward * this.speedPerSec * num;
			this.distance += this.speedPerSec * num;
		}
		if (this.distance > this.maxRange)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040017A0 RID: 6048
	public float speedPerSec;

	// Token: 0x040017A1 RID: 6049
	public float lastUpdateTime;

	// Token: 0x040017A2 RID: 6050
	public GameObject impactPrefab;

	// Token: 0x040017A3 RID: 6051
	public GameObject fleshImpactPrefab;

	// Token: 0x040017A4 RID: 6052
	public GameObject decalPrefab;

	// Token: 0x040017A5 RID: 6053
	public GameObject bloodDecalPrefab;

	// Token: 0x040017A6 RID: 6054
	public GameObject myMesh;

	// Token: 0x040017A7 RID: 6055
	public Vector3 startScale;

	// Token: 0x040017A8 RID: 6056
	public float distance;

	// Token: 0x040017A9 RID: 6057
	public float startTime;

	// Token: 0x040017AA RID: 6058
	public float fadeDistStart = 0.15f;

	// Token: 0x040017AB RID: 6059
	public float fadeDistLength = 0.25f;

	// Token: 0x040017AC RID: 6060
	public AudioClip[] impactSounds;

	// Token: 0x040017AD RID: 6061
	public AudioClip[] bodyImpactSounds;

	// Token: 0x040017AE RID: 6062
	private Collider colliderToHit;

	// Token: 0x040017AF RID: 6063
	private bool thereIsACollider;

	// Token: 0x040017B0 RID: 6064
	private bool thereIsABodyPart;

	// Token: 0x040017B1 RID: 6065
	private bool allowBlood;

	// Token: 0x040017B2 RID: 6066
	private int layerMask = 406721553;

	// Token: 0x040017B3 RID: 6067
	private float maxRange = 800f;
}
