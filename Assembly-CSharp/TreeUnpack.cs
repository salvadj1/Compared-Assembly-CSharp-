using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x0200006C RID: 108
public sealed class TreeUnpack : global::ThrottledTask, Facepunch.Progress.IProgress
{
	// Token: 0x0600035E RID: 862 RVA: 0x00010A2C File Offset: 0x0000EC2C
	private new void Awake()
	{
		base.Awake();
		base.StartCoroutine("DoUnpack");
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x0600035F RID: 863 RVA: 0x00010A40 File Offset: 0x0000EC40
	public float progress
	{
		get
		{
			return (this.totalTrees <= 0) ? 1f : ((float)this.currentTreeIndex / (float)this.totalTrees);
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00010A68 File Offset: 0x0000EC68
	private IEnumerator DoUnpack()
	{
		this.totalTrees = 0;
		this.currentTreeIndex = 0;
		foreach (TreeUnpackGroup grp in this.unpackGroups)
		{
			this.totalTrees += grp.meshes.Length;
		}
		base.Working = true;
		this.groupEnumerator = ((IEnumerable<TreeUnpackGroup>)this.unpackGroups).GetEnumerator();
		global::ThrottledTask.Timer timer = base.Begin;
		while (this.MoveNext())
		{
			GameObject col = new GameObject(string.Empty, new Type[]
			{
				typeof(MeshCollider)
			})
			{
				hideFlags = 9,
				tag = this.currentGroup.tag,
				layer = ((this.currentGroup.layer != 0) ? this.currentGroup.layer : 10)
			};
			MeshCollider mc = (MeshCollider)col.collider;
			mc.smoothSphereCollisions = this.currentGroup.spherical;
			mc.sharedMesh = this.currentMesh;
			if (!timer.Continue)
			{
				yield return new WaitForEndOfFrame();
				timer = base.Begin;
			}
		}
		base.Working = false;
		Object.Destroy(this);
		yield break;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x00010A84 File Offset: 0x0000EC84
	private bool MoveNext()
	{
		if (this.meshEnumerator != null)
		{
			while (this.meshEnumerator.MoveNext())
			{
				this.currentTreeIndex++;
				this.currentMesh = this.meshEnumerator.Current;
				if (this.currentMesh)
				{
					return true;
				}
			}
		}
		if (this.groupEnumerator.MoveNext())
		{
			this.currentGroup = this.groupEnumerator.Current;
			this.meshEnumerator = this.currentGroup.meshes.GetEnumerator();
			return this.MoveNext();
		}
		return false;
	}

	// Token: 0x0400022C RID: 556
	[SerializeField]
	private TreeUnpackGroup[] unpackGroups;

	// Token: 0x0400022D RID: 557
	private IEnumerator<Mesh> meshEnumerator;

	// Token: 0x0400022E RID: 558
	private IEnumerator<TreeUnpackGroup> groupEnumerator;

	// Token: 0x0400022F RID: 559
	private TreeUnpackGroup currentGroup;

	// Token: 0x04000230 RID: 560
	private Mesh currentMesh;

	// Token: 0x04000231 RID: 561
	[NonSerialized]
	private int totalTrees;

	// Token: 0x04000232 RID: 562
	[NonSerialized]
	private int currentTreeIndex;
}
