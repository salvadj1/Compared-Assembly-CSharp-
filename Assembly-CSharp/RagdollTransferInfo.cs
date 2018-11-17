using System;
using System.Text;
using UnityEngine;

// Token: 0x0200009D RID: 157
public struct RagdollTransferInfo
{
	// Token: 0x06000350 RID: 848 RVA: 0x000108E4 File Offset: 0x0000EAE4
	public RagdollTransferInfo(string headBoneName)
	{
		this.headBoneName = headBoneName;
		this.headBone = null;
		this.providedHeadBone = false;
		this.providedHeadBoneName = (headBoneName != null);
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00010914 File Offset: 0x0000EB14
	public RagdollTransferInfo(Transform transform)
	{
		this.providedHeadBone = transform;
		this.providedHeadBoneName = false;
		this.headBoneName = null;
		this.headBone = transform;
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00010938 File Offset: 0x0000EB38
	private static void FindNameRecurse(Transform child, StringBuilder sb)
	{
		Transform parent = child.parent;
		if (parent)
		{
			RagdollTransferInfo.FindNameRecurse(parent, sb);
			if (sb.Length > 0)
			{
				sb.Append('/');
			}
			else
			{
				sb.Append(child.name);
			}
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00010988 File Offset: 0x0000EB88
	public bool FindHead(Transform root, out Transform headBone)
	{
		if (this.providedHeadBoneName)
		{
			Transform transform;
			headBone = (transform = root.Find(this.headBoneName));
			return transform;
		}
		if (this.providedHeadBone && this.headBone)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RagdollTransferInfo.FindNameRecurse(this.headBone, stringBuilder);
			Transform transform;
			headBone = (transform = root.Find(stringBuilder.ToString()));
			return transform;
		}
		headBone = root;
		return false;
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00010A00 File Offset: 0x0000EC00
	public static implicit operator RagdollTransferInfo(string headBoneName)
	{
		return new RagdollTransferInfo(headBoneName);
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00010A08 File Offset: 0x0000EC08
	public static implicit operator RagdollTransferInfo(Transform transform)
	{
		return new RagdollTransferInfo(transform);
	}

	// Token: 0x040002C0 RID: 704
	public readonly string headBoneName;

	// Token: 0x040002C1 RID: 705
	public readonly Transform headBone;

	// Token: 0x040002C2 RID: 706
	public readonly bool providedHeadBone;

	// Token: 0x040002C3 RID: 707
	public readonly bool providedHeadBoneName;
}
