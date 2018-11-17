using System;
using Facepunch;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x020000AE RID: 174
public sealed class ContextProbe : global::IDLocalCharacterAddon
{
	// Token: 0x060003B4 RID: 948 RVA: 0x0001197C File Offset: 0x0000FB7C
	public ContextProbe() : this(global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | global::IDLocalCharacterAddon.AddonFlags.FireOnWillRemoveAddon)
	{
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00011988 File Offset: 0x0000FB88
	private ContextProbe(global::IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060003B6 RID: 950 RVA: 0x00011994 File Offset: 0x0000FB94
	public bool isHighlighting
	{
		get
		{
			return this.hasHighlight;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001199C File Offset: 0x0000FB9C
	public static bool aProbeIsHighlighting
	{
		get
		{
			return global::ContextProbe.singleton && global::ContextProbe.singleton.hasHighlight;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000119BC File Offset: 0x0000FBBC
	protected override void OnAddonAwake()
	{
		global::ContextProbe.singleton = this;
		global::CharacterContextProbeTrait trait = base.GetTrait<global::CharacterContextProbeTrait>();
		this.raycastLength = trait.rayLength;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000119E4 File Offset: 0x0000FBE4
	protected override void OnWillRemoveAddon()
	{
		if (global::ContextProbe.singleton == this)
		{
			global::ContextProbe.singleton = null;
		}
		this.hasHighlight = false;
		this.lastUseHighlight = null;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00011A18 File Offset: 0x0000FC18
	private void Update()
	{
		if (base.dead)
		{
			return;
		}
		bool press;
		if (LockCursorManager.IsLocked())
		{
			press = global::Context.ButtonDown;
			bool buttonUp = global::Context.ButtonUp;
		}
		else
		{
			press = false;
			bool flag = global::Context.WorkingInMenu && global::Context.ButtonUp;
		}
		this.hasHighlight = this.ClientCheckUse(base.eyesRay, press);
		if (global::Context.ButtonUp)
		{
			global::Context.EndQuery();
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00011A84 File Offset: 0x0000FC84
	private bool ClientCheckUse(Ray ray, bool press)
	{
		RaycastHit raycastHit;
		MonoBehaviour monoBehaviour;
		if (Physics.Raycast(ray, ref raycastHit, this.raycastLength, -201523205))
		{
			Transform transform = raycastHit.transform;
			Transform parent = transform.parent;
			global::NetEntityID netEntityID;
			MonoBehaviour component;
			global::NetEntityID.Kind kind;
			while ((int)(kind = global::NetEntityID.Of(transform, out netEntityID, out component)) == 0 && parent)
			{
				transform = parent;
				parent = transform.parent;
			}
			global::Contextual contextual;
			if ((int)kind == 0)
			{
				monoBehaviour = null;
			}
			else if (global::Contextual.ContextOf(component, out contextual))
			{
				monoBehaviour = contextual.implementor;
				if (press)
				{
					global::Context.BeginQuery(contextual);
				}
			}
			else
			{
				monoBehaviour = null;
			}
		}
		else
		{
			monoBehaviour = null;
		}
		if (monoBehaviour != this.lastUseHighlight)
		{
			this.lastUseHighlight = monoBehaviour;
			if (monoBehaviour)
			{
				global::IContextRequestableText contextRequestableText = monoBehaviour as global::IContextRequestableText;
				if (contextRequestableText != null)
				{
					global::RPOS.UseHoverTextSet(base.controllable, contextRequestableText);
				}
				else
				{
					global::RPOS.UseHoverTextSet(monoBehaviour.name);
				}
			}
			else
			{
				global::RPOS.UseHoverTextClear();
			}
		}
		return monoBehaviour;
	}

	// Token: 0x04000322 RID: 802
	private const global::IDLocalCharacterAddon.AddonFlags ContextProbeAddonFlags = global::IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | global::IDLocalCharacterAddon.AddonFlags.FireOnWillRemoveAddon;

	// Token: 0x04000323 RID: 803
	[NonSerialized]
	private float raycastLength;

	// Token: 0x04000324 RID: 804
	[NonSerialized]
	private MonoBehaviour lastUseHighlight;

	// Token: 0x04000325 RID: 805
	[NonSerialized]
	private bool hasHighlight;

	// Token: 0x04000326 RID: 806
	private static global::ContextProbe singleton;
}
