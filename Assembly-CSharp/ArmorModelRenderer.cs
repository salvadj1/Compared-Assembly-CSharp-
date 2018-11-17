using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x0200050C RID: 1292
public sealed class ArmorModelRenderer : IDLocalCharacter
{
	// Token: 0x170009A7 RID: 2471
	// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000AE8FC File Offset: 0x000ACAFC
	public ArmorModelGroup defaultArmorModelGroup
	{
		get
		{
			return ((!this.armorTrait) ? (this.armorTrait = base.GetTrait<CharacterArmorTrait>()) : this.armorTrait).defaultGroup;
		}
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x000AE938 File Offset: 0x000ACB38
	private void OnEnable()
	{
		if (this.awake)
		{
			for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
			{
				ActorMeshRenderer actorMeshRenderer = this.renderers[armorModelSlot];
				if (actorMeshRenderer)
				{
					actorMeshRenderer.renderer.enabled = true;
				}
			}
		}
		else if (this.originalRenderer)
		{
			this.originalRenderer.enabled = true;
		}
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x000AE9A8 File Offset: 0x000ACBA8
	private void OnDisable()
	{
		if (this.awake)
		{
			for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
			{
				ActorMeshRenderer actorMeshRenderer = this.renderers[armorModelSlot];
				if (actorMeshRenderer)
				{
					actorMeshRenderer.renderer.enabled = false;
				}
			}
		}
		else if (this.originalRenderer)
		{
			this.originalRenderer.enabled = false;
		}
	}

	// Token: 0x170009A8 RID: 2472
	// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x000AEA18 File Offset: 0x000ACC18
	public ActorRig actorRig
	{
		get
		{
			return this.boneStructure.actorRig;
		}
	}

	// Token: 0x170009A9 RID: 2473
	public ArmorModel this[ArmorModelSlot slot]
	{
		get
		{
			if (this.awake)
			{
				return this.models[slot];
			}
			ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
			if (defaultArmorModelGroup)
			{
				return defaultArmorModelGroup[slot];
			}
			return null;
		}
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x000AEA68 File Offset: 0x000ACC68
	public ArmorModelMemberMap GetArmorModelMemberMapCopy()
	{
		if (this.awake)
		{
			return this.models;
		}
		ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (!defaultArmorModelGroup)
		{
			return default(ArmorModelMemberMap);
		}
		return defaultArmorModelGroup.armorModelMemberMap;
	}

	// Token: 0x06002BB6 RID: 11190 RVA: 0x000AEAAC File Offset: 0x000ACCAC
	private bool BindArmorModel<TArmorModel>(TArmorModel model) where TArmorModel : ArmorModel, new()
	{
		if (model)
		{
			return this.BindArmorModelCheckedNonNull(model);
		}
		ArmorModel armorModel = this.defaultArmorModelGroup[ArmorModelSlotUtility.GetArmorModelSlotForClass<TArmorModel>()];
		return armorModel && this.BindArmorModelCheckedNonNull(armorModel);
	}

	// Token: 0x06002BB7 RID: 11191 RVA: 0x000AEAFC File Offset: 0x000ACCFC
	private bool BindArmorModel(ArmorModel model, ArmorModelSlot slot)
	{
		if (!model)
		{
			ArmorModel armorModel = this.defaultArmorModelGroup[slot];
			return armorModel && this.BindArmorModelCheckedNonNull(armorModel);
		}
		if (model.slot != slot)
		{
			Debug.LogError("model.slot != " + slot, model);
			return false;
		}
		return this.BindArmorModelCheckedNonNull(model);
	}

	// Token: 0x06002BB8 RID: 11192 RVA: 0x000AEB64 File Offset: 0x000ACD64
	public ArmorModelSlotMask BindArmorModels(ArmorModelMemberMap map)
	{
		if (!this.awake)
		{
			return this.Initialize(map, ArmorModelSlotMask.Feet | ArmorModelSlotMask.Legs | ArmorModelSlotMask.Torso | ArmorModelSlotMask.Head);
		}
		ArmorModelSlotMask armorModelSlotMask = (ArmorModelSlotMask)0;
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
		{
			if (this.BindArmorModel(map[armorModelSlot], armorModelSlot))
			{
				armorModelSlotMask |= armorModelSlot.ToMask();
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002BB9 RID: 11193 RVA: 0x000AEBB8 File Offset: 0x000ACDB8
	public ArmorModelSlotMask BindArmorModels(ArmorModelMemberMap map, ArmorModelSlotMask slotMask)
	{
		if (!this.awake)
		{
			return this.Initialize(map, slotMask);
		}
		ArmorModelSlotMask armorModelSlotMask = (ArmorModelSlotMask)0;
		foreach (ArmorModelSlot slot in slotMask.EnumerateSlots())
		{
			if (this.BindArmorModel(map[slot], slot))
			{
				armorModelSlotMask |= slot.ToMask();
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002BBA RID: 11194 RVA: 0x000AEC18 File Offset: 0x000ACE18
	public ArmorModelSlotMask BindArmorGroup(ArmorModelGroup group, ArmorModelSlotMask slotMask)
	{
		if (this.awake)
		{
			ArmorModelSlotMask armorModelSlotMask = (ArmorModelSlotMask)0;
			foreach (ArmorModelSlot slot in slotMask.EnumerateSlots())
			{
				ArmorModel armorModel = group[slot];
				if (armorModel && this.BindArmorModelCheckedNonNull(armorModel))
				{
					armorModelSlotMask |= slot.ToMask();
				}
			}
			return armorModelSlotMask;
		}
		if (!group)
		{
			return (ArmorModelSlotMask)0;
		}
		return this.Initialize(group.armorModelMemberMap, slotMask);
	}

	// Token: 0x06002BBB RID: 11195 RVA: 0x000AEC98 File Offset: 0x000ACE98
	public ArmorModelSlotMask BindArmorGroup(ArmorModelGroup group)
	{
		ArmorModelSlotMask armorModelSlotMask = (ArmorModelSlotMask)0;
		if (group)
		{
			for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
			{
				ArmorModel armorModel = group[armorModelSlot];
				if (armorModel && this.BindArmorModelCheckedNonNull(armorModel))
				{
					armorModelSlotMask |= armorModelSlot.ToMask();
				}
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x000AECF0 File Offset: 0x000ACEF0
	public ArmorModelSlotMask BindDefaultArmorGroup()
	{
		ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (defaultArmorModelGroup)
		{
			return this.BindArmorGroup(this.defaultArmorModelGroup);
		}
		return (ArmorModelSlotMask)0;
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x000AED20 File Offset: 0x000ACF20
	public bool Contains(ArmorModel model)
	{
		if (!model)
		{
			return false;
		}
		if (!this.awake)
		{
			ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
			return defaultArmorModelGroup && defaultArmorModelGroup[model.slot] == model;
		}
		return this.models[model.slot] == model;
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x000AED84 File Offset: 0x000ACF84
	public bool Contains<TArmorModel>(TArmorModel model) where TArmorModel : ArmorModel, new()
	{
		if (!model)
		{
			return false;
		}
		if (!this.awake)
		{
			ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
			return defaultArmorModelGroup && defaultArmorModelGroup.GetArmorModel<TArmorModel>() == model;
		}
		return this.models.GetArmorModel<TArmorModel>() == model;
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x000AEDF8 File Offset: 0x000ACFF8
	public T GetArmorModel<T>() where T : ArmorModel, new()
	{
		if (this.awake)
		{
			return this.models.GetArmorModel<T>();
		}
		ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (defaultArmorModelGroup)
		{
			return defaultArmorModelGroup.GetArmorModel<T>();
		}
		return (T)((object)null);
	}

	// Token: 0x06002BC0 RID: 11200 RVA: 0x000AEE3C File Offset: 0x000AD03C
	private ArmorModelSlotMask Initialize(ArmorModelMemberMap memberMap, ArmorModelSlotMask memberMask)
	{
		this.awake = true;
		string rendererName = ArmorModelSlot.Head.GetRendererName();
		ActorRig actorRig = this.defaultArmorModelGroup[ArmorModelSlot.Head].actorRig;
		ActorMeshRenderer actorMeshRenderer;
		if (this.originalRenderer)
		{
			actorMeshRenderer = ActorMeshRenderer.Replace(this.originalRenderer, actorRig, this.boneStructure.rigOrderedTransformArray, rendererName);
		}
		else
		{
			actorMeshRenderer = ActorMeshRenderer.CreateOn(base.transform, rendererName, actorRig, this.boneStructure.rigOrderedTransformArray, base.gameObject.layer);
		}
		this.renderers[ArmorModelSlot.Head] = actorMeshRenderer;
		for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < ArmorModelSlot.Head; armorModelSlot += 1)
		{
			this.renderers[armorModelSlot] = actorMeshRenderer.CloneBlank(armorModelSlot.GetRendererName());
		}
		for (ArmorModelSlot armorModelSlot2 = ArmorModelSlot.Feet; armorModelSlot2 < ArmorModelSlot.Head; armorModelSlot2 += 1)
		{
			ActorMeshRenderer actorMeshRenderer2 = this.renderers[armorModelSlot2];
			if (actorMeshRenderer2)
			{
				actorMeshRenderer2.renderer.enabled = base.enabled;
			}
		}
		ArmorModelSlotMask armorModelSlotMask = (ArmorModelSlotMask)0;
		ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (defaultArmorModelGroup)
		{
			ArmorModelSlot armorModelSlot3 = ArmorModelSlot.Feet;
			while (armorModelSlot3 < (ArmorModelSlot)4)
			{
				if (!memberMask.Contains(armorModelSlot3))
				{
					goto IL_14D;
				}
				ArmorModel armorModel = memberMap.GetArmorModel(armorModelSlot3);
				if (!armorModel || !this.BindArmorModelCheckedNonNull(armorModel))
				{
					goto IL_14D;
				}
				armorModelSlotMask |= armorModelSlot3.ToMask();
				IL_16D:
				armorModelSlot3 += 1;
				continue;
				IL_14D:
				ArmorModel armorModel2 = defaultArmorModelGroup[armorModelSlot3];
				if (armorModel2)
				{
					this.BindArmorModelCheckedNonNull(armorModel2);
					goto IL_16D;
				}
				goto IL_16D;
			}
		}
		else
		{
			foreach (ArmorModelSlot slot in memberMask.EnumerateSlots())
			{
				ArmorModel armorModel3 = memberMap.GetArmorModel(slot);
				if (armorModel3 && this.BindArmorModelCheckedNonNull(armorModel3))
				{
					armorModelSlotMask |= slot.ToMask();
				}
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002BC1 RID: 11201 RVA: 0x000AF024 File Offset: 0x000AD224
	private bool BindArmorModelCheckedNonNull(ArmorModel model)
	{
		ArmorModelSlot slot = model.slot;
		if (!ArmorModelRenderer.rebindingCensorship)
		{
			ArmorModel armorModel = this.models[slot];
			if (armorModel == model)
			{
				return false;
			}
		}
		ActorMeshRenderer actorMeshRenderer = this.renderers[slot];
		ArmorModel armorModel2;
		if (ArmorModelRenderer.censored)
		{
			armorModel2 = model.censoredModel;
			if (!armorModel2)
			{
				armorModel2 = model;
			}
		}
		else
		{
			armorModel2 = model;
		}
		if (actorMeshRenderer.actorRig != armorModel2.actorRig)
		{
			return false;
		}
		if (!base.enabled)
		{
			actorMeshRenderer.renderer.enabled = true;
		}
		actorMeshRenderer.Bind(armorModel2.actorMeshInfo, armorModel2.sharedMaterials);
		if (!base.enabled)
		{
			actorMeshRenderer.renderer.enabled = false;
		}
		this.models[slot] = model;
		return true;
	}

	// Token: 0x06002BC2 RID: 11202 RVA: 0x000AF0F8 File Offset: 0x000AD2F8
	private void OnDestroy()
	{
		if (!this.awake)
		{
			this.awake = true;
		}
		else
		{
			for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
			{
				ActorMeshRenderer actorMeshRenderer = this.renderers[armorModelSlot];
				if (actorMeshRenderer)
				{
					Object.Destroy(actorMeshRenderer.gameObject);
				}
			}
		}
	}

	// Token: 0x06002BC3 RID: 11203 RVA: 0x000AF154 File Offset: 0x000AD354
	private void Start()
	{
		if (!this.awake)
		{
			this.Initialize(default(ArmorModelMemberMap), (ArmorModelSlotMask)0);
		}
	}

	// Token: 0x170009AA RID: 2474
	// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000AF180 File Offset: 0x000AD380
	// (set) Token: 0x06002BC5 RID: 11205 RVA: 0x000AF188 File Offset: 0x000AD388
	public static bool Censored
	{
		get
		{
			return ArmorModelRenderer.censored;
		}
		set
		{
			if (ArmorModelRenderer.censored != value)
			{
				ArmorModelRenderer.censored = value;
				try
				{
					ArmorModelRenderer.rebindingCensorship = true;
					foreach (Object @object in Object.FindObjectsOfType(typeof(ArmorModelRenderer)))
					{
						ArmorModelRenderer armorModelRenderer = (ArmorModelRenderer)@object;
						if (armorModelRenderer)
						{
							for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
							{
								ArmorModel armorModel = armorModelRenderer[armorModelSlot];
								if (armorModel && armorModel.hasCensoredModel)
								{
									if (!armorModelRenderer.awake)
									{
										armorModelRenderer.Initialize(default(ArmorModelMemberMap), (ArmorModelSlotMask)0);
										break;
									}
									armorModelRenderer.BindArmorModelCheckedNonNull(armorModel);
								}
							}
						}
					}
					SleepingAvatar.RebindAllRenderers();
				}
				finally
				{
					ArmorModelRenderer.rebindingCensorship = false;
				}
			}
		}
	}

	// Token: 0x040017E0 RID: 6112
	[PrefetchComponent]
	[SerializeField]
	private BoneStructure boneStructure;

	// Token: 0x040017E1 RID: 6113
	[SerializeField]
	[PrefetchChildComponent]
	private SkinnedMeshRenderer originalRenderer;

	// Token: 0x040017E2 RID: 6114
	[NonSerialized]
	private ArmorModelMemberMap<ActorMeshRenderer> renderers;

	// Token: 0x040017E3 RID: 6115
	[NonSerialized]
	private ArmorModelMemberMap models;

	// Token: 0x040017E4 RID: 6116
	[NonSerialized]
	private bool awake;

	// Token: 0x040017E5 RID: 6117
	[NonSerialized]
	private CharacterArmorTrait armorTrait;

	// Token: 0x040017E6 RID: 6118
	private static bool censored = true;

	// Token: 0x040017E7 RID: 6119
	private static bool rebindingCensorship;
}
