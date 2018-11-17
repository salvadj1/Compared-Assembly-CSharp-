using System;
using System.Collections.Generic;
using Facepunch.MeshBatch;
using uLink;
using UnityEngine;

// Token: 0x0200073A RID: 1850
[RequireComponent(typeof(uLink.NetworkView))]
public class StructureMaster : IDMain, global::IServerSaveable, global::IServerSaveNotify
{
	// Token: 0x06003D78 RID: 15736 RVA: 0x000DC378 File Offset: 0x000DA578
	protected StructureMaster(IDFlags idFlags) : base(idFlags)
	{
	}

	// Token: 0x06003D79 RID: 15737 RVA: 0x000DC388 File Offset: 0x000DA588
	public StructureMaster() : this(0)
	{
	}

	// Token: 0x06003D7B RID: 15739 RVA: 0x000DC3EC File Offset: 0x000DA5EC
	void global::IServerSaveNotify.PostLoad()
	{
	}

	// Token: 0x17000BAE RID: 2990
	// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000DC3F0 File Offset: 0x000DA5F0
	public static List<global::StructureMaster> AllStructuresWithBounds
	{
		get
		{
			return global::StructureMaster.g_StructuresWithBounds;
		}
	}

	// Token: 0x17000BAF RID: 2991
	// (get) Token: 0x06003D7D RID: 15741 RVA: 0x000DC3F8 File Offset: 0x000DA5F8
	public static List<global::StructureMaster> AllStructures
	{
		get
		{
			return global::StructureMaster.g_Structures;
		}
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x000DC400 File Offset: 0x000DA600
	public static global::StructureMaster[] RayTestStructures(Ray ray)
	{
		return global::StructureMaster.RayTestStructures(ray, 10f);
	}

	// Token: 0x06003D7F RID: 15743 RVA: 0x000DC410 File Offset: 0x000DA610
	public static global::StructureMaster[] RayTestStructures(Ray ray, float maxDistance)
	{
		List<global::StructureMaster> list = null;
		bool flag = false;
		List<KeyValuePair<global::StructureMaster, float>> list2 = new List<KeyValuePair<global::StructureMaster, float>>();
		foreach (global::StructureMaster structureMaster in global::StructureMaster.AllStructuresWithBounds)
		{
			if (!structureMaster)
			{
				if (!flag)
				{
					flag = true;
					list = new List<global::StructureMaster>();
				}
				list.Add(structureMaster);
			}
			else
			{
				float num = 0f;
				Bounds bounds2;
				bool bounds;
				try
				{
					bounds = structureMaster.GetBounds(out bounds2);
				}
				catch (Exception ex)
				{
					if (!flag)
					{
						flag = true;
						list = new List<global::StructureMaster>();
					}
					list.Add(structureMaster);
					Debug.LogException(ex, structureMaster);
					continue;
				}
				if (bounds && bounds2.IntersectRay(ray, ref num) && num <= maxDistance)
				{
					list2.Add(new KeyValuePair<global::StructureMaster, float>(structureMaster, num));
				}
			}
		}
		if (flag)
		{
			foreach (global::StructureMaster item in list)
			{
				global::StructureMaster.g_Structures.Remove(item);
				global::StructureMaster.g_StructuresWithBounds.Remove(item);
			}
		}
		if (list2.Count == 0)
		{
			return global::StructureMaster.Empty.array;
		}
		list2.Sort((KeyValuePair<global::StructureMaster, float> x, KeyValuePair<global::StructureMaster, float> y) => x.Value.CompareTo(y.Value));
		global::StructureMaster[] array = new global::StructureMaster[list2.Count];
		int num2 = 0;
		foreach (KeyValuePair<global::StructureMaster, float> keyValuePair in list2)
		{
			array[num2++] = keyValuePair.Key;
		}
		return array;
	}

	// Token: 0x17000BB0 RID: 2992
	// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000DC634 File Offset: 0x000DA834
	public Bounds containedBounds
	{
		get
		{
			if (this._boundsDirty)
			{
				this.RecalculateBounds();
			}
			return this._containedBounds;
		}
	}

	// Token: 0x06003D81 RID: 15745 RVA: 0x000DC650 File Offset: 0x000DA850
	public void MarkBoundsDirty()
	{
		this._boundsDirty = true;
	}

	// Token: 0x06003D82 RID: 15746 RVA: 0x000DC65C File Offset: 0x000DA85C
	public void SetMaterialType(global::StructureMaster.StructureMaterialType type)
	{
		if (this._materialType == global::StructureMaster.StructureMaterialType.UNSET)
		{
			this._materialType = type;
		}
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x000DC670 File Offset: 0x000DA870
	public global::StructureMaster.StructureMaterialType GetMaterialType()
	{
		return this._materialType;
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x000DC678 File Offset: 0x000DA878
	public float GetDecayDelayForType(global::StructureMaster.StructureMaterialType type)
	{
		switch (type)
		{
		default:
			return 0f;
		case global::StructureMaster.StructureMaterialType.Wood:
			return 172800f;
		case global::StructureMaster.StructureMaterialType.Metal:
			return 345600f;
		case global::StructureMaster.StructureMaterialType.Brick:
			return 259200f;
		case global::StructureMaster.StructureMaterialType.Concrete:
			return 432000f;
		}
	}

	// Token: 0x06003D85 RID: 15749 RVA: 0x000DC6C4 File Offset: 0x000DA8C4
	public float GetDecayTimeMaxHealthForType(global::StructureMaster.StructureMaterialType type)
	{
		switch (type)
		{
		default:
			return 60f;
		case global::StructureMaster.StructureMaterialType.Wood:
			return 21600f;
		case global::StructureMaster.StructureMaterialType.Metal:
			return 43200f;
		case global::StructureMaster.StructureMaterialType.Brick:
			return 86400f;
		case global::StructureMaster.StructureMaterialType.Concrete:
			return 259200f;
		}
	}

	// Token: 0x06003D86 RID: 15750 RVA: 0x000DC710 File Offset: 0x000DA910
	public float GetDecayTimeMaxHealth()
	{
		return this.GetDecayTimeMaxHealthForType(this._materialType);
	}

	// Token: 0x06003D87 RID: 15751 RVA: 0x000DC720 File Offset: 0x000DA920
	public float GetDecayDelay()
	{
		return this.GetDecayDelayForType(this._materialType);
	}

	// Token: 0x06003D88 RID: 15752 RVA: 0x000DC730 File Offset: 0x000DA930
	public void Awake()
	{
		this._structureComponents = new HashSet<global::StructureComponent>();
		this._structureComponentsByPosition = new Dictionary<global::StructureComponentKey, global::StructureMaster.CompPosNode>();
		global::StructureMaster.g_Structures.Add(this);
	}

	// Token: 0x06003D89 RID: 15753 RVA: 0x000DC754 File Offset: 0x000DA954
	public void OnDestroy()
	{
		try
		{
			global::StructureMaster.g_StructuresWithBounds.Remove(this);
			global::StructureMaster.g_Structures.Remove(this);
		}
		finally
		{
			base.OnDestroy();
		}
	}

	// Token: 0x06003D8A RID: 15754 RVA: 0x000DC7A4 File Offset: 0x000DA9A4
	public bool GetBounds(out Bounds bounds)
	{
		bounds = this.containedBounds;
		return this._structureComponents.Count > 0;
	}

	// Token: 0x06003D8B RID: 15755 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
	public void AddWeightLink(global::StructureComponent me, global::StructureComponent weight)
	{
		if (this._weightOnMe.ContainsKey(me))
		{
			this._weightOnMe[me].Add(weight);
		}
		else
		{
			this._weightOnMe.Add(me, new HashSet<global::StructureComponent>());
			this._weightOnMe[me].Add(weight);
		}
		if (this._hasWeightOn.ContainsKey(weight))
		{
			this._hasWeightOn[weight].Add(me);
		}
		else
		{
			this._hasWeightOn.Add(weight, new HashSet<global::StructureComponent>());
			this._hasWeightOn[weight].Add(me);
		}
	}

	// Token: 0x06003D8C RID: 15756 RVA: 0x000DC868 File Offset: 0x000DAA68
	public Vector3 LocalIndexRound(Vector3 toRound)
	{
		return toRound;
	}

	// Token: 0x06003D8D RID: 15757 RVA: 0x000DC86C File Offset: 0x000DAA6C
	public void RemoveLinkForComp(global::StructureComponent comp)
	{
		if (this._weightOnMe.ContainsKey(comp))
		{
			foreach (global::StructureComponent key in this._weightOnMe[comp])
			{
				if (this._hasWeightOn[key].Contains(comp))
				{
					this._hasWeightOn[key].Remove(comp);
					if (this._hasWeightOn[key].Count == 0)
					{
						this._hasWeightOn.Remove(key);
					}
				}
			}
			this._weightOnMe.Remove(comp);
		}
		if (this._hasWeightOn.ContainsKey(comp))
		{
			foreach (global::StructureComponent key2 in this._hasWeightOn[comp])
			{
				if (this._weightOnMe[key2].Contains(comp))
				{
					this._weightOnMe[key2].Remove(comp);
					if (this._weightOnMe[key2].Count == 0)
					{
						this._weightOnMe.Remove(key2);
					}
				}
			}
			this._hasWeightOn.Remove(comp);
		}
	}

	// Token: 0x06003D8E RID: 15758 RVA: 0x000DC9FC File Offset: 0x000DABFC
	public void GenerateLinkForComp(global::StructureComponent comp)
	{
		if (this._hasWeightOn == null)
		{
			this._hasWeightOn = new Dictionary<global::StructureComponent, HashSet<global::StructureComponent>>();
		}
		if (this._weightOnMe == null)
		{
			this._weightOnMe = new Dictionary<global::StructureComponent, HashSet<global::StructureComponent>>();
		}
		Vector3 vector = this.LocalIndexRound(base.transform.InverseTransformPoint(comp.transform.position));
		if (comp.type == global::StructureComponent.StructureComponentType.Wall || comp.type == global::StructureComponent.StructureComponentType.Doorway || comp.type == global::StructureComponent.StructureComponentType.WindowWall)
		{
			Vector3 worldPos = comp.transform.position + comp.transform.rotation * -Vector3.right * 2.5f;
			global::StructureComponent componentFromPositionWorld = this.GetComponentFromPositionWorld(worldPos);
			Vector3 worldPos2 = comp.transform.position + comp.transform.rotation * Vector3.right * 2.5f;
			global::StructureComponent componentFromPositionWorld2 = this.GetComponentFromPositionWorld(worldPos2);
			if (componentFromPositionWorld && componentFromPositionWorld.type == global::StructureComponent.StructureComponentType.Pillar)
			{
				this.AddWeightLink(componentFromPositionWorld, comp);
			}
			if (componentFromPositionWorld2 && componentFromPositionWorld2.type == global::StructureComponent.StructureComponentType.Pillar)
			{
				this.AddWeightLink(componentFromPositionWorld2, comp);
			}
		}
		else if (comp.type == global::StructureComponent.StructureComponentType.Pillar)
		{
			global::StructureComponent structureComponent = this.CompByLocal(vector - new Vector3(0f, global::StructureMaster.gridSpacingY, 0f), global::StructureComponent.StructureComponentType.Pillar);
			if (structureComponent)
			{
				this.AddWeightLink(structureComponent, comp);
			}
			float num = -global::StructureMaster.gridSpacingY;
			Vector3[] array = new Vector3[]
			{
				new Vector3(-2.5f, num, -2.5f),
				new Vector3(2.5f, num, 2.5f),
				new Vector3(-2.5f, num, 2.5f),
				new Vector3(2.5f, num, -2.5f),
				new Vector3(2.5f, num, 0f),
				new Vector3(-2.5f, num, 0f),
				new Vector3(0f, num, 2.5f),
				new Vector3(0f, num, -2.5f),
				new Vector3(0f, num, 0f)
			};
			foreach (Vector3 vector2 in array)
			{
				global::StructureComponent structureComponent2 = this.CompByLocal(vector + vector2, global::StructureComponent.StructureComponentType.Foundation);
				global::StructureComponent structureComponent3 = this.CompByLocal(vector + vector2, global::StructureComponent.StructureComponentType.Ceiling);
				if (structureComponent2)
				{
					this.AddWeightLink(structureComponent2, comp);
				}
				if (structureComponent3)
				{
					this.AddWeightLink(structureComponent3, comp);
				}
			}
		}
		else if (comp.type == global::StructureComponent.StructureComponentType.Ceiling)
		{
			Vector3[] array3 = new Vector3[]
			{
				new Vector3(-2.5f, 0f, -2.5f),
				new Vector3(2.5f, 0f, 2.5f),
				new Vector3(-2.5f, 0f, 2.5f),
				new Vector3(2.5f, 0f, -2.5f)
			};
			foreach (Vector3 vector3 in array3)
			{
				global::StructureComponent structureComponent4 = this.CompByLocal(vector + vector3, global::StructureComponent.StructureComponentType.Pillar);
				if (structureComponent4 != null)
				{
					this.AddWeightLink(structureComponent4, comp);
				}
			}
		}
		else if (comp.type == global::StructureComponent.StructureComponentType.Ramp)
		{
			global::StructureComponent structureComponent5 = this.CompByLocal(vector - new Vector3(0f, global::StructureMaster.gridSpacingY, 0f));
			if (structureComponent5)
			{
				this.AddWeightLink(structureComponent5, comp);
			}
		}
		else if (comp.type == global::StructureComponent.StructureComponentType.Foundation)
		{
			global::StructureComponent structureComponent6 = this.CompByLocal(vector - new Vector3(0f, global::StructureMaster.gridSpacingY, 0f), global::StructureComponent.StructureComponentType.Foundation);
			if (structureComponent6)
			{
				if (structureComponent6 != comp)
				{
					this.AddWeightLink(structureComponent6, comp);
				}
				else
				{
					Debug.Log("MAJOR FUCKUP");
				}
			}
		}
		else if (comp.type == global::StructureComponent.StructureComponentType.Stairs)
		{
			Vector3[] array5 = new Vector3[]
			{
				new Vector3(-2.5f, 0f, -2.5f),
				new Vector3(2.5f, 0f, 2.5f),
				new Vector3(-2.5f, 0f, 2.5f),
				new Vector3(2.5f, 0f, -2.5f)
			};
			foreach (Vector3 vector4 in array5)
			{
				global::StructureComponent structureComponent7 = this.CompByLocal(vector + vector4, global::StructureComponent.StructureComponentType.Pillar);
				if (structureComponent7 != null && structureComponent7.type == global::StructureComponent.StructureComponentType.Pillar)
				{
					this.AddWeightLink(structureComponent7, comp);
				}
			}
		}
	}

	// Token: 0x06003D8F RID: 15759 RVA: 0x000DCF9C File Offset: 0x000DB19C
	public void GenerateLinks()
	{
		this._hasWeightOn = new Dictionary<global::StructureComponent, HashSet<global::StructureComponent>>();
		this._weightOnMe = new Dictionary<global::StructureComponent, HashSet<global::StructureComponent>>();
		foreach (global::StructureComponent comp in this._structureComponents)
		{
			this.GenerateLinkForComp(comp);
		}
	}

	// Token: 0x06003D90 RID: 15760 RVA: 0x000DD018 File Offset: 0x000DB218
	public bool CheckIsWall(global::StructureComponent wallTest)
	{
		return wallTest != null && wallTest.IsWallType();
	}

	// Token: 0x06003D91 RID: 15761 RVA: 0x000DD030 File Offset: 0x000DB230
	public bool ComponentCarryingWeight(global::StructureComponent comp)
	{
		return this._weightOnMe != null && this._weightOnMe.ContainsKey(comp) && this._weightOnMe[comp].Count > 0;
	}

	// Token: 0x17000BB1 RID: 2993
	// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000DD074 File Offset: 0x000DB274
	// (set) Token: 0x06003D93 RID: 15763 RVA: 0x000DD07C File Offset: 0x000DB27C
	private static float decayRate
	{
		get
		{
			return global::StructureMaster._decayRate;
		}
		set
		{
			global::StructureMaster._decayRate = value;
		}
	}

	// Token: 0x06003D94 RID: 15764 RVA: 0x000DD084 File Offset: 0x000DB284
	public void Touched()
	{
		this._decayDelayRemaining = this.GetDecayDelay();
	}

	// Token: 0x06003D95 RID: 15765 RVA: 0x000DD094 File Offset: 0x000DB294
	public static Vector3 SnapToGrid(Transform gridCenter, Vector3 desiredPosition, bool useHeight)
	{
		Vector3 vector = gridCenter.InverseTransformPoint(desiredPosition);
		vector.x = Mathf.Round(vector.x / global::StructureMaster.gridSpacingXZ) * global::StructureMaster.gridSpacingXZ;
		vector.z = Mathf.Round(vector.z / global::StructureMaster.gridSpacingXZ) * global::StructureMaster.gridSpacingXZ;
		if (useHeight)
		{
			vector.y = Mathf.Round(vector.y / global::StructureMaster.gridSpacingY) * global::StructureMaster.gridSpacingY;
		}
		vector = gridCenter.TransformPoint(vector);
		return vector;
	}

	// Token: 0x06003D96 RID: 15766 RVA: 0x000DD118 File Offset: 0x000DB318
	public bool AddCompPositionEntry(global::StructureComponent comp)
	{
		Vector3 v = this.LocalIndexRound(base.transform.InverseTransformPoint(comp.transform.position));
		global::StructureComponentKey key = new global::StructureComponentKey(v);
		global::StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			compPosNode.Add(comp);
		}
		else
		{
			compPosNode = new global::StructureMaster.CompPosNode();
			compPosNode.Add(comp);
			this._structureComponentsByPosition.Add(key, compPosNode);
		}
		return true;
	}

	// Token: 0x06003D97 RID: 15767 RVA: 0x000DD184 File Offset: 0x000DB384
	public bool RemoveCompPositionEntry(global::StructureComponent comp)
	{
		Vector3 v = this.LocalIndexRound(base.transform.InverseTransformPoint(comp.transform.position));
		global::StructureComponentKey key = new global::StructureComponentKey(v);
		global::StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			compPosNode.Remove(comp);
			if (compPosNode.GetAny())
			{
				this._structureComponentsByPosition.Remove(key);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06003D98 RID: 15768 RVA: 0x000DD1F0 File Offset: 0x000DB3F0
	public global::StructureComponent CompByLocal(Vector3 localPos)
	{
		global::StructureComponentKey key = new global::StructureComponentKey(localPos);
		global::StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			return compPosNode.GetAny();
		}
		return null;
	}

	// Token: 0x06003D99 RID: 15769 RVA: 0x000DD220 File Offset: 0x000DB420
	public global::StructureComponent CompByLocal(Vector3 localPos, global::StructureComponent.StructureComponentType type)
	{
		global::StructureComponentKey key = new global::StructureComponentKey(localPos);
		global::StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			return compPosNode.GetType(type);
		}
		return null;
	}

	// Token: 0x06003D9A RID: 15770 RVA: 0x000DD254 File Offset: 0x000DB454
	public void TryGenerateLOD()
	{
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x000DD258 File Offset: 0x000DB458
	public void GenerateLOD()
	{
		base.GetComponent<global::CombineChildren>().DoCombine();
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x000DD268 File Offset: 0x000DB468
	internal void AppendStructureComponent(global::StructureComponent comp)
	{
		this.AppendStructureComponent(comp, false);
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x000DD274 File Offset: 0x000DB474
	protected void AppendStructureComponent(global::StructureComponent comp, bool nobind)
	{
		if (comp.type == global::StructureComponent.StructureComponentType.Foundation && this._materialType == global::StructureMaster.StructureMaterialType.UNSET)
		{
			this.SetMaterialType(comp.GetMaterialType());
		}
		this._structureComponents.Add(comp);
		this.AddCompPositionEntry(comp);
		this.GenerateLinkForComp(comp);
		this.RecalculateStructureLinks();
		this.MarkBoundsDirty();
		if (!nobind)
		{
			try
			{
				comp.OnOwnedByMasterStructure(this);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}
		if (this._structureComponents.Count == 1)
		{
			global::StructureMaster.g_StructuresWithBounds.Add(this);
		}
		if (this.meshBatchTargetGraphical)
		{
			foreach (MeshBatchInstance meshBatchInstance in comp.GetComponentsInChildren<MeshBatchInstance>(true))
			{
				meshBatchInstance.graphicalTarget = this.meshBatchTargetGraphical;
			}
		}
	}

	// Token: 0x06003D9E RID: 15774 RVA: 0x000DD360 File Offset: 0x000DB560
	public bool RemoveComponent(global::StructureComponent comp)
	{
		this.RecalculateStructureLinks();
		this.MarkBoundsDirty();
		return true;
	}

	// Token: 0x06003D9F RID: 15775 RVA: 0x000DD370 File Offset: 0x000DB570
	public void RecalculateBounds()
	{
		this._containedBounds = new Bounds(base.transform.position, Vector3.zero);
		foreach (global::StructureComponent structureComponent in this._structureComponents)
		{
			this._containedBounds.Encapsulate(structureComponent.collider.bounds);
		}
		this.RecalculateStructureSize();
		this._containedBounds.Expand(5f);
		this._boundsDirty = false;
	}

	// Token: 0x06003DA0 RID: 15776 RVA: 0x000DD420 File Offset: 0x000DB620
	public void RecalculateStructureSize()
	{
		Bounds localBounds;
		localBounds..ctor(Vector3.zero, Vector3.zero);
		foreach (global::StructureComponent structureComponent in this._structureComponents)
		{
			if (structureComponent.type == global::StructureComponent.StructureComponentType.Foundation)
			{
				Vector3 vector = base.transform.InverseTransformPoint(structureComponent.transform.position);
				localBounds.Encapsulate(vector);
			}
			else if (structureComponent.type == global::StructureComponent.StructureComponentType.Pillar)
			{
			}
		}
		localBounds.Expand(global::StructureMaster.gridSpacingXZ * 2f);
		this._localBounds = localBounds;
	}

	// Token: 0x06003DA1 RID: 15777 RVA: 0x000DD4E4 File Offset: 0x000DB6E4
	public void GetStructureSize(out int maxWidth, out int maxLength, out int maxHeight)
	{
		Bounds containedBounds = this.containedBounds;
		float num = this._localBounds.size.x / (global::StructureMaster.gridSpacingXZ * 2f);
		float num2 = this._localBounds.size.z / (global::StructureMaster.gridSpacingXZ * 2f);
		float num3 = this._localBounds.size.y / global::StructureMaster.gridSpacingY;
		maxWidth = Mathf.RoundToInt(num);
		maxLength = Mathf.RoundToInt(num2);
		maxHeight = Mathf.RoundToInt(num3);
	}

	// Token: 0x06003DA2 RID: 15778 RVA: 0x000DD570 File Offset: 0x000DB770
	public void RecalculateStructureLinks()
	{
	}

	// Token: 0x06003DA3 RID: 15779 RVA: 0x000DD574 File Offset: 0x000DB774
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(this.containedBounds.center, this.containedBounds.size);
	}

	// Token: 0x06003DA4 RID: 15780 RVA: 0x000DD5AC File Offset: 0x000DB7AC
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(this.containedBounds.center, this.containedBounds.size);
		if (this._hasWeightOn == null)
		{
			return;
		}
		foreach (KeyValuePair<global::StructureComponent, HashSet<global::StructureComponent>> keyValuePair in this._hasWeightOn)
		{
			if (keyValuePair.Key)
			{
				Gizmos.color = Color.gray;
				Gizmos.DrawWireSphere(keyValuePair.Key.transform.position + new Vector3(0f, 0.25f, 0f), 0.25f);
				Gizmos.color = Color.green;
				foreach (global::StructureComponent structureComponent in keyValuePair.Value)
				{
					if (structureComponent)
					{
						Gizmos.DrawLine(keyValuePair.Key.transform.position, structureComponent.transform.position);
					}
				}
			}
		}
	}

	// Token: 0x06003DA5 RID: 15781 RVA: 0x000DD720 File Offset: 0x000DB920
	public global::StructureComponent GetComponentFromPositionWorld(Vector3 worldPos)
	{
		Vector3 localPos = this.LocalIndexRound(base.transform.InverseTransformPoint(worldPos));
		return this.CompByLocal(localPos);
	}

	// Token: 0x06003DA6 RID: 15782 RVA: 0x000DD748 File Offset: 0x000DB948
	public global::StructureComponent GetComponentFromPositionLocal(Vector3 localPos)
	{
		foreach (global::StructureComponent structureComponent in this._structureComponents)
		{
			if (Vector3.Distance(localPos, base.transform.InverseTransformPoint(structureComponent.transform.position)) < 0.01f)
			{
				return structureComponent;
			}
		}
		return null;
	}

	// Token: 0x06003DA7 RID: 15783 RVA: 0x000DD7D8 File Offset: 0x000DB9D8
	public bool Approx(float a, float b)
	{
		return (double)Mathf.Abs(a - b) < 0.001;
	}

	// Token: 0x06003DA8 RID: 15784 RVA: 0x000DD7F4 File Offset: 0x000DB9F4
	public bool IsValidFoundationSpot(Vector3 searchPos)
	{
		if (this._structureComponents.Count == 0)
		{
			return true;
		}
		foreach (global::StructureComponent structureComponent in this._structureComponents)
		{
			if (structureComponent.type == global::StructureComponent.StructureComponentType.Foundation)
			{
				Vector3 vector = structureComponent.transform.InverseTransformPoint(searchPos);
				bool flag = ((this.Approx(Mathf.Abs(vector.x), 5f) && this.Approx(vector.z, 0f)) || (this.Approx(Mathf.Abs(vector.z), 5f) && this.Approx(vector.x, 0f))) && this.Approx(vector.y, 0f);
				bool flag2 = false;
				Vector3 vector2;
				Vector3 vector3;
				if (global::TransformHelpers.GetGroundInfoTerrainOnly(searchPos + new Vector3(0f, 3.5f, 0f), 3.5f, out vector2, out vector3))
				{
					flag2 = true;
				}
				if (flag && !flag2)
				{
					flag = false;
				}
				bool flag3 = false;
				if (flag || flag3)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06003DA9 RID: 15785 RVA: 0x000DD960 File Offset: 0x000DBB60
	public bool GetFoundationForPoint(Vector3 searchPos)
	{
		foreach (global::StructureComponent structureComponent in this._structureComponents)
		{
			if (structureComponent.type == global::StructureComponent.StructureComponentType.Foundation)
			{
				Vector3 vector = structureComponent.transform.InverseTransformPoint(searchPos);
				if (Mathf.Abs(vector.x) <= 2.51f && Mathf.Abs(vector.z) <= 2.51f && this.Approx(vector.y, 4f))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06003DAA RID: 15786 RVA: 0x000DDA2C File Offset: 0x000DBC2C
	public void CacheCreator()
	{
	}

	// Token: 0x06003DAB RID: 15787 RVA: 0x000DDA30 File Offset: 0x000DBC30
	[RPC]
	public void GetOwnerInfo(ulong creator, ulong owner)
	{
		this.creatorID = creator;
		this.ownerID = owner;
	}

	// Token: 0x06003DAC RID: 15788 RVA: 0x000DDA40 File Offset: 0x000DBC40
	public bool CullComponent(global::StructureComponent component)
	{
		if (component && this._structureComponents.Remove(component))
		{
			this.RemoveCompPositionEntry(component);
			this.RecalculateStructureLinks();
			this.MarkBoundsDirty();
			if (this._structureComponents.Count == 0)
			{
				global::StructureMaster.g_StructuresWithBounds.Remove(this);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06003DAD RID: 15789 RVA: 0x000DDA9C File Offset: 0x000DBC9C
	public int FindComponentID(global::StructureComponent component)
	{
		int num = 0;
		foreach (global::StructureComponent structureComponent in this._structureComponents)
		{
			if (!(structureComponent != component))
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x04001F79 RID: 8057
	public static Vector3 foundationSize = new Vector3(5f, 0.5f, 5f);

	// Token: 0x04001F7A RID: 8058
	public static float gridSpacingXZ = 2.5f;

	// Token: 0x04001F7B RID: 8059
	public static float gridSpacingY = 4f;

	// Token: 0x04001F7C RID: 8060
	[SerializeField]
	private Facepunch.MeshBatch.MeshBatchGraphicalTarget meshBatchTargetGraphical;

	// Token: 0x04001F7D RID: 8061
	[SerializeField]
	private Facepunch.MeshBatch.MeshBatchPhysicalTarget meshBatchTargetPhysical;

	// Token: 0x04001F7E RID: 8062
	private static List<global::StructureMaster> g_Structures = new List<global::StructureMaster>();

	// Token: 0x04001F7F RID: 8063
	private static List<global::StructureMaster> g_StructuresWithBounds = new List<global::StructureMaster>();

	// Token: 0x04001F80 RID: 8064
	protected HashSet<global::StructureComponent> _structureComponents;

	// Token: 0x04001F81 RID: 8065
	protected List<Vector3> _foundationPoints;

	// Token: 0x04001F82 RID: 8066
	private bool _boundsDirty = true;

	// Token: 0x04001F83 RID: 8067
	private Bounds _containedBounds;

	// Token: 0x04001F84 RID: 8068
	private Vector3 _buildingSize;

	// Token: 0x04001F85 RID: 8069
	private Bounds _localBounds;

	// Token: 0x04001F86 RID: 8070
	protected int nextID;

	// Token: 0x04001F87 RID: 8071
	protected float _lastDecayTime;

	// Token: 0x04001F88 RID: 8072
	private float _decayDelayRemaining;

	// Token: 0x04001F89 RID: 8073
	private float _pentUpDecayTime;

	// Token: 0x04001F8A RID: 8074
	public ulong creatorID;

	// Token: 0x04001F8B RID: 8075
	public ulong ownerID;

	// Token: 0x04001F8C RID: 8076
	protected global::StructureMaster.StructureMaterialType _materialType;

	// Token: 0x04001F8D RID: 8077
	protected Dictionary<global::StructureComponent, HashSet<global::StructureComponent>> _hasWeightOn;

	// Token: 0x04001F8E RID: 8078
	protected Dictionary<global::StructureComponent, HashSet<global::StructureComponent>> _weightOnMe;

	// Token: 0x04001F8F RID: 8079
	protected Dictionary<global::StructureComponentKey, global::StructureMaster.CompPosNode> _structureComponentsByPosition;

	// Token: 0x04001F90 RID: 8080
	private static float _decayRate = 1f;

	// Token: 0x0200073B RID: 1851
	private static class Empty
	{
		// Token: 0x04001F92 RID: 8082
		public static readonly global::StructureMaster[] array = new global::StructureMaster[0];
	}

	// Token: 0x0200073C RID: 1852
	[Serializable]
	public enum StructureMaterialType
	{
		// Token: 0x04001F94 RID: 8084
		UNSET,
		// Token: 0x04001F95 RID: 8085
		Wood,
		// Token: 0x04001F96 RID: 8086
		Metal,
		// Token: 0x04001F97 RID: 8087
		Brick,
		// Token: 0x04001F98 RID: 8088
		Concrete
	}

	// Token: 0x0200073D RID: 1853
	public class CompPosNode
	{
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000DDB58 File Offset: 0x000DBD58
		public global::StructureComponent GetType(global::StructureComponent.StructureComponentType type)
		{
			switch (type)
			{
			case global::StructureComponent.StructureComponentType.Pillar:
				return this._pillar;
			case global::StructureComponent.StructureComponentType.Wall:
			case global::StructureComponent.StructureComponentType.Doorway:
			case global::StructureComponent.StructureComponentType.WindowWall:
				return this._wall;
			case global::StructureComponent.StructureComponentType.Ceiling:
				return this._ceiling;
			case global::StructureComponent.StructureComponentType.Stairs:
				return this._stairs;
			case global::StructureComponent.StructureComponentType.Foundation:
				return this._foundation;
			default:
				return null;
			}
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000DDBB4 File Offset: 0x000DBDB4
		public void Add(global::StructureComponent toAdd)
		{
			switch (toAdd.type)
			{
			case global::StructureComponent.StructureComponentType.Pillar:
				this._pillar = toAdd;
				break;
			case global::StructureComponent.StructureComponentType.Wall:
			case global::StructureComponent.StructureComponentType.Doorway:
			case global::StructureComponent.StructureComponentType.WindowWall:
				this._wall = toAdd;
				break;
			case global::StructureComponent.StructureComponentType.Ceiling:
				this._ceiling = toAdd;
				break;
			case global::StructureComponent.StructureComponentType.Stairs:
				this._stairs = toAdd;
				break;
			case global::StructureComponent.StructureComponentType.Foundation:
				this._foundation = toAdd;
				break;
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000DDC34 File Offset: 0x000DBE34
		public global::StructureComponent GetAny()
		{
			if (this._ceiling != null)
			{
				return this._ceiling;
			}
			if (this._stairs != null)
			{
				return this._stairs;
			}
			if (this._pillar != null)
			{
				return this._pillar;
			}
			if (this._foundation != null)
			{
				return this._foundation;
			}
			if (this._wall != null)
			{
				return this._wall;
			}
			return null;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000DDCBC File Offset: 0x000DBEBC
		public void Remove(global::StructureComponent toRemove)
		{
			if (this._wall == toRemove)
			{
				this._wall = null;
			}
			else if (this._foundation == toRemove)
			{
				this._foundation = null;
			}
			else if (this._pillar == toRemove)
			{
				this._pillar = null;
			}
			else if (this._stairs == toRemove)
			{
				this._stairs = null;
			}
			else if (this._ceiling == toRemove)
			{
				this._ceiling = null;
			}
		}

		// Token: 0x04001F99 RID: 8089
		public global::StructureComponent _wall;

		// Token: 0x04001F9A RID: 8090
		public global::StructureComponent _foundation;

		// Token: 0x04001F9B RID: 8091
		public global::StructureComponent _pillar;

		// Token: 0x04001F9C RID: 8092
		public global::StructureComponent _stairs;

		// Token: 0x04001F9D RID: 8093
		public global::StructureComponent _ceiling;
	}
}
