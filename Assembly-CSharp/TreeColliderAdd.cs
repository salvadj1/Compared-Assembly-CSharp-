using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class TreeColliderAdd : MonoBehaviour
{
	// Token: 0x06000356 RID: 854 RVA: 0x0001065C File Offset: 0x0000E85C
	private void Start()
	{
		this.terrainData = this.terrain.terrainData;
		this.lastPos = base.transform.position;
		this.treeColliderPool = new List<GameObject>();
		this.usedCollidersPool = new List<GameObject>();
		this.convertedTreePositions = new Vector3[this.terrainData.treeInstances.Length];
		int num = 0;
		foreach (TreeInstance treeInstance in this.terrainData.treeInstances)
		{
			this.convertedTreePositions[num] = Vector3.Scale(treeInstance.position, this.terrainData.size) + this.terrain.transform.position;
			num++;
		}
		Debug.Log("Tree instances length:" + this.terrainData.treeInstances.Length);
		for (int j = 0; j < this.pooledColliders; j++)
		{
			GameObject item = Object.Instantiate(this.treeColliderPrefab, new Vector3(0f, -20000f, 0f), Quaternion.identity) as GameObject;
			this.treeColliderPool.Add(item);
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x000107A0 File Offset: 0x0000E9A0
	public GameObject GetFreeTreeCollider()
	{
		if (this.treeColliderPool.Count > 0)
		{
			GameObject result = this.treeColliderPool[0];
			this.treeColliderPool.RemoveAt(0);
			return result;
		}
		return null;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x000107DC File Offset: 0x0000E9DC
	private void Update()
	{
		Vector3 position = base.transform.position;
		if (Vector3.Distance(position, this.lastPos) >= 100f)
		{
			this.AddNewColliders();
			this.lastPos = position;
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00010818 File Offset: 0x0000EA18
	private void CleanupOldColliders()
	{
		foreach (GameObject item in this.usedCollidersPool)
		{
			this.treeColliderPool.Add(item);
		}
		this.usedCollidersPool.Clear();
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00010890 File Offset: 0x0000EA90
	private void AddNewColliders()
	{
		this.CleanupOldColliders();
		Vector3 position = base.transform.position;
		int num = 0;
		int num2 = 0;
		int num3 = this.treeColliderPool.Count;
		int i = 0;
		int num4 = this.convertedTreePositions.Length;
		while (i < num4)
		{
			Vector3 vector;
			vector.x = this.convertedTreePositions[i].x - position.x;
			vector.y = this.convertedTreePositions[i].y - position.y;
			vector.z = this.convertedTreePositions[i].z - position.z;
			float num5 = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
			if (num5 <= 40000f)
			{
				GameObject freeTreeCollider = this.GetFreeTreeCollider();
				if (!freeTreeCollider)
				{
					return;
				}
				Vector3 vector2 = this.convertedTreePositions[i];
				freeTreeCollider.transform.position = vector2;
				this.usedCollidersPool.Add(freeTreeCollider);
				this.convertedTreePositions[i] = this.convertedTreePositions[num2];
				this.convertedTreePositions[num2++] = vector2;
				if (--num3 == 0)
				{
					break;
				}
			}
			num++;
			i++;
		}
	}

	// Token: 0x0400021E RID: 542
	public Terrain terrain;

	// Token: 0x0400021F RID: 543
	private TerrainData terrainData;

	// Token: 0x04000220 RID: 544
	public Vector3 lastPos;

	// Token: 0x04000221 RID: 545
	public GameObject treeColliderPrefab;

	// Token: 0x04000222 RID: 546
	private int pooledColliders = 500;

	// Token: 0x04000223 RID: 547
	private List<GameObject> treeColliderPool;

	// Token: 0x04000224 RID: 548
	private List<GameObject> usedCollidersPool;

	// Token: 0x04000225 RID: 549
	private Vector3[] convertedTreePositions;
}
