using System;
using UnityEngine;

// Token: 0x02000564 RID: 1380
public class BulletWeaponImpact : WeaponImpact
{
	// Token: 0x06002FA0 RID: 12192 RVA: 0x000B9424 File Offset: 0x000B7624
	public BulletWeaponImpact(BulletWeaponDataBlock dataBlock, IBulletWeaponItem item, ItemRepresentation itemRep, Transform hitTransform, Vector3 localHitPoint, Vector3 localHitDirection) : base(dataBlock, item, itemRep)
	{
		this.hitTransform = hitTransform;
		this.hitPoint = localHitPoint;
		this.hitDirection = localHitDirection;
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x000B9448 File Offset: 0x000B7648
	public BulletWeaponImpact(BulletWeaponDataBlock dataBlock, IBulletWeaponItem item, ItemRepresentation itemRep, Vector3 worldHitPoint, Vector3 worldHitDirection) : this(dataBlock, item, itemRep, null, worldHitPoint, worldHitDirection)
	{
	}

	// Token: 0x17000A22 RID: 2594
	// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000B9458 File Offset: 0x000B7658
	public new BulletWeaponDataBlock dataBlock
	{
		get
		{
			return (BulletWeaponDataBlock)this.dataBlock;
		}
	}

	// Token: 0x17000A23 RID: 2595
	// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000B9468 File Offset: 0x000B7668
	public new IBulletWeaponItem item
	{
		get
		{
			return this.item as IBulletWeaponItem;
		}
	}

	// Token: 0x17000A24 RID: 2596
	// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000B9478 File Offset: 0x000B7678
	public Vector3 localPoint
	{
		get
		{
			return (!this.hitTransform) ? default(Vector3) : this.hitPoint;
		}
	}

	// Token: 0x17000A25 RID: 2597
	// (get) Token: 0x06002FA5 RID: 12197 RVA: 0x000B94AC File Offset: 0x000B76AC
	public Vector3 worldPoint
	{
		get
		{
			return (!this.hitTransform) ? this.hitPoint : this.hitTransform.TransformPoint(this.hitPoint);
		}
	}

	// Token: 0x17000A26 RID: 2598
	// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000B94E8 File Offset: 0x000B76E8
	public Vector3 localDirection
	{
		get
		{
			return (!this.hitTransform) ? Vector3.forward : this.hitDirection;
		}
	}

	// Token: 0x17000A27 RID: 2599
	// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000B9518 File Offset: 0x000B7718
	public Vector3 worldDirection
	{
		get
		{
			return (!this.hitTransform) ? this.hitDirection : this.hitTransform.TransformDirection(this.hitDirection);
		}
	}

	// Token: 0x04001951 RID: 6481
	public readonly Transform hitTransform;

	// Token: 0x04001952 RID: 6482
	private Vector3 hitPoint;

	// Token: 0x04001953 RID: 6483
	private Vector3 hitDirection;
}
