using System;
using System.Collections.Generic;
using Facepunch.MeshBatch;
using uLink;
using UnityEngine;

// Token: 0x02000676 RID: 1654
[RequireComponent(typeof(NetworkView))]
public class StructureMaster : IDMain, IServerSaveable, IServerSaveNotify
{
	// Token: 0x06003984 RID: 14724 RVA: 0x000D3998 File Offset: 0x000D1B98
	protected StructureMaster(IDFlags idFlags) : base(idFlags)
	{
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x000D39A8 File Offset: 0x000D1BA8
	public StructureMaster() : this(0)
	{
	}

	// Token: 0x06003987 RID: 14727 RVA: 0x000D3A0C File Offset: 0x000D1C0C
	void IServerSaveNotify.PostLoad()
	{
	}

	// Token: 0x17000B2C RID: 2860
	// (get) Token: 0x06003988 RID: 14728 RVA: 0x000D3A10 File Offset: 0x000D1C10
	public static List<StructureMaster> AllStructuresWithBounds
	{
		get
		{
			return StructureMaster.g_StructuresWithBounds;
		}
	}

	// Token: 0x17000B2D RID: 2861
	// (get) Token: 0x06003989 RID: 14729 RVA: 0x000D3A18 File Offset: 0x000D1C18
	public static List<StructureMaster> AllStructures
	{
		get
		{
			return StructureMaster.g_Structures;
		}
	}

	// Token: 0x0600398A RID: 14730 RVA: 0x000D3A20 File Offset: 0x000D1C20
	public static StructureMaster[] RayTestStructures(Ray ray)
	{
		return StructureMaster.RayTestStructures(ray, 10f);
	}

	// Token: 0x0600398B RID: 14731 RVA: 0x000D3A30 File Offset: 0x000D1C30
	public static StructureMaster[] RayTestStructures(Ray ray, float maxDistance)
	{
		List<StructureMaster> list = null;
		bool flag = false;
		List<KeyValuePair<StructureMaster, float>> list2 = new List<KeyValuePair<StructureMaster, float>>();
		foreach (StructureMaster structureMaster in StructureMaster.AllStructuresWithBounds)
		{
			if (!structureMaster)
			{
				if (!flag)
				{
					flag = true;
					list = new List<StructureMaster>();
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
						list = new List<StructureMaster>();
					}
					list.Add(structureMaster);
					Debug.LogException(ex, structureMaster);
					continue;
				}
				if (bounds && bounds2.IntersectRay(ray, ref num) && num <= maxDistance)
				{
					list2.Add(new KeyValuePair<StructureMaster, float>(structureMaster, num));
				}
			}
		}
		if (flag)
		{
			foreach (StructureMaster item in list)
			{
				StructureMaster.g_Structures.Remove(item);
				StructureMaster.g_StructuresWithBounds.Remove(item);
			}
		}
		if (list2.Count == 0)
		{
			return StructureMaster.Empty.array;
		}
		list2.Sort((KeyValuePair<StructureMaster, float> x, KeyValuePair<StructureMaster, float> y) => x.Value.CompareTo(y.Value));
		StructureMaster[] array = new StructureMaster[list2.Count];
		int num2 = 0;
		foreach (KeyValuePair<StructureMaster, float> keyValuePair in list2)
		{
			array[num2++] = keyValuePair.Key;
		}
		return array;
	}

	// Token: 0x17000B2E RID: 2862
	// (get) Token: 0x0600398C RID: 14732 RVA: 0x000D3C54 File Offset: 0x000D1E54
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

	// Token: 0x0600398D RID: 14733 RVA: 0x000D3C70 File Offset: 0x000D1E70
	public void MarkBoundsDirty()
	{
		this._boundsDirty = true;
	}

	// Token: 0x0600398E RID: 14734 RVA: 0x000D3C7C File Offset: 0x000D1E7C
	public void SetMaterialType(StructureMaster.StructureMaterialType type)
	{
		if (this._materialType == StructureMaster.StructureMaterialType.UNSET)
		{
			this._materialType = type;
		}
	}

	// Token: 0x0600398F RID: 14735 RVA: 0x000D3C90 File Offset: 0x000D1E90
	public StructureMaster.StructureMaterialType GetMaterialType()
	{
		return this._materialType;
	}

	// Token: 0x06003990 RID: 14736 RVA: 0x000D3C98 File Offset: 0x000D1E98
	public float GetDecayDelayForType(StructureMaster.StructureMaterialType type)
	{
		switch (type)
		{
		default:
			return 0f;
		case StructureMaster.StructureMaterialType.Wood:
			return 172800f;
		case StructureMaster.StructureMaterialType.Metal:
			return 345600f;
		case StructureMaster.StructureMaterialType.Brick:
			return 259200f;
		case StructureMaster.StructureMaterialType.Concrete:
			return 432000f;
		}
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x000D3CE4 File Offset: 0x000D1EE4
	public float GetDecayTimeMaxHealthForType(StructureMaster.StructureMaterialType type)
	{
		switch (type)
		{
		default:
			return 60f;
		case StructureMaster.StructureMaterialType.Wood:
			return 21600f;
		case StructureMaster.StructureMaterialType.Metal:
			return 43200f;
		case StructureMaster.StructureMaterialType.Brick:
			return 86400f;
		case StructureMaster.StructureMaterialType.Concrete:
			return 259200f;
		}
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x000D3D30 File Offset: 0x000D1F30
	public float GetDecayTimeMaxHealth()
	{
		return this.GetDecayTimeMaxHealthForType(this._materialType);
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x000D3D40 File Offset: 0x000D1F40
	public float GetDecayDelay()
	{
		return this.GetDecayDelayForType(this._materialType);
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x000D3D50 File Offset: 0x000D1F50
	public void Awake()
	{
		this._structureComponents = new HashSet<StructureComponent>();
		this._structureComponentsByPosition = new Dictionary<StructureComponentKey, StructureMaster.CompPosNode>();
		StructureMaster.g_Structures.Add(this);
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x000D3D74 File Offset: 0x000D1F74
	public void OnDestroy()
	{
		try
		{
			StructureMaster.g_StructuresWithBounds.Remove(this);
			StructureMaster.g_Structures.Remove(this);
		}
		finally
		{
			base.OnDestroy();
		}
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x000D3DC4 File Offset: 0x000D1FC4
	public bool GetBounds(out Bounds bounds)
	{
		bounds = this.containedBounds;
		return this._structureComponents.Count > 0;
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x000D3DE0 File Offset: 0x000D1FE0
	public void AddWeightLink(StructureComponent me, StructureComponent weight)
	{
		if (this._weightOnMe.ContainsKey(me))
		{
			this._weightOnMe[me].Add(weight);
		}
		else
		{
			this._weightOnMe.Add(me, new HashSet<StructureComponent>());
			this._weightOnMe[me].Add(weight);
		}
		if (this._hasWeightOn.ContainsKey(weight))
		{
			this._hasWeightOn[weight].Add(me);
		}
		else
		{
			this._hasWeightOn.Add(weight, new HashSet<StructureComponent>());
			this._hasWeightOn[weight].Add(me);
		}
	}

	// Token: 0x06003998 RID: 14744 RVA: 0x000D3E88 File Offset: 0x000D2088
	public Vector3 LocalIndexRound(Vector3 toRound)
	{
		return toRound;
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x000D3E8C File Offset: 0x000D208C
	public void RemoveLinkForComp(StructureComponent comp)
	{
		if (this._weightOnMe.ContainsKey(comp))
		{
			foreach (StructureComponent key in this._weightOnMe[comp])
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
			foreach (StructureComponent key2 in this._hasWeightOn[comp])
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

	// Token: 0x0600399A RID: 14746 RVA: 0x000D401C File Offset: 0x000D221C
	public void GenerateLinkForComp(StructureComponent comp)
	{
		if (this._hasWeightOn == null)
		{
			this._hasWeightOn = new Dictionary<StructureComponent, HashSet<StructureComponent>>();
		}
		if (this._weightOnMe == null)
		{
			this._weightOnMe = new Dictionary<StructureComponent, HashSet<StructureComponent>>();
		}
		Vector3 vector = this.LocalIndexRound(base.transform.InverseTransformPoint(comp.transform.position));
		if (comp.type == StructureComponent.StructureComponentType.Wall || comp.type == StructureComponent.StructureComponentType.Doorway || comp.type == StructureComponent.StructureComponentType.WindowWall)
		{
			Vector3 worldPos = comp.transform.position + comp.transform.rotation * -Vector3.right * 2.5f;
			StructureComponent componentFromPositionWorld = this.GetComponentFromPositionWorld(worldPos);
			Vector3 worldPos2 = comp.transform.position + comp.transform.rotation * Vector3.right * 2.5f;
			StructureComponent componentFromPositionWorld2 = this.GetComponentFromPositionWorld(worldPos2);
			if (componentFromPositionWorld && componentFromPositionWorld.type == StructureComponent.StructureComponentType.Pillar)
			{
				this.AddWeightLink(componentFromPositionWorld, comp);
			}
			if (componentFromPositionWorld2 && componentFromPositionWorld2.type == StructureComponent.StructureComponentType.Pillar)
			{
				this.AddWeightLink(componentFromPositionWorld2, comp);
			}
		}
		else if (comp.type == StructureComponent.StructureComponentType.Pillar)
		{
			StructureComponent structureComponent = this.CompByLocal(vector - new Vector3(0f, StructureMaster.gridSpacingY, 0f), StructureComponent.StructureComponentType.Pillar);
			if (structureComponent)
			{
				this.AddWeightLink(structureComponent, comp);
			}
			float num = -StructureMaster.gridSpacingY;
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
				StructureComponent structureComponent2 = this.CompByLocal(vector + vector2, StructureComponent.StructureComponentType.Foundation);
				StructureComponent structureComponent3 = this.CompByLocal(vector + vector2, StructureComponent.StructureComponentType.Ceiling);
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
		else if (comp.type == StructureComponent.StructureComponentType.Ceiling)
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
				StructureComponent structureComponent4 = this.CompByLocal(vector + vector3, StructureComponent.StructureComponentType.Pillar);
				if (structureComponent4 != null)
				{
					this.AddWeightLink(structureComponent4, comp);
				}
			}
		}
		else if (comp.type == StructureComponent.StructureComponentType.Ramp)
		{
			StructureComponent structureComponent5 = this.CompByLocal(vector - new Vector3(0f, StructureMaster.gridSpacingY, 0f));
			if (structureComponent5)
			{
				this.AddWeightLink(structureComponent5, comp);
			}
		}
		else if (comp.type == StructureComponent.StructureComponentType.Foundation)
		{
			StructureComponent structureComponent6 = this.CompByLocal(vector - new Vector3(0f, StructureMaster.gridSpacingY, 0f), StructureComponent.StructureComponentType.Foundation);
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
		else if (comp.type == StructureComponent.StructureComponentType.Stairs)
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
				StructureComponent structureComponent7 = this.CompByLocal(vector + vector4, StructureComponent.StructureComponentType.Pillar);
				if (structureComponent7 != null && structureComponent7.type == StructureComponent.StructureComponentType.Pillar)
				{
					this.AddWeightLink(structureComponent7, comp);
				}
			}
		}
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x000D45BC File Offset: 0x000D27BC
	public void GenerateLinks()
	{
		this._hasWeightOn = new Dictionary<StructureComponent, HashSet<StructureComponent>>();
		this._weightOnMe = new Dictionary<StructureComponent, HashSet<StructureComponent>>();
		foreach (StructureComponent comp in this._structureComponents)
		{
			this.GenerateLinkForComp(comp);
		}
	}

	// Token: 0x0600399C RID: 14748 RVA: 0x000D4638 File Offset: 0x000D2838
	public bool CheckIsWall(StructureComponent wallTest)
	{
		return wallTest != null && wallTest.IsWallType();
	}

	// Token: 0x0600399D RID: 14749 RVA: 0x000D4650 File Offset: 0x000D2850
	public bool ComponentCarryingWeight(StructureComponent comp)
	{
		return this._weightOnMe != null && this._weightOnMe.ContainsKey(comp) && this._weightOnMe[comp].Count > 0;
	}

	// Token: 0x17000B2F RID: 2863
	// (get) Token: 0x0600399E RID: 14750 RVA: 0x000D4694 File Offset: 0x000D2894
	// (set) Token: 0x0600399F RID: 14751 RVA: 0x000D469C File Offset: 0x000D289C
	private static float decayRate
	{
		get
		{
			return StructureMaster._decayRate;
		}
		set
		{
			StructureMaster._decayRate = value;
		}
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x000D46A4 File Offset: 0x000D28A4
	public void Touched()
	{
		this._decayDelayRemaining = this.GetDecayDelay();
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x000D46B4 File Offset: 0x000D28B4
	public static Vector3 SnapToGrid(Transform gridCenter, Vector3 desiredPosition, bool useHeight)
	{
		Vector3 vector = gridCenter.InverseTransformPoint(desiredPosition);
		vector.x = Mathf.Round(vector.x / StructureMaster.gridSpacingXZ) * StructureMaster.gridSpacingXZ;
		vector.z = Mathf.Round(vector.z / StructureMaster.gridSpacingXZ) * StructureMaster.gridSpacingXZ;
		if (useHeight)
		{
			vector.y = Mathf.Round(vector.y / StructureMaster.gridSpacingY) * StructureMaster.gridSpacingY;
		}
		vector = gridCenter.TransformPoint(vector);
		return vector;
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x000D4738 File Offset: 0x000D2938
	public bool AddCompPositionEntry(StructureComponent comp)
	{
		Vector3 v = this.LocalIndexRound(base.transform.InverseTransformPoint(comp.transform.position));
		StructureComponentKey key = new StructureComponentKey(v);
		StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			compPosNode.Add(comp);
		}
		else
		{
			compPosNode = new StructureMaster.CompPosNode();
			compPosNode.Add(comp);
			this._structureComponentsByPosition.Add(key, compPosNode);
		}
		return true;
	}

	// Token: 0x060039A3 RID: 14755 RVA: 0x000D47A4 File Offset: 0x000D29A4
	public bool RemoveCompPositionEntry(StructureComponent comp)
	{
		Vector3 v = this.LocalIndexRound(base.transform.InverseTransformPoint(comp.transform.position));
		StructureComponentKey key = new StructureComponentKey(v);
		StructureMaster.CompPosNode compPosNode;
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

	// Token: 0x060039A4 RID: 14756 RVA: 0x000D4810 File Offset: 0x000D2A10
	public StructureComponent CompByLocal(Vector3 localPos)
	{
		StructureComponentKey key = new StructureComponentKey(localPos);
		StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			return compPosNode.GetAny();
		}
		return null;
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x000D4840 File Offset: 0x000D2A40
	public StructureComponent CompByLocal(Vector3 localPos, StructureComponent.StructureComponentType type)
	{
		StructureComponentKey key = new StructureComponentKey(localPos);
		StructureMaster.CompPosNode compPosNode;
		if (this._structureComponentsByPosition.TryGetValue(key, out compPosNode))
		{
			return compPosNode.GetType(type);
		}
		return null;
	}

	// Token: 0x060039A6 RID: 14758 RVA: 0x000D4874 File Offset: 0x000D2A74
	public void TryGenerateLOD()
	{
	}

	// Token: 0x060039A7 RID: 14759 RVA: 0x000D4878 File Offset: 0x000D2A78
	public void GenerateLOD()
	{
		base.GetComponent<CombineChildren>().DoCombine();
	}

	// Token: 0x060039A8 RID: 14760 RVA: 0x000D4888 File Offset: 0x000D2A88
	internal void AppendStructureComponent(StructureComponent comp)
	{
		this.AppendStructureComponent(comp, false);
	}

	// Token: 0x060039A9 RID: 14761 RVA: 0x000D4894 File Offset: 0x000D2A94
	protected void AppendStructureComponent(StructureComponent comp, bool nobind)
	{
		if (comp.type == StructureComponent.StructureComponentType.Foundation && this._materialType == StructureMaster.StructureMaterialType.UNSET)
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
			StructureMaster.g_StructuresWithBounds.Add(this);
		}
		if (this.meshBatchTargetGraphical)
		{
			foreach (MeshBatchInstance meshBatchInstance in comp.GetComponentsInChildren<MeshBatchInstance>(true))
			{
				meshBatchInstance.graphicalTarget = this.meshBatchTargetGraphical;
			}
		}
	}

	// Token: 0x060039AA RID: 14762 RVA: 0x000D4980 File Offset: 0x000D2B80
	public bool RemoveComponent(StructureComponent comp)
	{
		this.RecalculateStructureLinks();
		this.MarkBoundsDirty();
		return true;
	}

	// Token: 0x060039AB RID: 14763 RVA: 0x000D4990 File Offset: 0x000D2B90
	public void RecalculateBounds()
	{
		this._containedBounds = new Bounds(base.transform.position, Vector3.zero);
		foreach (StructureComponent structureComponent in this._structureComponents)
		{
			this._containedBounds.Encapsulate(structureComponent.collider.bounds);
		}
		this.RecalculateStructureSize();
		this._containedBounds.Expand(5f);
		this._boundsDirty = false;
	}

	// Token: 0x060039AC RID: 14764 RVA: 0x000D4A40 File Offset: 0x000D2C40
	public void RecalculateStructureSize()
	{
		Bounds localBounds;
		localBounds..ctor(Vector3.zero, Vector3.zero);
		foreach (StructureComponent structureComponent in this._structureComponents)
		{
			if (structureComponent.type == StructureComponent.StructureComponentType.Foundation)
			{
				Vector3 vector = base.transform.InverseTransformPoint(structureComponent.transform.position);
				localBounds.Encapsulate(vector);
			}
			else if (structureComponent.type == StructureComponent.StructureComponentType.Pillar)
			{
			}
		}
		localBounds.Expand(StructureMaster.gridSpacingXZ * 2f);
		this._localBounds = localBounds;
	}

	// Token: 0x060039AD RID: 14765 RVA: 0x000D4B04 File Offset: 0x000D2D04
	public void GetStructureSize(out int maxWidth, out int maxLength, out int maxHeight)
	{
		Bounds containedBounds = this.containedBounds;
		float num = this._localBounds.size.x / (StructureMaster.gridSpacingXZ * 2f);
		float num2 = this._localBounds.size.z / (StructureMaster.gridSpacingXZ * 2f);
		float num3 = this._localBounds.size.y / StructureMaster.gridSpacingY;
		maxWidth = Mathf.RoundToInt(num);
		maxLength = Mathf.RoundToInt(num2);
		maxHeight = Mathf.RoundToInt(num3);
	}

	// Token: 0x060039AE RID: 14766 RVA: 0x000D4B90 File Offset: 0x000D2D90
	public void RecalculateStructureLinks()
	{
	}

	// Token: 0x060039AF RID: 14767 RVA: 0x000D4B94 File Offset: 0x000D2D94
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(this.containedBounds.center, this.containedBounds.size);
	}

	// Token: 0x060039B0 RID: 14768 RVA: 0x000D4BCC File Offset: 0x000D2DCC
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(this.containedBounds.center, this.containedBounds.size);
		if (this._hasWeightOn == null)
		{
			return;
		}
		foreach (KeyValuePair<StructureComponent, HashSet<StructureComponent>> keyValuePair in this._hasWeightOn)
		{
			if (keyValuePair.Key)
			{
				Gizmos.color = Color.gray;
				Gizmos.DrawWireSphere(keyValuePair.Key.transform.position + new Vector3(0f, 0.25f, 0f), 0.25f);
				Gizmos.color = Color.green;
				foreach (StructureComponent structureComponent in keyValuePair.Value)
				{
					if (structureComponent)
					{
						Gizmos.DrawLine(keyValuePair.Key.transform.position, structureComponent.transform.position);
					}
				}
			}
		}
	}

	// Token: 0x060039B1 RID: 14769 RVA: 0x000D4D40 File Offset: 0x000D2F40
	public StructureComponent GetComponentFromPositionWorld(Vector3 worldPos)
	{
		Vector3 localPos = this.LocalIndexRound(base.transform.InverseTransformPoint(worldPos));
		return this.CompByLocal(localPos);
	}

	// Token: 0x060039B2 RID: 14770 RVA: 0x000D4D68 File Offset: 0x000D2F68
	public StructureComponent GetComponentFromPositionLocal(Vector3 localPos)
	{
		foreach (StructureComponent structureComponent in this._structureComponents)
		{
			if (Vector3.Distance(localPos, base.transform.InverseTransformPoint(structureComponent.transform.position)) < 0.01f)
			{
				return structureComponent;
			}
		}
		return null;
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x000D4DF8 File Offset: 0x000D2FF8
	public bool Approx(float a, float b)
	{
		return (double)Mathf.Abs(a - b) < 0.001;
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x000D4E14 File Offset: 0x000D3014
	public bool IsValidFoundationSpot(Vector3 searchPos)
	{
		if (this._structureComponents.Count == 0)
		{
			return true;
		}
		foreach (StructureComponent structureComponent in this._structureComponents)
		{
			if (structureComponent.type == StructureComponent.StructureComponentType.Foundation)
			{
				Vector3 vector = structureComponent.transform.InverseTransformPoint(searchPos);
				bool flag = ((this.Approx(Mathf.Abs(vector.x), 5f) && this.Approx(vector.z, 0f)) || (this.Approx(Mathf.Abs(vector.z), 5f) && this.Approx(vector.x, 0f))) && this.Approx(vector.y, 0f);
				bool flag2 = false;
				Vector3 vector2;
				Vector3 vector3;
				if (TransformHelpers.GetGroundInfoTerrainOnly(searchPos + new Vector3(0f, 3.5f, 0f), 3.5f, out vector2, out vector3))
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

	// Token: 0x060039B5 RID: 14773 RVA: 0x000D4F80 File Offset: 0x000D3180
	public bool GetFoundationForPoint(Vector3 searchPos)
	{
		foreach (StructureComponent structureComponent in this._structureComponents)
		{
			if (structureComponent.type == StructureComponent.StructureComponentType.Foundation)
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

	// Token: 0x060039B6 RID: 14774 RVA: 0x000D504C File Offset: 0x000D324C
	public void CacheCreator()
	{
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x000D5050 File Offset: 0x000D3250
	[RPC]
	public void GetOwnerInfo(ulong creator, ulong owner)
	{
		this.creatorID = creator;
		this.ownerID = owner;
	}

	// Token: 0x060039B8 RID: 14776 RVA: 0x000D5060 File Offset: 0x000D3260
	public bool CullComponent(StructureComponent component)
	{
		if (component && this._structureComponents.Remove(component))
		{
			this.RemoveCompPositionEntry(component);
			this.RecalculateStructureLinks();
			this.MarkBoundsDirty();
			if (this._structureComponents.Count == 0)
			{
				StructureMaster.g_StructuresWithBounds.Remove(this);
			}
			return true;
		}
		return false;
	}

	// Token: 0x060039B9 RID: 14777 RVA: 0x000D50BC File Offset: 0x000D32BC
	public int FindComponentID(StructureComponent component)
	{
		int num = 0;
		foreach (StructureComponent structureComponent in this._structureComponents)
		{
			if (!(structureComponent != component))
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x04001D81 RID: 7553
	public static Vector3 foundationSize = new Vector3(5f, 0.5f, 5f);

	// Token: 0x04001D82 RID: 7554
	public static float gridSpacingXZ = 2.5f;

	// Token: 0x04001D83 RID: 7555
	public static float gridSpacingY = 4f;

	// Token: 0x04001D84 RID: 7556
	[SerializeField]
	private MeshBatchGraphicalTarget meshBatchTargetGraphical;

	// Token: 0x04001D85 RID: 7557
	[SerializeField]
	private MeshBatchPhysicalTarget meshBatchTargetPhysical;

	// Token: 0x04001D86 RID: 7558
	private static List<StructureMaster> g_Structures = new List<StructureMaster>();

	// Token: 0x04001D87 RID: 7559
	private static List<StructureMaster> g_StructuresWithBounds = new List<StructureMaster>();

	// Token: 0x04001D88 RID: 7560
	protected HashSet<StructureComponent> _structureComponents;

	// Token: 0x04001D89 RID: 7561
	protected List<Vector3> _foundationPoints;

	// Token: 0x04001D8A RID: 7562
	private bool _boundsDirty = true;

	// Token: 0x04001D8B RID: 7563
	private Bounds _containedBounds;

	// Token: 0x04001D8C RID: 7564
	private Vector3 _buildingSize;

	// Token: 0x04001D8D RID: 7565
	private Bounds _localBounds;

	// Token: 0x04001D8E RID: 7566
	protected int nextID;

	// Token: 0x04001D8F RID: 7567
	protected float _lastDecayTime;

	// Token: 0x04001D90 RID: 7568
	private float _decayDelayRemaining;

	// Token: 0x04001D91 RID: 7569
	private float _pentUpDecayTime;

	// Token: 0x04001D92 RID: 7570
	public ulong creatorID;

	// Token: 0x04001D93 RID: 7571
	public ulong ownerID;

	// Token: 0x04001D94 RID: 7572
	protected StructureMaster.StructureMaterialType _materialType;

	// Token: 0x04001D95 RID: 7573
	protected Dictionary<StructureComponent, HashSet<StructureComponent>> _hasWeightOn;

	// Token: 0x04001D96 RID: 7574
	protected Dictionary<StructureComponent, HashSet<StructureComponent>> _weightOnMe;

	// Token: 0x04001D97 RID: 7575
	protected Dictionary<StructureComponentKey, StructureMaster.CompPosNode> _structureComponentsByPosition;

	// Token: 0x04001D98 RID: 7576
	private static float _decayRate = 1f;

	// Token: 0x02000677 RID: 1655
	private static class Empty
	{
		// Token: 0x04001D9A RID: 7578
		public static readonly StructureMaster[] array = new StructureMaster[0];
	}

	// Token: 0x02000678 RID: 1656
	[Serializable]
	public enum StructureMaterialType
	{
		// Token: 0x04001D9C RID: 7580
		UNSET,
		// Token: 0x04001D9D RID: 7581
		Wood,
		// Token: 0x04001D9E RID: 7582
		Metal,
		// Token: 0x04001D9F RID: 7583
		Brick,
		// Token: 0x04001DA0 RID: 7584
		Concrete
	}

	// Token: 0x02000679 RID: 1657
	public class CompPosNode
	{
		// Token: 0x060039BD RID: 14781 RVA: 0x000D5178 File Offset: 0x000D3378
		public StructureComponent GetType(StructureComponent.StructureComponentType type)
		{
			switch (type)
			{
			case StructureComponent.StructureComponentType.Pillar:
				return this._pillar;
			case StructureComponent.StructureComponentType.Wall:
			case StructureComponent.StructureComponentType.Doorway:
			case StructureComponent.StructureComponentType.WindowWall:
				return this._wall;
			case StructureComponent.StructureComponentType.Ceiling:
				return this._ceiling;
			case StructureComponent.StructureComponentType.Stairs:
				return this._stairs;
			case StructureComponent.StructureComponentType.Foundation:
				return this._foundation;
			default:
				return null;
			}
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000D51D4 File Offset: 0x000D33D4
		public void Add(StructureComponent toAdd)
		{
			switch (toAdd.type)
			{
			case StructureComponent.StructureComponentType.Pillar:
				this._pillar = toAdd;
				break;
			case StructureComponent.StructureComponentType.Wall:
			case StructureComponent.StructureComponentType.Doorway:
			case StructureComponent.StructureComponentType.WindowWall:
				this._wall = toAdd;
				break;
			case StructureComponent.StructureComponentType.Ceiling:
				this._ceiling = toAdd;
				break;
			case StructureComponent.StructureComponentType.Stairs:
				this._stairs = toAdd;
				break;
			case StructureComponent.StructureComponentType.Foundation:
				this._foundation = toAdd;
				break;
			}
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000D5254 File Offset: 0x000D3454
		public StructureComponent GetAny()
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

		// Token: 0x060039C0 RID: 14784 RVA: 0x000D52DC File Offset: 0x000D34DC
		public void Remove(StructureComponent toRemove)
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

		// Token: 0x04001DA1 RID: 7585
		public StructureComponent _wall;

		// Token: 0x04001DA2 RID: 7586
		public StructureComponent _foundation;

		// Token: 0x04001DA3 RID: 7587
		public StructureComponent _pillar;

		// Token: 0x04001DA4 RID: 7588
		public StructureComponent _stairs;

		// Token: 0x04001DA5 RID: 7589
		public StructureComponent _ceiling;
	}
}
