using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000737 RID: 1847
[global::NGCAutoAddScript]
[RequireComponent(typeof(global::TakeDamage))]
public class StructureComponent : IDMain, global::IServerSaveable
{
	// Token: 0x06003D58 RID: 15704 RVA: 0x000DB970 File Offset: 0x000D9B70
	public StructureComponent() : base(0)
	{
	}

	// Token: 0x06003D5A RID: 15706 RVA: 0x000DB994 File Offset: 0x000D9B94
	public global::StructureMaster.StructureMaterialType GetMaterialType()
	{
		return this._materialType;
	}

	// Token: 0x06003D5B RID: 15707 RVA: 0x000DB99C File Offset: 0x000D9B9C
	public void Touched()
	{
		this._master.Touched();
	}

	// Token: 0x06003D5C RID: 15708 RVA: 0x000DB9AC File Offset: 0x000D9BAC
	public void OnHurt(global::DamageEvent damage)
	{
	}

	// Token: 0x06003D5D RID: 15709 RVA: 0x000DB9B0 File Offset: 0x000D9BB0
	public void OnRepair()
	{
	}

	// Token: 0x06003D5E RID: 15710 RVA: 0x000DB9B4 File Offset: 0x000D9BB4
	public bool IsType(global::StructureComponent.StructureComponentType checkType)
	{
		return this.type == checkType;
	}

	// Token: 0x06003D5F RID: 15711 RVA: 0x000DB9C0 File Offset: 0x000D9BC0
	public bool IsPillar()
	{
		return this.type == global::StructureComponent.StructureComponentType.Pillar;
	}

	// Token: 0x06003D60 RID: 15712 RVA: 0x000DB9CC File Offset: 0x000D9BCC
	public bool IsWallType()
	{
		return this.type == global::StructureComponent.StructureComponentType.Wall || this.type == global::StructureComponent.StructureComponentType.Doorway || this.type == global::StructureComponent.StructureComponentType.WindowWall;
	}

	// Token: 0x06003D61 RID: 15713 RVA: 0x000DBA00 File Offset: 0x000D9C00
	[RPC]
	public void ClientHealthUpdate(float newHealth)
	{
		this.healthDimmer.UpdateHealthAmount(this, newHealth, false);
	}

	// Token: 0x06003D62 RID: 15714 RVA: 0x000DBA10 File Offset: 0x000D9C10
	[RPC]
	public void ClientKilled()
	{
		if (this.deathEffect)
		{
			GameObject gameObject = Object.Instantiate(this.deathEffect, base.transform.position, base.transform.rotation) as GameObject;
			Object.Destroy(gameObject, 5f);
		}
	}

	// Token: 0x06003D63 RID: 15715 RVA: 0x000DBA60 File Offset: 0x000D9C60
	private void cl_predestroy(global::NGCView view)
	{
		if (this._master)
		{
			this._master.CullComponent(this);
		}
	}

	// Token: 0x06003D64 RID: 15716 RVA: 0x000DBA80 File Offset: 0x000D9C80
	protected internal virtual void OnOwnedByMasterStructure(global::StructureMaster master)
	{
		this._master = master;
		global::NGCView component = base.GetComponent<global::NGCView>();
		if (component && !this.addedDestroyCallback && component)
		{
			this.addedDestroyCallback = true;
			component.OnPreDestroy += this.cl_predestroy;
		}
	}

	// Token: 0x06003D65 RID: 15717 RVA: 0x000DBAD8 File Offset: 0x000D9CD8
	[Obsolete("Do not call manually", true)]
	[RPC]
	protected void SMSet(uLink.NetworkViewID masterViewID)
	{
		Facepunch.NetworkView networkView = Facepunch.NetworkView.Find(masterViewID);
		if (networkView)
		{
			global::StructureMaster component = networkView.GetComponent<global::StructureMaster>();
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

	// Token: 0x06003D66 RID: 15718 RVA: 0x000DBB30 File Offset: 0x000D9D30
	public virtual bool CheckLocation(global::StructureMaster master, Vector3 placePos, Quaternion placeRot)
	{
		bool flag = false;
		bool flag2 = false;
		Vector3 vector = master.transform.InverseTransformPoint(placePos);
		if (master.GetMaterialType() != global::StructureMaster.StructureMaterialType.UNSET && master.GetMaterialType() != this.GetMaterialType())
		{
			if (global::StructureComponent.logFailures)
			{
				Debug.Log("Not proper material type, master is :" + master.GetMaterialType());
			}
			return false;
		}
		global::StructureComponent componentFromPositionWorld = master.GetComponentFromPositionWorld(placePos);
		if (componentFromPositionWorld != null)
		{
			if (global::StructureComponent.logFailures)
			{
				Debug.Log("Occupied space", componentFromPositionWorld);
			}
			flag = true;
		}
		global::StructureComponent structureComponent = master.CompByLocal(vector - new Vector3(0f, global::StructureMaster.gridSpacingY, 0f));
		if (this.type != global::StructureComponent.StructureComponentType.Foundation)
		{
			bool foundationForPoint = master.GetFoundationForPoint(placePos);
			if (foundationForPoint)
			{
				flag2 = true;
			}
		}
		if (this.type == global::StructureComponent.StructureComponentType.Wall || this.type == global::StructureComponent.StructureComponentType.Doorway || this.type == global::StructureComponent.StructureComponentType.WindowWall)
		{
			if (flag)
			{
				return false;
			}
			Vector3 vector2 = placePos + placeRot * -Vector3.right * 2.5f;
			global::StructureComponent componentFromPositionWorld2 = master.GetComponentFromPositionWorld(vector2);
			Vector3 vector3 = placePos + placeRot * Vector3.right * 2.5f;
			global::StructureComponent componentFromPositionWorld3 = master.GetComponentFromPositionWorld(vector3);
			if (global::StructureComponent.logFailures)
			{
				Debug.DrawLine(vector2, vector3, Color.cyan);
			}
			if (componentFromPositionWorld2 && componentFromPositionWorld3)
			{
				bool flag3;
				if (componentFromPositionWorld2.type != global::StructureComponent.StructureComponentType.Pillar)
				{
					if (global::StructureComponent.logFailures)
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
				if (componentFromPositionWorld3.type != global::StructureComponent.StructureComponentType.Pillar)
				{
					if (global::StructureComponent.logFailures)
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
			if (global::StructureComponent.logFailures)
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
			if (this.type == global::StructureComponent.StructureComponentType.Foundation)
			{
				foreach (global::StructureMaster structureMaster in global::StructureMaster.AllStructuresWithBounds)
				{
					if (!(structureMaster == master))
					{
						if (structureMaster.containedBounds.Intersects(new Bounds(placePos, new Vector3(5f, 5f, 4f))))
						{
							if (global::StructureComponent.logFailures)
							{
								Debug.Log("Too close to something");
							}
							return false;
						}
					}
				}
				bool flag5 = master.IsValidFoundationSpot(placePos);
				if (global::StructureComponent.logFailures)
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
			if (this.type == global::StructureComponent.StructureComponentType.Ramp)
			{
				return componentFromPositionWorld == null && (master.IsValidFoundationSpot(placePos) || (structureComponent && (structureComponent.type == global::StructureComponent.StructureComponentType.Ceiling || structureComponent.type == global::StructureComponent.StructureComponentType.Foundation)));
			}
			if (this.type == global::StructureComponent.StructureComponentType.Pillar)
			{
				return ((structureComponent && structureComponent.type == global::StructureComponent.StructureComponentType.Pillar) || flag2) && !flag;
			}
			if (this.type != global::StructureComponent.StructureComponentType.Stairs && this.type != global::StructureComponent.StructureComponentType.Ceiling)
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
				global::StructureComponent structureComponent2 = master.CompByLocal(vector + vector4);
				if (structureComponent2 == null || structureComponent2.type != global::StructureComponent.StructureComponentType.Pillar)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x06003D67 RID: 15719 RVA: 0x000DBFE0 File Offset: 0x000DA1E0
	protected void OnPoolRetire()
	{
		this.oldHealth = 0f;
		this.addedDestroyCallback = false;
		this.healthDimmer.Reset();
	}

	// Token: 0x04001F5B RID: 8027
	public GameObject deathEffect;

	// Token: 0x04001F5C RID: 8028
	public global::StructureMaster _master;

	// Token: 0x04001F5D RID: 8029
	protected float oldHealth;

	// Token: 0x04001F5E RID: 8030
	[NonSerialized]
	private bool addedDestroyCallback;

	// Token: 0x04001F5F RID: 8031
	public float Width = 5f;

	// Token: 0x04001F60 RID: 8032
	public float Height = 1f;

	// Token: 0x04001F61 RID: 8033
	public global::StructureMaster.StructureMaterialType _materialType;

	// Token: 0x04001F62 RID: 8034
	public global::StructureComponent.StructureComponentType type;

	// Token: 0x04001F63 RID: 8035
	[NonSerialized]
	private global::HealthDimmer healthDimmer;

	// Token: 0x04001F64 RID: 8036
	private static bool logFailures;

	// Token: 0x02000738 RID: 1848
	[Serializable]
	public enum StructureComponentType
	{
		// Token: 0x04001F66 RID: 8038
		Pillar,
		// Token: 0x04001F67 RID: 8039
		Wall,
		// Token: 0x04001F68 RID: 8040
		Doorway,
		// Token: 0x04001F69 RID: 8041
		Ceiling,
		// Token: 0x04001F6A RID: 8042
		Stairs,
		// Token: 0x04001F6B RID: 8043
		Foundation,
		// Token: 0x04001F6C RID: 8044
		WindowWall,
		// Token: 0x04001F6D RID: 8045
		Ramp,
		// Token: 0x04001F6E RID: 8046
		Last
	}
}
