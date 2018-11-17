using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200084D RID: 2125
public class TreeExplosion : MonoBehaviour
{
	// Token: 0x06004AE4 RID: 19172 RVA: 0x001472DC File Offset: 0x001454DC
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

	// Token: 0x06004AE5 RID: 19173 RVA: 0x00147438 File Offset: 0x00145638
	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			this.Explode();
		}
	}

	// Token: 0x04002BFC RID: 11260
	public float BlastRange = 30f;

	// Token: 0x04002BFD RID: 11261
	public float BlastForce = 30000f;

	// Token: 0x04002BFE RID: 11262
	public GameObject DeadReplace;

	// Token: 0x04002BFF RID: 11263
	public GameObject Explosion;
}
