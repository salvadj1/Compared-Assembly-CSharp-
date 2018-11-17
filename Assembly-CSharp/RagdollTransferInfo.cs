using System;
using System.Text;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public struct RagdollTransferInfo
{
	// Token: 0x060003C8 RID: 968 RVA: 0x000120D4 File Offset: 0x000102D4
	public RagdollTransferInfo(string headBoneName)
	{
		this.headBoneName = headBoneName;
		this.headBone = null;
		this.providedHeadBone = false;
		this.providedHeadBoneName = (headBoneName != null);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00012104 File Offset: 0x00010304
	public RagdollTransferInfo(Transform transform)
	{
		this.providedHeadBone = transform;
		this.providedHeadBoneName = false;
		this.headBoneName = null;
		this.headBone = transform;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00012128 File Offset: 0x00010328
	private static void FindNameRecurse(Transform child, StringBuilder sb)
	{
		Transform parent = child.parent;
		if (parent)
		{
			global::RagdollTransferInfo.FindNameRecurse(parent, sb);
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

	// Token: 0x060003CB RID: 971 RVA: 0x00012178 File Offset: 0x00010378
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
			global::RagdollTransferInfo.FindNameRecurse(this.headBone, stringBuilder);
			Transform transform;
			headBone = (transform = root.Find(stringBuilder.ToString()));
			return transform;
		}
		headBone = root;
		return false;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x000121F0 File Offset: 0x000103F0
	public static implicit operator global::RagdollTransferInfo(string headBoneName)
	{
		return new global::RagdollTransferInfo(headBoneName);
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000121F8 File Offset: 0x000103F8
	public static implicit operator global::RagdollTransferInfo(Transform transform)
	{
		return new global::RagdollTransferInfo(transform);
	}

	// Token: 0x0400032B RID: 811
	public readonly string headBoneName;

	// Token: 0x0400032C RID: 812
	public readonly Transform headBone;

	// Token: 0x0400032D RID: 813
	public readonly bool providedHeadBone;

	// Token: 0x0400032E RID: 814
	public readonly bool providedHeadBoneName;
}
