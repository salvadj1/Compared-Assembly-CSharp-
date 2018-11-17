using System;
using UnityEngine;

// Token: 0x0200084A RID: 2122
public class CreateCraterOnImpact : MonoBehaviour
{
	// Token: 0x06004ADD RID: 19165 RVA: 0x00146D20 File Offset: 0x00144F20
	private void OnCollisionEnter(Collision collision)
	{
		if (this.Explosion)
		{
			Object.Instantiate(this.Explosion, collision.contacts[0].point, Quaternion.identity);
		}
		CraterMaker component = collision.gameObject.GetComponent<CraterMaker>();
		if (component)
		{
			component.Create(collision.contacts[0].point, this.Radius, this.Depth, this.Noise);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04002BF4 RID: 11252
	public float Radius = 15f;

	// Token: 0x04002BF5 RID: 11253
	public float Depth = 10f;

	// Token: 0x04002BF6 RID: 11254
	public float Noise = 0.5f;

	// Token: 0x04002BF7 RID: 11255
	public GameObject Explosion;
}
