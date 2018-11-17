using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000943 RID: 2371
public class TreeExplosion : MonoBehaviour
{
	// Token: 0x06004FA5 RID: 20389 RVA: 0x001518A0 File Offset: 0x0014FAA0
	private void Explode()
	{
		Object.Instantiate(this.Explosion, base.transform.position, Quaternion.identity);
		TerrainData terrainData = Terrain.activeTerrain.terrainData;
		ArrayList arrayList = new ArrayList();
		foreach (TreeInstance treeInstance in terrainData.treeInstances)
		{
			float num = Vector3.Distance(Vector3.Scale(treeInstance.position, terrainData.size) + Terrain.activeTerrain.transform.position, base.transform.position);
			if (num < this.BlastRange)
			{
				GameObject gameObject = Object.Instantiate(this.DeadReplace, Vector3.Scale(treeInstance.position, terrainData.size) + Terrain.activeTerrain.transform.position, Quaternion.identity) as GameObject;
				gameObject.rigidbody.maxAngularVelocity = 1f;
				gameObject.rigidbody.AddExplosionForce(this.BlastForce, base.transform.position, 20f + this.BlastRange * 5f, -20f);
			}
			else
			{
				arrayList.Add(treeInstance);
			}
		}
		terrainData.treeInstances = (TreeInstance[])arrayList.ToArray(typeof(TreeInstance));
	}

	// Token: 0x06004FA6 RID: 20390 RVA: 0x001519FC File Offset: 0x0014FBFC
	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			this.Explode();
		}
	}

	// Token: 0x04002E6A RID: 11882
	public float BlastRange = 30f;

	// Token: 0x04002E6B RID: 11883
	public float BlastForce = 30000f;

	// Token: 0x04002E6C RID: 11884
	public GameObject DeadReplace;

	// Token: 0x04002E6D RID: 11885
	public GameObject Explosion;
}
