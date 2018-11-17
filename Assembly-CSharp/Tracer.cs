using System;
using UnityEngine;

// Token: 0x020004A9 RID: 1193
public class Tracer : MonoBehaviour
{
	// Token: 0x06002A00 RID: 10752 RVA: 0x000A4470 File Offset: 0x000A2670
	private void Awake()
	{
		this.startTime = Time.time;
		float num = Random.Range(0.75f, 1f);
		this.startScale = new Vector3(base.transform.localScale.x * num, base.transform.localScale.y * num, base.transform.localScale.z * Random.Range(0.5f, 1f));
		base.transform.localScale = new Vector3(0f, 0f, this.startScale.z);
	}

	// Token: 0x06002A01 RID: 10753 RVA: 0x000A4518 File Offset: 0x000A2718
	public void Init(Component component, int layerMask, float range, bool allowBlood)
	{
		this.layerMask = layerMask;
		this.colliderToHit = ((!(component is Collider)) ? null : ((Collider)component));
		this.thereIsACollider = base.collider;
		this.maxRange = range;
		this.allowBlood = allowBlood;
	}

	// Token: 0x06002A02 RID: 10754 RVA: 0x000A456C File Offset: 0x000A276C
	private void Start()
	{
		this.lastUpdateTime = Time.time;
	}

	// Token: 0x06002A03 RID: 10755 RVA: 0x000A457C File Offset: 0x000A277C
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
			SurfaceInfo.DoImpact(gameObject, SurfaceInfoObject.ImpactType.Bullet, point + normal * 0.01f, quaternion);
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

	// Token: 0x040015E3 RID: 5603
	public float speedPerSec;

	// Token: 0x040015E4 RID: 5604
	public float lastUpdateTime;

	// Token: 0x040015E5 RID: 5605
	public GameObject impactPrefab;

	// Token: 0x040015E6 RID: 5606
	public GameObject fleshImpactPrefab;

	// Token: 0x040015E7 RID: 5607
	public GameObject decalPrefab;

	// Token: 0x040015E8 RID: 5608
	public GameObject bloodDecalPrefab;

	// Token: 0x040015E9 RID: 5609
	public GameObject myMesh;

	// Token: 0x040015EA RID: 5610
	public Vector3 startScale;

	// Token: 0x040015EB RID: 5611
	public float distance;

	// Token: 0x040015EC RID: 5612
	public float startTime;

	// Token: 0x040015ED RID: 5613
	public float fadeDistStart = 0.15f;

	// Token: 0x040015EE RID: 5614
	public float fadeDistLength = 0.25f;

	// Token: 0x040015EF RID: 5615
	public AudioClip[] impactSounds;

	// Token: 0x040015F0 RID: 5616
	public AudioClip[] bodyImpactSounds;

	// Token: 0x040015F1 RID: 5617
	private Collider colliderToHit;

	// Token: 0x040015F2 RID: 5618
	private bool thereIsACollider;

	// Token: 0x040015F3 RID: 5619
	private bool thereIsABodyPart;

	// Token: 0x040015F4 RID: 5620
	private bool allowBlood;

	// Token: 0x040015F5 RID: 5621
	private int layerMask = 406721553;

	// Token: 0x040015F6 RID: 5622
	private float maxRange = 800f;
}
