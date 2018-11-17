using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000673 RID: 1651
[RequireComponent(typeof(TakeDamage))]
[NGCAutoAddScript]
public class StructureComponent : IDMain, IServerSaveable
{
	// Token: 0x06003964 RID: 14692 RVA: 0x000D2F90 File Offset: 0x000D1190
	public StructureComponent() : base(0)
	{
	}

	// Token: 0x06003966 RID: 14694 RVA: 0x000D2FB4 File Offset: 0x000D11B4
	public StructureMaster.StructureMaterialType GetMaterialType()
	{
		return this._materialType;
	}

	// Token: 0x06003967 RID: 14695 RVA: 0x000D2FBC File Offset: 0x000D11BC
	public void Touched()
	{
		this._master.Touched();
	}

	// Token: 0x06003968 RID: 14696 RVA: 0x000D2FCC File Offset: 0x000D11CC
	public void OnHurt(DamageEvent damage)
	{
	}

	// Token: 0x06003969 RID: 14697 RVA: 0x000D2FD0 File Offset: 0x000D11D0
	public void OnRepair()
	{
	}

	// Token: 0x0600396A RID: 14698 RVA: 0x000D2FD4 File Offset: 0x000D11D4
	public bool IsType(StructureComponent.StructureComponentType checkType)
	{
		return this.type == checkType;
	}

	// Token: 0x0600396B RID: 14699 RVA: 0x000D2FE0 File Offset: 0x000D11E0
	public bool IsPillar()
	{
		return this.type == StructureComponent.StructureComponentType.Pillar;
	}

	// Token: 0x0600396C RID: 14700 RVA: 0x000D2FEC File Offset: 0x000D11EC
	public bool IsWallType()
	{
		return this.type == StructureComponent.StructureComponentType.Wall || this.type == StructureComponent.StructureComponentType.Doorway || this.type == StructureComponent.StructureComponentType.WindowWall;
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x000D3020 File Offset: 0x000D1220
	[RPC]
	public void ClientHealthUpdate(float newHealth)
	{
		this.healthDimmer.UpdateHealthAmount(this, newHealth, false);
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x000D3030 File Offset: 0x000D1230
	[RPC]
	public void ClientKilled()
	{
		if (this.deathEffect)
		{
			GameObject gameObject = Object.Instantiate(this.deathEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 5f);
		}
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x000D3080 File Offset: 0x000D1280
	private void cl_predestroy(NGCView view)
	{
		if (this._master)
		{
			this._master.CullComponent(this);
		}
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x000D30A0 File Offset: 0x000D12A0
	protected internal virtual void OnOwnedByMasterStructure(StructureMaster master)
	{
		this._master = master;
		NGCView component = base.GetComponent<NGCView>();
		if (component && !this.addedDestroyCallback && component)
		{
			this.addedDestroyCallback = true;
			component.OnPreDestroy += this.cl_predestroy;
		}
	}

	// Token: 0x06003971 RID: 14705 RVA: 0x000D30F8 File Offset: 0x000D12F8
	[RPC]
	[Obsolete("Do not call manually", true)]
	protected void SMSet(NetworkViewID masterViewID)
	{
		NetworkView networkView = NetworkView.Find(masterViewID);
		if (networkView)
		{
			StructureMaster component = networkView.GetComponent<StructureMaster>();
			if (component)
			{
				component.AppendStructureComponent(this);
			}
			else
			{
				Debug.LogWarning("No Master On GO", networkView);
			}
		}
		else
		{
			Debug.LogWarning("Couldnt find master view", this);
		}
	}

	// Token: 0x06003972 RID: 14706 RVA: 0x000D3150 File Offset: 0x000D1350
	public virtual bool CheckLocation(StructureMaster master, Vector3 placePos, Quaternion placeRot)
	{
		bool flag = false;
		bool flag2 = false;
		Vector3 vector = master.transform.InverseTransformPoint(placePos);
		if (master.GetMaterialType() != StructureMaster.StructureMaterialType.UNSET && master.GetMaterialType() != this.GetMaterialType())
		{
			if (StructureComponent.logFailures)
			{
				Debug.Log("Not proper material type, master is :" + master.GetMaterialType());
			}
			return false;
		}
		StructureComponent componentFromPositionWorld = master.GetComponentFromPositionWorld(placePos);
		if (componentFromPositionWorld != null)
		{
			if (StructureComponent.logFailures)
			{
				Debug.Log("Occupied space", componentFromPositionWorld);
			}
			flag = true;
		}
		StructureComponent structureComponent = master.CompByLocal(vector - new Vector3(0f, StructureMaster.gridSpacingY, 0f));
		if (this.type != StructureComponent.StructureComponentType.Foundation)
		{
			bool foundationForPoint = master.GetFoundationForPoint(placePos);
			if (foundationForPoint)
			{
				flag2 = true;
			}
		}
		if (this.type == StructureComponent.StructureComponentType.Wall || this.type == StructureComponent.StructureComponentType.Doorway || this.type == StructureComponent.StructureComponentType.WindowWall)
		{
			if (flag)
			{
				return false;
			}
			Vector3 vector2 = placePos + placeRot * -Vector3.right * 2.5f;
			StructureComponent componentFromPositionWorld2 = master.GetComponentFromPositionWorld(vector2);
			Vector3 vector3 = placePos + placeRot * Vector3.right * 2.5f;
			StructureComponent componentFromPositionWorld3 = master.GetComponentFromPositionWorld(vector3);
			if (StructureComponent.logFailures)
			{
				Debug.DrawLine(vector2, vector3, Color.cyan);
			}
			if (componentFromPositionWorld2 && componentFromPositionWorld3)
			{
				bool flag3;
				if (componentFromPositionWorld2.type != StructureComponent.StructureComponentType.Pillar)
				{
					if (StructureComponent.logFailures)
					{
						Debug.Log("Left was not acceptable", componentFromPositionWorld2);
					}
					flag3 = false;
				}
				else
				{
					flag3 = true;
				}
				bool flag4;
				if (componentFromPositionWorld3.type != StructureComponent.StructureComponentType.Pillar)
				{
					if (StructureComponent.logFailures)
					{
						Debug.Log("Right was not acceptable", componentFromPositionWorld3);
					}
					flag4 = false;
				}
				else
				{
					flag4 = true;
				}
				return flag3 && flag4;
			}
			if (StructureComponent.logFailures)
			{
				if (!componentFromPositionWorld2)
				{
					Debug.Log("Did not find left");
				}
				if (!componentFromPositionWorld3)
				{
					Debug.Log("Did not find right");
				}
			}
			return false;
		}
		else
		{
			if (this.type == StructureComponent.StructureComponentType.Foundation)
			{
				foreach (StructureMaster structureMaster in StructureMaster.AllStructuresWithBounds)
				{
					if (!(structureMaster == master))
					{
						if (structureMaster.containedBounds.Intersects(new Bounds(placePos, new Vector3(5f, 5f, 4f))))
						{
							if (StructureComponent.logFailures)
							{
								Debug.Log("Too close to something");
							}
							return false;
						}
					}
				}
				bool flag5 = master.IsValidFoundationSpot(placePos);
				if (StructureComponent.logFailures)
				{
					Debug.Log(string.Concat(new object[]
					{
						"returning here : mastervalid:",
						flag5,
						"compinplace",
						componentFromPositionWorld
					}));
				}
				return flag5 && !componentFromPositionWorld;
			}
			if (this.type == StructureComponent.StructureComponentType.Ramp)
			{
				return componentFromPositionWorld == null && (master.IsValidFoundationSpot(placePos) || (structureComponent && (structureComponent.type == StructureComponent.StructureComponentType.Ceiling || structureComponent.type == StructureComponent.StructureComponentType.Foundation)));
			}
			if (this.type == StructureComponent.StructureComponentType.Pillar)
			{
				return ((structureComponent && structureComponent.type == StructureComponent.StructureComponentType.Pillar) || flag2) && !flag;
			}
			if (this.type != StructureComponent.StructureComponentType.Stairs && this.type != StructureComponent.StructureComponentType.Ceiling)
			{
				return false;
			}
			if (flag)
			{
				return false;
			}
			Vector3[] array = new Vector3[]
			{
				new Vector3(-2.5f, 0f, -2.5f),
				new Vector3(2.5f, 0f, 2.5f),
				new Vector3(-2.5f, 0f, 2.5f),
				new Vector3(2.5f, 0f, -2.5f)
			};
			foreach (Vector3 vector4 in array)
			{
				StructureComponent structureComponent2 = master.CompByLocal(vector + vector4);
				if (structureComponent2 == null || structureComponent2.type != StructureComponent.StructureComponentType.Pillar)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x06003973 RID: 14707 RVA: 0x000D3600 File Offset: 0x000D1800
	protected void OnPoolRetire()
	{
		this.oldHealth = 0f;
		this.addedDestroyCallback = false;
		this.healthDimmer.Reset();
	}

	// Token: 0x04001D63 RID: 7523
	public GameObject deathEffect;

	// Token: 0x04001D64 RID: 7524
	public StructureMaster _master;

	// Token: 0x04001D65 RID: 7525
	protected float oldHealth;

	// Token: 0x04001D66 RID: 7526
	[NonSerialized]
	private bool addedDestroyCallback;

	// Token: 0x04001D67 RID: 7527
	public float Width = 5f;

	// Token: 0x04001D68 RID: 7528
	public float Height = 1f;

	// Token: 0x04001D69 RID: 7529
	public StructureMaster.StructureMaterialType _materialType;

	// Token: 0x04001D6A RID: 7530
	public StructureComponent.StructureComponentType type;

	// Token: 0x04001D6B RID: 7531
	[NonSerialized]
	private HealthDimmer healthDimmer;

	// Token: 0x04001D6C RID: 7532
	private static bool logFailures;

	// Token: 0x02000674 RID: 1652
	[Serializable]
	public enum StructureComponentType
	{
		// Token: 0x04001D6E RID: 7534
		Pillar,
		// Token: 0x04001D6F RID: 7535
		Wall,
		// Token: 0x04001D70 RID: 7536
		Doorway,
		// Token: 0x04001D71 RID: 7537
		Ceiling,
		// Token: 0x04001D72 RID: 7538
		Stairs,
		// Token: 0x04001D73 RID: 7539
		Foundation,
		// Token: 0x04001D74 RID: 7540
		WindowWall,
		// Token: 0x04001D75 RID: 7541
		Ramp,
		// Token: 0x04001D76 RID: 7542
		Last
	}
}
