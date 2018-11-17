using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class LocalDamageDisplay : IDLocalCharacterAddon
{
	// Token: 0x06002978 RID: 10616 RVA: 0x000A29F8 File Offset: 0x000A0BF8
	public LocalDamageDisplay() : this(IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake)
	{
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x000A2A04 File Offset: 0x000A0C04
	protected LocalDamageDisplay(IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags | IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake)
	{
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x000A2A1C File Offset: 0x000A0C1C
	private void Update()
	{
		if (DebugInput.GetKeyDown(111))
		{
			LocalDamageDisplay.adminObjectShow = !LocalDamageDisplay.adminObjectShow;
			if (LocalDamageDisplay.adminObjectShow)
			{
				Debug.Log("shown object overlay", this);
			}
			else
			{
				Debug.Log("hid object overlay", this);
			}
		}
		if (LocalDamageDisplay.adminObjectShow && DebugInput.GetKeyDown(108))
		{
			LocalDamageDisplay.mode = (LocalDamageDisplay.mode + 1) % 2;
		}
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x000A2A8C File Offset: 0x000A0C8C
	public void SetNewHealthPercent(float newHealthPercent, GameObject attacker)
	{
		if (newHealthPercent < this.lastHealthPercent)
		{
			this.Hurt(newHealthPercent, attacker);
		}
		this.lastHealthPercent = newHealthPercent;
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x000A2AAC File Offset: 0x000A0CAC
	public void Hurt(float percent, GameObject attacker)
	{
		if (percent < 0.05f)
		{
			return;
		}
		this.lastTakeDamageTime = Time.time;
		if (CameraMount.current == null)
		{
			return;
		}
		HeadBob component = CameraMount.current.GetComponent<HeadBob>();
		if (component == null)
		{
			Debug.Log("no camera headbob");
		}
		if (component)
		{
			bool flag;
			if (attacker)
			{
				Controllable component2 = attacker.GetComponent<Controllable>();
				flag = (component2 && component2.npcName == "zombie");
				if (!flag)
				{
					flag = (attacker.GetComponent<BasicWildLifeAI>() != null);
				}
			}
			else
			{
				flag = false;
			}
			component.AddEffect((!flag) ? this.takeDamageBob : this.meleeBob);
		}
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x000A2B78 File Offset: 0x000A0D78
	private int UpdateFadeValues(out float alpha, out float impactAlpha)
	{
		alpha = 1f - this.lastHealthPercent;
		float num = Mathf.Abs(Mathf.Sin(Time.time * 6f));
		int num2 = 0;
		if (this.lastHealthPercent <= 0.6f && alpha > 0f)
		{
			num2 |= 1;
			alpha = (alpha - 0.6f) * 2.5f * num;
		}
		impactAlpha = 1f - Mathf.Clamp01((Time.time - this.lastTakeDamageTime) / 0.5f);
		impactAlpha *= 1f;
		if (impactAlpha > 0f)
		{
			num2 |= 2;
		}
		return num2;
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x000A2C18 File Offset: 0x000A0E18
	private void LateUpdate()
	{
		GameFullscreen instance = ImageEffectManager.GetInstance<GameFullscreen>();
		float alpha;
		float alpha2;
		int num = this.UpdateFadeValues(out alpha, out alpha2);
		int num2 = num ^ this.lastShowFlags;
		this.lastShowFlags = num;
		if (num2 != 0)
		{
			if ((num2 & 1) == 1)
			{
				if ((num & 1) == 1)
				{
					instance.overlays[0].texture = this.damageOverlay;
					instance.overlays[0].pass = 3;
				}
				else
				{
					instance.overlays[0].texture = null;
				}
			}
			if ((num2 & 2) == 2)
			{
				if ((num & 2) == 2)
				{
					instance.overlays[1].texture = this.damageOverlay2;
					instance.overlays[1].pass = 3;
				}
				else
				{
					instance.overlays[1].texture = null;
				}
			}
		}
		if ((num & 1) == 1)
		{
			instance.overlays[0].alpha = alpha;
		}
		if ((num & 2) == 2)
		{
			instance.overlays[1].alpha = alpha2;
		}
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x000A2D2C File Offset: 0x000A0F2C
	private void OnDisable()
	{
		GameFullscreen instance = ImageEffectManager.GetInstance<GameFullscreen>();
		int num = this.lastShowFlags;
		this.lastShowFlags = 0;
		if ((num & 1) == 1)
		{
			instance.overlays[0].texture = null;
		}
		if ((num & 2) == 2)
		{
			instance.overlays[1].texture = null;
		}
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x000A2D84 File Offset: 0x000A0F84
	private static void DrawLabel(Vector3 point, string label)
	{
		Vector3? vector = CameraFX.World2Screen(point);
		if (vector != null)
		{
			Vector3 value = vector.Value;
			if (value.z > 0f)
			{
				Vector2 vector2 = GUIUtility.ScreenToGUIPoint(value);
				vector2.y = (float)Screen.height - (vector2.y + 1f);
				GUI.color = Color.white;
				GUI.Label(new Rect(vector2.x - 64f, vector2.y - 12f, 128f, 24f), label);
			}
		}
	}

	// Token: 0x06002981 RID: 10625 RVA: 0x000A2E20 File Offset: 0x000A1020
	private void OnGUI()
	{
		if (Event.current.type != 7)
		{
			return;
		}
		if (LocalDamageDisplay.adminObjectShow)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(5f, 5f, 128f, 24f), (LocalDamageDisplay.mode != 0) ? "showing selection" : "showing characters");
			if (LocalDamageDisplay.mode == 0)
			{
				foreach (Object @object in Object.FindObjectsOfType(typeof(Character)))
				{
					if (@object)
					{
						Character character = (Character)@object;
						if (!(character.gameObject == this))
						{
							LocalDamageDisplay.DrawLabel(character.origin, character.name);
						}
					}
				}
			}
		}
	}

	// Token: 0x06002982 RID: 10626 RVA: 0x000A2EF4 File Offset: 0x000A10F4
	protected override void OnAddonAwake()
	{
		CharacterOverlayTrait trait = base.GetTrait<CharacterOverlayTrait>();
		this.damageOverlay = trait.damageOverlay;
		this.damageOverlay2 = trait.damageOverlay2;
		this.takeDamageBob = (trait.takeDamageBob as BobEffect);
		this.meleeBob = (trait.meleeBob as BobEffect);
	}

	// Token: 0x0400155C RID: 5468
	private const int SHOW_DAMAGE_OVERLAY = 1;

	// Token: 0x0400155D RID: 5469
	private const int SHOW_IMPACT_OVERLAY = 2;

	// Token: 0x0400155E RID: 5470
	private const int kDamageOverlayIndex = 0;

	// Token: 0x0400155F RID: 5471
	private const int kImpactOverlayIndex = 1;

	// Token: 0x04001560 RID: 5472
	private const int kDamageOverlayPass = 3;

	// Token: 0x04001561 RID: 5473
	private const int kImpactOverlayPass = 1;

	// Token: 0x04001562 RID: 5474
	private const int mode_count = 2;

	// Token: 0x04001563 RID: 5475
	protected const IDLocalCharacterAddon.AddonFlags kRequiredAddonFlags = IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake;

	// Token: 0x04001564 RID: 5476
	[NonSerialized]
	public Texture2D damageOverlay;

	// Token: 0x04001565 RID: 5477
	[NonSerialized]
	public Texture2D damageOverlay2;

	// Token: 0x04001566 RID: 5478
	[NonSerialized]
	public float lastHealthPercent = 1f;

	// Token: 0x04001567 RID: 5479
	[NonSerialized]
	public BobEffect takeDamageBob;

	// Token: 0x04001568 RID: 5480
	[NonSerialized]
	public BobEffect meleeBob;

	// Token: 0x04001569 RID: 5481
	[NonSerialized]
	public float lastTakeDamageTime;

	// Token: 0x0400156A RID: 5482
	private int lastShowFlags;

	// Token: 0x0400156B RID: 5483
	private static bool adminObjectShow;

	// Token: 0x0400156C RID: 5484
	private static int mode;
}
