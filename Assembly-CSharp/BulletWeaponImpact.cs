using System;
using UnityEngine;

// Token: 0x02000622 RID: 1570
public class BulletWeaponImpact : global::WeaponImpact
{
	// Token: 0x06003368 RID: 13160 RVA: 0x000C1680 File Offset: 0x000BF880
	public BulletWeaponImpact(global::BulletWeaponDataBlock dataBlock, global::IBulletWeaponItem item, global::ItemRepresentation itemRep, Transform hitTransform, Vector3 localHitPoint, Vector3 localHitDirection) : base(dataBlock, item, itemRep)
	{
		this.hitTransform = hitTransform;
		this.hitPoint = localHitPoint;
		this.hitDirection = localHitDirection;
	}

	// Token: 0x06003369 RID: 13161 RVA: 0x000C16A4 File Offset: 0x000BF8A4
	public BulletWeaponImpact(global::BulletWeaponDataBlock dataBlock, global::IBulletWeaponItem item, global::ItemRepresentation itemRep, Vector3 worldHitPoint, Vector3 worldHitDirection) : this(dataBlock, item, itemRep, null, worldHitPoint, worldHitDirection)
	{
	}

	// Token: 0x17000A98 RID: 2712
	// (get) Token: 0x0600336A RID: 13162 RVA: 0x000C16B4 File Offset: 0x000BF8B4
	public new global::BulletWeaponDataBlock dataBlock
	{
		get
		{
			return (global::BulletWeaponDataBlock)this.dataBlock;
		}
	}

	// Token: 0x17000A99 RID: 2713
	// (get) Token: 0x0600336B RID: 13163 RVA: 0x000C16C4 File Offset: 0x000BF8C4
	public new global::IBulletWeaponItem item
	{
		get
		{
			return this.item as global::IBulletWeaponItem;
		}
	}

	// Token: 0x17000A9A RID: 2714
	// (get) Token: 0x0600336C RID: 13164 RVA: 0x000C16D4 File Offset: 0x000BF8D4
	public Vector3 localPoint
	{
		get
		{
			return (!this.hitTransform) ? default(Vector3) : this.hitPoint;
		}
	}

	// Token: 0x17000A9B RID: 2715
	// (get) Token: 0x0600336D RID: 13165 RVA: 0x000C1708 File Offset: 0x000BF908
	public Vector3 worldPoint
	{
		get
		{
			return (!this.hitTransform) ? this.hitPoint : this.hitTransform.TransformPoint(this.hitPoint);
		}
	}

	// Token: 0x17000A9C RID: 2716
	// (get) Token: 0x0600336E RID: 13166 RVA: 0x000C1744 File Offset: 0x000BF944
	public Vector3 localDirection
	{
		get
		{
			return (!this.hitTransform) ? Vector3.forward : this.hitDirection;
		}
	}

	// Token: 0x17000A9D RID: 2717
	// (get) Token: 0x0600336F RID: 13167 RVA: 0x000C1774 File Offset: 0x000BF974
	public Vector3 worldDirection
	{
		get
		{
			return (!this.hitTransform) ? this.hitDirection : this.hitTransform.TransformDirection(this.hitDirection);
		}
	}

	// Token: 0x04001B22 RID: 6946
	public readonly Transform hitTransform;

	// Token: 0x04001B23 RID: 6947
	private Vector3 hitPoint;

	// Token: 0x04001B24 RID: 6948
	private Vector3 hitDirection;
}
