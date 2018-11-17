using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public sealed class ContextActivator : MonoBehaviour, IContextRequestable, IContextRequestableQuick, IContextRequestableStatus, IContextRequestableText, IContextRequestablePointText, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060028D4 RID: 10452 RVA: 0x000A0104 File Offset: 0x0009E304
	bool IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		if (this.useTextPoint)
		{
			if (!this.useSpriteTextPoint || !ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint))
			{
				if (this.isSwitch)
				{
					ActivationToggleState toggleState = this.toggleState;
					if (toggleState != ActivationToggleState.On)
					{
						if (toggleState != ActivationToggleState.Off)
						{
							worldPoint = this.defaultTextPoint;
						}
						else
						{
							worldPoint = this.offTextPoint;
						}
					}
					else
					{
						worldPoint = this.onTextPoint;
					}
				}
				else
				{
					worldPoint = this.defaultTextPoint;
				}
				worldPoint = base.transform.TransformPoint(worldPoint);
			}
			return true;
		}
		worldPoint = default(Vector3);
		return false;
	}

	// Token: 0x060028D5 RID: 10453 RVA: 0x000A01C8 File Offset: 0x0009E3C8
	string IContextRequestableText.ContextText(Controllable localControllable)
	{
		if (this.isSwitch)
		{
			ActivationToggleState toggleState = this.toggleState;
			if (toggleState == ActivationToggleState.On)
			{
				return this.onText;
			}
			if (toggleState == ActivationToggleState.Off)
			{
				return this.offText;
			}
		}
		return this.defaultText;
	}

	// Token: 0x060028D6 RID: 10454 RVA: 0x000A0210 File Offset: 0x0009E410
	ContextStatusFlags IContextRequestableStatus.ContextStatusPoll()
	{
		ContextActivator.SpriteQuickMode spriteQuickMode;
		if (this.isSwitch)
		{
			ActivationToggleState toggleState = this.toggleState;
			if (toggleState != ActivationToggleState.On)
			{
				if (toggleState != ActivationToggleState.Off)
				{
					spriteQuickMode = this.defaultSprite;
				}
				else
				{
					spriteQuickMode = this.offSprite;
				}
			}
			else
			{
				spriteQuickMode = this.onSprite;
			}
		}
		else
		{
			spriteQuickMode = this.defaultSprite;
		}
		switch (spriteQuickMode)
		{
		case ContextActivator.SpriteQuickMode.Faded:
			return ContextStatusFlags.SpriteFlag0;
		case ContextActivator.SpriteQuickMode.AlwaysVisible:
			return ContextStatusFlags.SpriteFlag0 | ContextStatusFlags.SpriteFlag1;
		case ContextActivator.SpriteQuickMode.NeverVisisble:
			return ContextStatusFlags.SpriteFlag1;
		default:
			return (ContextStatusFlags)0;
		}
	}

	// Token: 0x1700094A RID: 2378
	// (get) Token: 0x060028D7 RID: 10455 RVA: 0x000A02A0 File Offset: 0x0009E4A0
	private ActivationToggleState toggleState
	{
		get
		{
			if (!this.mainAction)
			{
				return ActivationToggleState.Unspecified;
			}
			return this.mainAction.toggleState;
		}
	}

	// Token: 0x060028D8 RID: 10456 RVA: 0x000A02C0 File Offset: 0x0009E4C0
	private ActivationResult ApplyActivatable(Activatable activatable, Character instigator, ulong timestamp, bool extra)
	{
		ActivationResult result;
		if (activatable)
		{
			ContextActivator.ActivationMode activationMode = this.activationMode;
			if (activationMode != ContextActivator.ActivationMode.TurnOn)
			{
				if (activationMode != ContextActivator.ActivationMode.TurnOff)
				{
					result = activatable.Activate(instigator, timestamp);
				}
				else
				{
					result = activatable.Activate(false, instigator, timestamp);
				}
			}
			else
			{
				result = activatable.Activate(true, instigator, timestamp);
			}
		}
		else
		{
			result = ActivationResult.Error_Destroyed;
		}
		return result;
	}

	// Token: 0x040014BE RID: 5310
	[SerializeField]
	private Activatable mainAction;

	// Token: 0x040014BF RID: 5311
	[SerializeField]
	private ContextActivator.ActivationMode activationMode;

	// Token: 0x040014C0 RID: 5312
	[SerializeField]
	private Activatable[] extraActions;

	// Token: 0x040014C1 RID: 5313
	[SerializeField]
	private string defaultText;

	// Token: 0x040014C2 RID: 5314
	[SerializeField]
	private string onText;

	// Token: 0x040014C3 RID: 5315
	[SerializeField]
	private string offText;

	// Token: 0x040014C4 RID: 5316
	[SerializeField]
	private Vector3 defaultTextPoint;

	// Token: 0x040014C5 RID: 5317
	[SerializeField]
	private Vector3 onTextPoint;

	// Token: 0x040014C6 RID: 5318
	[SerializeField]
	private Vector3 offTextPoint;

	// Token: 0x040014C7 RID: 5319
	[SerializeField]
	private bool useTextPoint;

	// Token: 0x040014C8 RID: 5320
	[SerializeField]
	private bool useSpriteTextPoint;

	// Token: 0x040014C9 RID: 5321
	[SerializeField]
	private ContextActivator.SpriteQuickMode defaultSprite;

	// Token: 0x040014CA RID: 5322
	[SerializeField]
	private ContextActivator.SpriteQuickMode onSprite;

	// Token: 0x040014CB RID: 5323
	[SerializeField]
	private ContextActivator.SpriteQuickMode offSprite;

	// Token: 0x040014CC RID: 5324
	[SerializeField]
	private bool isSwitch;

	// Token: 0x040014CD RID: 5325
	private bool isToggle;

	// Token: 0x02000462 RID: 1122
	private enum SpriteQuickMode
	{
		// Token: 0x040014CF RID: 5327
		Default,
		// Token: 0x040014D0 RID: 5328
		Faded,
		// Token: 0x040014D1 RID: 5329
		AlwaysVisible,
		// Token: 0x040014D2 RID: 5330
		NeverVisisble
	}

	// Token: 0x02000463 RID: 1123
	private enum ActivationMode
	{
		// Token: 0x040014D4 RID: 5332
		ActivateOrToggle,
		// Token: 0x040014D5 RID: 5333
		TurnOn,
		// Token: 0x040014D6 RID: 5334
		TurnOff
	}
}
