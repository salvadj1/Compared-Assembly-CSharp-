using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x0200005A RID: 90
public sealed class TreeUnpack : ThrottledTask, IProgress
{
	// Token: 0x060002EC RID: 748 RVA: 0x0000F484 File Offset: 0x0000D684
	private new void Awake()
	{
		base.Awake();
		base.StartCoroutine("DoUnpack");
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F498 File Offset: 0x0000D698
	public float progress
	{
		get
		{
			return (this.totalTrees <= 0) ? 1f : ((float)this.currentTreeIndex / (float)this.totalTrees);
		}
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
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
		ThrottledTask.Timer timer = base.Begin;
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

	// Token: 0x060002EF RID: 751 RVA: 0x0000F4DC File Offset: 0x0000D6DC
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

	// Token: 0x040001CA RID: 458
	[SerializeField]
	private TreeUnpackGroup[] unpackGroups;

	// Token: 0x040001CB RID: 459
	private IEnumerator<Mesh> meshEnumerator;

	// Token: 0x040001CC RID: 460
	private IEnumerator<TreeUnpackGroup> groupEnumerator;

	// Token: 0x040001CD RID: 461
	private TreeUnpackGroup currentGroup;

	// Token: 0x040001CE RID: 462
	private Mesh currentMesh;

	// Token: 0x040001CF RID: 463
	[NonSerialized]
	private int totalTrees;

	// Token: 0x040001D0 RID: 464
	[NonSerialized]
	private int currentTreeIndex;
}
