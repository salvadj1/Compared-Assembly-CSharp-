using System;
using UnityEngine;

// Token: 0x02000940 RID: 2368
public class CreateCraterOnImpact : MonoBehaviour
{
	// Token: 0x06004F9E RID: 20382 RVA: 0x001512E4 File Offset: 0x0014F4E4
	private void OnCollisionEnter(Collision collision)
	{
		if (this.Explosion)
		{
			Object.Instantiate(this.Explosion, collision.contacts[0].point, Quaternion.identity);
		}
		global::CraterMaker component = collision.gameObject.GetComponent<global::CraterMaker>();
		if (component)
		{
			component.Create(collision.contacts[0].point, this.Radius, this.Depth, this.Noise);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04002E62 RID: 11874
	public float Radius = 15f;

	// Token: 0x04002E63 RID: 11875
	public float Depth = 10f;

	// Token: 0x04002E64 RID: 11876
	public float Noise = 0.5f;

	// Token: 0x04002E65 RID: 11877
	public GameObject Explosion;
}
