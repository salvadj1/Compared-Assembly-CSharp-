using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x020005C9 RID: 1481
public sealed class ArmorModelRenderer : global::IDLocalCharacter
{
	// Token: 0x17000A1B RID: 2587
	// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000B6998 File Offset: 0x000B4B98
	public global::ArmorModelGroup defaultArmorModelGroup
	{
		get
		{
			return ((!this.armorTrait) ? (this.armorTrait = base.GetTrait<global::CharacterArmorTrait>()) : this.armorTrait).defaultGroup;
		}
	}

	// Token: 0x06002F71 RID: 12145 RVA: 0x000B69D4 File Offset: 0x000B4BD4
	private void OnEnable()
	{
		if (this.awake)
		{
			for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
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

	// Token: 0x06002F72 RID: 12146 RVA: 0x000B6A44 File Offset: 0x000B4C44
	private void OnDisable()
	{
		if (this.awake)
		{
			for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
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

	// Token: 0x17000A1C RID: 2588
	// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000B6AB4 File Offset: 0x000B4CB4
	public ActorRig actorRig
	{
		get
		{
			return this.boneStructure.actorRig;
		}
	}

	// Token: 0x17000A1D RID: 2589
	public global::ArmorModel this[global::ArmorModelSlot slot]
	{
		get
		{
			if (this.awake)
			{
				return this.models[slot];
			}
			global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
			if (defaultArmorModelGroup)
			{
				return defaultArmorModelGroup[slot];
			}
			return null;
		}
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x000B6B04 File Offset: 0x000B4D04
	public global::ArmorModelMemberMap GetArmorModelMemberMapCopy()
	{
		if (this.awake)
		{
			return this.models;
		}
		global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (!defaultArmorModelGroup)
		{
			return default(global::ArmorModelMemberMap);
		}
		return defaultArmorModelGroup.armorModelMemberMap;
	}

	// Token: 0x06002F76 RID: 12150 RVA: 0x000B6B48 File Offset: 0x000B4D48
	private bool BindArmorModel<TArmorModel>(TArmorModel model) where TArmorModel : global::ArmorModel, new()
	{
		if (model)
		{
			return this.BindArmorModelCheckedNonNull(model);
		}
		global::ArmorModel armorModel = this.defaultArmorModelGroup[global::ArmorModelSlotUtility.GetArmorModelSlotForClass<TArmorModel>()];
		return armorModel && this.BindArmorModelCheckedNonNull(armorModel);
	}

	// Token: 0x06002F77 RID: 12151 RVA: 0x000B6B98 File Offset: 0x000B4D98
	private bool BindArmorModel(global::ArmorModel model, global::ArmorModelSlot slot)
	{
		if (!model)
		{
			global::ArmorModel armorModel = this.defaultArmorModelGroup[slot];
			return armorModel && this.BindArmorModelCheckedNonNull(armorModel);
		}
		if (model.slot != slot)
		{
			Debug.LogError("model.slot != " + slot, model);
			return false;
		}
		return this.BindArmorModelCheckedNonNull(model);
	}

	// Token: 0x06002F78 RID: 12152 RVA: 0x000B6C00 File Offset: 0x000B4E00
	public global::ArmorModelSlotMask BindArmorModels(global::ArmorModelMemberMap map)
	{
		if (!this.awake)
		{
			return this.Initialize(map, global::ArmorModelSlotMask.Feet | global::ArmorModelSlotMask.Legs | global::ArmorModelSlotMask.Torso | global::ArmorModelSlotMask.Head);
		}
		global::ArmorModelSlotMask armorModelSlotMask = (global::ArmorModelSlotMask)0;
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
		{
			if (this.BindArmorModel(map[armorModelSlot], armorModelSlot))
			{
				armorModelSlotMask |= armorModelSlot.ToMask();
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002F79 RID: 12153 RVA: 0x000B6C54 File Offset: 0x000B4E54
	public global::ArmorModelSlotMask BindArmorModels(global::ArmorModelMemberMap map, global::ArmorModelSlotMask slotMask)
	{
		if (!this.awake)
		{
			return this.Initialize(map, slotMask);
		}
		global::ArmorModelSlotMask armorModelSlotMask = (global::ArmorModelSlotMask)0;
		foreach (global::ArmorModelSlot slot in slotMask.EnumerateSlots())
		{
			if (this.BindArmorModel(map[slot], slot))
			{
				armorModelSlotMask |= slot.ToMask();
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x000B6CB4 File Offset: 0x000B4EB4
	public global::ArmorModelSlotMask BindArmorGroup(global::ArmorModelGroup group, global::ArmorModelSlotMask slotMask)
	{
		if (this.awake)
		{
			global::ArmorModelSlotMask armorModelSlotMask = (global::ArmorModelSlotMask)0;
			foreach (global::ArmorModelSlot slot in slotMask.EnumerateSlots())
			{
				global::ArmorModel armorModel = group[slot];
				if (armorModel && this.BindArmorModelCheckedNonNull(armorModel))
				{
					armorModelSlotMask |= slot.ToMask();
				}
			}
			return armorModelSlotMask;
		}
		if (!group)
		{
			return (global::ArmorModelSlotMask)0;
		}
		return this.Initialize(group.armorModelMemberMap, slotMask);
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x000B6D34 File Offset: 0x000B4F34
	public global::ArmorModelSlotMask BindArmorGroup(global::ArmorModelGroup group)
	{
		global::ArmorModelSlotMask armorModelSlotMask = (global::ArmorModelSlotMask)0;
		if (group)
		{
			for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
			{
				global::ArmorModel armorModel = group[armorModelSlot];
				if (armorModel && this.BindArmorModelCheckedNonNull(armorModel))
				{
					armorModelSlotMask |= armorModelSlot.ToMask();
				}
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002F7C RID: 12156 RVA: 0x000B6D8C File Offset: 0x000B4F8C
	public global::ArmorModelSlotMask BindDefaultArmorGroup()
	{
		global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (defaultArmorModelGroup)
		{
			return this.BindArmorGroup(this.defaultArmorModelGroup);
		}
		return (global::ArmorModelSlotMask)0;
	}

	// Token: 0x06002F7D RID: 12157 RVA: 0x000B6DBC File Offset: 0x000B4FBC
	public bool Contains(global::ArmorModel model)
	{
		if (!model)
		{
			return false;
		}
		if (!this.awake)
		{
			global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
			return defaultArmorModelGroup && defaultArmorModelGroup[model.slot] == model;
		}
		return this.models[model.slot] == model;
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x000B6E20 File Offset: 0x000B5020
	public bool Contains<TArmorModel>(TArmorModel model) where TArmorModel : global::ArmorModel, new()
	{
		if (!model)
		{
			return false;
		}
		if (!this.awake)
		{
			global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
			return defaultArmorModelGroup && defaultArmorModelGroup.GetArmorModel<TArmorModel>() == model;
		}
		return this.models.GetArmorModel<TArmorModel>() == model;
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x000B6E94 File Offset: 0x000B5094
	public T GetArmorModel<T>() where T : global::ArmorModel, new()
	{
		if (this.awake)
		{
			return this.models.GetArmorModel<T>();
		}
		global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (defaultArmorModelGroup)
		{
			return defaultArmorModelGroup.GetArmorModel<T>();
		}
		return (T)((object)null);
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x000B6ED8 File Offset: 0x000B50D8
	private global::ArmorModelSlotMask Initialize(global::ArmorModelMemberMap memberMap, global::ArmorModelSlotMask memberMask)
	{
		this.awake = true;
		string rendererName = global::ArmorModelSlot.Head.GetRendererName();
		ActorRig actorRig = this.defaultArmorModelGroup[global::ArmorModelSlot.Head].actorRig;
		ActorMeshRenderer actorMeshRenderer;
		if (this.originalRenderer)
		{
			actorMeshRenderer = ActorMeshRenderer.Replace(this.originalRenderer, actorRig, this.boneStructure.rigOrderedTransformArray, rendererName);
		}
		else
		{
			actorMeshRenderer = ActorMeshRenderer.CreateOn(base.transform, rendererName, actorRig, this.boneStructure.rigOrderedTransformArray, base.gameObject.layer);
		}
		this.renderers[global::ArmorModelSlot.Head] = actorMeshRenderer;
		for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < global::ArmorModelSlot.Head; armorModelSlot += 1)
		{
			this.renderers[armorModelSlot] = actorMeshRenderer.CloneBlank(armorModelSlot.GetRendererName());
		}
		for (global::ArmorModelSlot armorModelSlot2 = global::ArmorModelSlot.Feet; armorModelSlot2 < global::ArmorModelSlot.Head; armorModelSlot2 += 1)
		{
			ActorMeshRenderer actorMeshRenderer2 = this.renderers[armorModelSlot2];
			if (actorMeshRenderer2)
			{
				actorMeshRenderer2.renderer.enabled = base.enabled;
			}
		}
		global::ArmorModelSlotMask armorModelSlotMask = (global::ArmorModelSlotMask)0;
		global::ArmorModelGroup defaultArmorModelGroup = this.defaultArmorModelGroup;
		if (defaultArmorModelGroup)
		{
			global::ArmorModelSlot armorModelSlot3 = global::ArmorModelSlot.Feet;
			while (armorModelSlot3 < (global::ArmorModelSlot)4)
			{
				if (!memberMask.Contains(armorModelSlot3))
				{
					goto IL_14D;
				}
				global::ArmorModel armorModel = memberMap.GetArmorModel(armorModelSlot3);
				if (!armorModel || !this.BindArmorModelCheckedNonNull(armorModel))
				{
					goto IL_14D;
				}
				armorModelSlotMask |= armorModelSlot3.ToMask();
				IL_16D:
				armorModelSlot3 += 1;
				continue;
				IL_14D:
				global::ArmorModel armorModel2 = defaultArmorModelGroup[armorModelSlot3];
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
			foreach (global::ArmorModelSlot slot in memberMask.EnumerateSlots())
			{
				global::ArmorModel armorModel3 = memberMap.GetArmorModel(slot);
				if (armorModel3 && this.BindArmorModelCheckedNonNull(armorModel3))
				{
					armorModelSlotMask |= slot.ToMask();
				}
			}
		}
		return armorModelSlotMask;
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x000B70C0 File Offset: 0x000B52C0
	private bool BindArmorModelCheckedNonNull(global::ArmorModel model)
	{
		global::ArmorModelSlot slot = model.slot;
		if (!global::ArmorModelRenderer.rebindingCensorship)
		{
			global::ArmorModel armorModel = this.models[slot];
			if (armorModel == model)
			{
				return false;
			}
		}
		ActorMeshRenderer actorMeshRenderer = this.renderers[slot];
		global::ArmorModel armorModel2;
		if (global::ArmorModelRenderer.censored)
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

	// Token: 0x06002F82 RID: 12162 RVA: 0x000B7194 File Offset: 0x000B5394
	private void OnDestroy()
	{
		if (!this.awake)
		{
			this.awake = true;
		}
		else
		{
			for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
			{
				ActorMeshRenderer actorMeshRenderer = this.renderers[armorModelSlot];
				if (actorMeshRenderer)
				{
					Object.Destroy(actorMeshRenderer.gameObject);
				}
			}
		}
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x000B71F0 File Offset: 0x000B53F0
	private void Start()
	{
		if (!this.awake)
		{
			this.Initialize(default(global::ArmorModelMemberMap), (global::ArmorModelSlotMask)0);
		}
	}

	// Token: 0x17000A1E RID: 2590
	// (get) Token: 0x06002F84 RID: 12164 RVA: 0x000B721C File Offset: 0x000B541C
	// (set) Token: 0x06002F85 RID: 12165 RVA: 0x000B7224 File Offset: 0x000B5424
	public static bool Censored
	{
		get
		{
			return global::ArmorModelRenderer.censored;
		}
		set
		{
			if (global::ArmorModelRenderer.censored != value)
			{
				global::ArmorModelRenderer.censored = value;
				try
				{
					global::ArmorModelRenderer.rebindingCensorship = true;
					foreach (Object @object in Object.FindObjectsOfType(typeof(global::ArmorModelRenderer)))
					{
						global::ArmorModelRenderer armorModelRenderer = (global::ArmorModelRenderer)@object;
						if (armorModelRenderer)
						{
							for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
							{
								global::ArmorModel armorModel = armorModelRenderer[armorModelSlot];
								if (armorModel && armorModel.hasCensoredModel)
								{
									if (!armorModelRenderer.awake)
									{
										armorModelRenderer.Initialize(default(global::ArmorModelMemberMap), (global::ArmorModelSlotMask)0);
										break;
									}
									armorModelRenderer.BindArmorModelCheckedNonNull(armorModel);
								}
							}
						}
					}
					global::SleepingAvatar.RebindAllRenderers();
				}
				finally
				{
					global::ArmorModelRenderer.rebindingCensorship = false;
				}
			}
		}
	}

	// Token: 0x040019AC RID: 6572
	[PrefetchComponent]
	[SerializeField]
	private BoneStructure boneStructure;

	// Token: 0x040019AD RID: 6573
	[SerializeField]
	[PrefetchChildComponent]
	private SkinnedMeshRenderer originalRenderer;

	// Token: 0x040019AE RID: 6574
	[NonSerialized]
	private global::ArmorModelMemberMap<ActorMeshRenderer> renderers;

	// Token: 0x040019AF RID: 6575
	[NonSerialized]
	private global::ArmorModelMemberMap models;

	// Token: 0x040019B0 RID: 6576
	[NonSerialized]
	private bool awake;

	// Token: 0x040019B1 RID: 6577
	[NonSerialized]
	private global::CharacterArmorTrait armorTrait;

	// Token: 0x040019B2 RID: 6578
	private static bool censored = true;

	// Token: 0x040019B3 RID: 6579
	private static bool rebindingCensorship;
}
