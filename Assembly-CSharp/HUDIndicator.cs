using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004BC RID: 1212
public abstract class HUDIndicator : MonoBehaviour
{
	// Token: 0x17000968 RID: 2408
	// (get) Token: 0x06002A4A RID: 10826 RVA: 0x000A6118 File Offset: 0x000A4318
	protected static double stepTime
	{
		get
		{
			return HUDIndicator._stepTime;
		}
	}

	// Token: 0x17000969 RID: 2409
	// (get) Token: 0x06002A4B RID: 10827 RVA: 0x000A6120 File Offset: 0x000A4320
	protected static double time
	{
		get
		{
			return (!DebugInput.GetKey(46)) ? (HUDIndicator._lastTime = NetCull.time) : HUDIndicator._lastTime;
		}
	}

	// Token: 0x06002A4C RID: 10828
	protected abstract bool Continue();

	// Token: 0x06002A4D RID: 10829 RVA: 0x000A6144 File Offset: 0x000A4344
	protected Vector3 GetPoint(HUDIndicator.PlacementSpace space, Vector3 input)
	{
		switch (space)
		{
		case HUDIndicator.PlacementSpace.World:
		{
			Camera camera = HUDIndicator.Target.camera;
			Vector3? vector = CameraFX.World2Screen(input);
			input = camera.ScreenToWorldPoint((vector == null) ? Vector3.zero : vector.Value);
			break;
		}
		case HUDIndicator.PlacementSpace.Screen:
			input = HUDIndicator.Target.camera.ScreenToWorldPoint(input);
			break;
		case HUDIndicator.PlacementSpace.Viewport:
			input = HUDIndicator.Target.camera.ViewportToWorldPoint(input);
			break;
		case HUDIndicator.PlacementSpace.Anchor:
			input = this.anchor.transform.TransformPoint(input);
			break;
		}
		return input;
	}

	// Token: 0x06002A4E RID: 10830 RVA: 0x000A61E8 File Offset: 0x000A43E8
	private static HUDIndicator InstantiateIndicator(ref HUDIndicator.Target target, HUDIndicator prefab, HUDIndicator.PlacementSpace space, Vector3 position, float rotation)
	{
		UIAnchor uianchor = target.anchor;
		Quaternion quaternion = Quaternion.AngleAxis(rotation, Vector3.back);
		switch (space)
		{
		case HUDIndicator.PlacementSpace.World:
		{
			Camera camera = HUDIndicator.Target.camera;
			Vector3? vector = CameraFX.World2Screen(position);
			position = camera.ScreenToWorldPoint((vector == null) ? Vector3.zero : vector.Value);
			break;
		}
		case HUDIndicator.PlacementSpace.Screen:
			position = HUDIndicator.Target.camera.ScreenToWorldPoint(position);
			break;
		case HUDIndicator.PlacementSpace.Viewport:
			position = HUDIndicator.Target.camera.ViewportToWorldPoint(position);
			break;
		case HUDIndicator.PlacementSpace.Anchor:
			position = uianchor.transform.TransformPoint(position);
			quaternion = uianchor.transform.rotation * quaternion;
			break;
		}
		position.z = uianchor.transform.position.z;
		HUDIndicator hudindicator = (HUDIndicator)Object.Instantiate(prefab, position, quaternion);
		hudindicator.transform.parent = uianchor.transform;
		hudindicator.transform.localScale = Vector3.one;
		hudindicator.anchor = target.anchor;
		return hudindicator;
	}

	// Token: 0x06002A4F RID: 10831 RVA: 0x000A6304 File Offset: 0x000A4504
	protected static HUDIndicator InstantiateIndicator(HUDIndicator.ScratchTarget target, HUDIndicator prefab, HUDIndicator.PlacementSpace space, Vector3 position, float rotation)
	{
		if (target == HUDIndicator.ScratchTarget.CenteredAuto)
		{
			return HUDIndicator.InstantiateIndicator(ref HUDIndicator.CenterAuto, prefab, space, position, rotation);
		}
		if (target != HUDIndicator.ScratchTarget.CenteredFixed3000Tall)
		{
			throw new ArgumentOutOfRangeException("target");
		}
		return HUDIndicator.InstantiateIndicator(ref HUDIndicator.CenterFixed3000Tall, prefab, space, position, rotation);
	}

	// Token: 0x06002A50 RID: 10832 RVA: 0x000A6350 File Offset: 0x000A4550
	protected static HUDIndicator InstantiateIndicator(HUDIndicator.ScratchTarget target, HUDIndicator prefab, HUDIndicator.PlacementSpace space, Vector3 position)
	{
		return HUDIndicator.InstantiateIndicator(target, prefab, space, position, 0f);
	}

	// Token: 0x06002A51 RID: 10833 RVA: 0x000A6360 File Offset: 0x000A4560
	protected static HUDIndicator InstantiateIndicator(HUDIndicator.ScratchTarget target, HUDIndicator prefab)
	{
		return HUDIndicator.InstantiateIndicator(target, prefab, HUDIndicator.PlacementSpace.Anchor, Vector3.zero, 0f);
	}

	// Token: 0x06002A52 RID: 10834 RVA: 0x000A6374 File Offset: 0x000A4574
	protected void Start()
	{
		if (!this.Continue())
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			HUDIndicator.INDICATOR.Add(this);
		}
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x000A6398 File Offset: 0x000A4598
	protected void OnDestroy()
	{
		if (this.listIndex != -1)
		{
			HUDIndicator.INDICATOR.Remove(this);
		}
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x000A63AC File Offset: 0x000A45AC
	internal static void Step()
	{
		HUDIndicator._stepTime = HUDIndicator.time;
		Camera main = Camera.main;
		if (main)
		{
			HUDIndicator.worldToCameraLocalMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * main.worldToCameraMatrix;
		}
		int num = HUDIndicator.INDICATOR.activeIndicators.Count;
		for (int i = num - 1; i >= 0; i--)
		{
			if (!HUDIndicator.INDICATOR.activeIndicators[i].Continue())
			{
				int j = i;
				do
				{
					HUDIndicator hudindicator = HUDIndicator.INDICATOR.activeIndicators[i];
					HUDIndicator.INDICATOR.activeIndicators.RemoveAt(i);
					hudindicator.listIndex = -1;
					Object.Destroy(hudindicator.gameObject);
					num--;
					if (--i >= 0)
					{
						while (HUDIndicator.INDICATOR.activeIndicators[i].Continue())
						{
							if (--i < 0)
							{
								goto IL_D6;
							}
						}
						j = i;
					}
					IL_D6:;
				}
				while (j == i);
				while (j < num)
				{
					HUDIndicator.INDICATOR.activeIndicators[j].listIndex = j;
					j++;
				}
				break;
			}
		}
	}

	// Token: 0x04001664 RID: 5732
	private static double _stepTime;

	// Token: 0x04001665 RID: 5733
	protected static Matrix4x4 worldToCameraLocalMatrix = Matrix4x4.identity;

	// Token: 0x04001666 RID: 5734
	private static double _lastTime;

	// Token: 0x04001667 RID: 5735
	private UIAnchor anchor;

	// Token: 0x04001668 RID: 5736
	private int listIndex = -1;

	// Token: 0x04001669 RID: 5737
	private static HUDIndicator.Target CenterFixed3000Tall = new HUDIndicator.Target("HUD_INDICATOR_CENTER_3000", 3000, UIAnchor.Side.Center);

	// Token: 0x0400166A RID: 5738
	private static HUDIndicator.Target CenterAuto = new HUDIndicator.Target("HUD_INDICATOR_CENTER_AUTO", UIAnchor.Side.Center);

	// Token: 0x020004BD RID: 1213
	private struct Target
	{
		// Token: 0x06002A55 RID: 10837 RVA: 0x000A64C8 File Offset: 0x000A46C8
		public Target(string name)
		{
			this = new HUDIndicator.Target(name, true, 1000, UIAnchor.Side.Center);
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000A64D8 File Offset: 0x000A46D8
		public Target(string name, int manualSize)
		{
			this = new HUDIndicator.Target(name, false, manualSize, UIAnchor.Side.Center);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000A64E4 File Offset: 0x000A46E4
		public Target(string name, UIAnchor.Side side)
		{
			this = new HUDIndicator.Target(name, true, 1000, side);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000A64F4 File Offset: 0x000A46F4
		public Target(string name, int manualSize, UIAnchor.Side side)
		{
			this = new HUDIndicator.Target(name, false, manualSize, side);
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000A6500 File Offset: 0x000A4700
		private Target(string name, bool automatic, int manualSize, UIAnchor.Side side)
		{
			this.automatic = automatic;
			this.manualSize = manualSize;
			this.name = name;
			this.side = side;
			this._root = null;
			this._anchor = null;
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x000A6530 File Offset: 0x000A4730
		public static UICamera uiCamera
		{
			get
			{
				if (!HUDIndicator.Target._uiCamera)
				{
					HUDIndicator.Target._uiCamera = UICamera.FindCameraForLayer(HUDIndicator.Target.g.layer);
				}
				return HUDIndicator.Target._uiCamera;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x000A6558 File Offset: 0x000A4758
		public static Camera camera
		{
			get
			{
				UICamera uiCamera = HUDIndicator.Target.uiCamera;
				return (!uiCamera) ? null : uiCamera.cachedCamera;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000A6584 File Offset: 0x000A4784
		public UIRoot root
		{
			get
			{
				if (!this._root)
				{
					this._root = new GameObject(this.name, new Type[]
					{
						typeof(UIRoot)
					})
					{
						layer = HUDIndicator.Target.g.layer
					}.GetComponent<UIRoot>();
					this._root.automatic = this.automatic;
					this._root.manualHeight = this.manualSize;
				}
				return this._root;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000A6600 File Offset: 0x000A4800
		public UIAnchor anchor
		{
			get
			{
				if (!this._anchor)
				{
					UIRoot root = this.root;
					this._anchor = new GameObject("ANCHOR", new Type[]
					{
						typeof(UIAnchor)
					})
					{
						layer = HUDIndicator.Target.g.layer
					}.GetComponent<UIAnchor>();
					this._anchor.transform.parent = root.transform;
					this._anchor.side = this.side;
					this._anchor.uiCamera = HUDIndicator.Target.camera;
				}
				return this._anchor;
			}
		}

		// Token: 0x0400166B RID: 5739
		private const int kDefaultManualSize = 1000;

		// Token: 0x0400166C RID: 5740
		private const UIAnchor.Side kDefaultSide = UIAnchor.Side.Center;

		// Token: 0x0400166D RID: 5741
		private UIRoot _root;

		// Token: 0x0400166E RID: 5742
		private UIAnchor _anchor;

		// Token: 0x0400166F RID: 5743
		public readonly string name;

		// Token: 0x04001670 RID: 5744
		public readonly bool automatic;

		// Token: 0x04001671 RID: 5745
		public readonly int manualSize;

		// Token: 0x04001672 RID: 5746
		public readonly UIAnchor.Side side;

		// Token: 0x04001673 RID: 5747
		private static UICamera _uiCamera;

		// Token: 0x020004BE RID: 1214
		private static class g
		{
			// Token: 0x04001674 RID: 5748
			public static readonly int layer = LayerMask.NameToLayer("NGUILayer2D");
		}
	}

	// Token: 0x020004BF RID: 1215
	protected enum ScratchTarget
	{
		// Token: 0x04001676 RID: 5750
		CenteredAuto,
		// Token: 0x04001677 RID: 5751
		CenteredFixed3000Tall
	}

	// Token: 0x020004C0 RID: 1216
	protected enum PlacementSpace
	{
		// Token: 0x04001679 RID: 5753
		World,
		// Token: 0x0400167A RID: 5754
		Screen,
		// Token: 0x0400167B RID: 5755
		Viewport,
		// Token: 0x0400167C RID: 5756
		Anchor,
		// Token: 0x0400167D RID: 5757
		DoNotModify
	}

	// Token: 0x020004C1 RID: 1217
	private static class INDICATOR
	{
		// Token: 0x06002A60 RID: 10848 RVA: 0x000A66B8 File Offset: 0x000A48B8
		public static void Add(HUDIndicator hud)
		{
			if (hud.listIndex != -1)
			{
				return;
			}
			hud.listIndex = HUDIndicator.INDICATOR.activeIndicators.Count;
			HUDIndicator.INDICATOR.activeIndicators.Add(hud);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000A66F0 File Offset: 0x000A48F0
		public static void Remove(HUDIndicator hud)
		{
			if (hud.listIndex == -1)
			{
				return;
			}
			try
			{
				HUDIndicator.INDICATOR.activeIndicators.RemoveAt(hud.listIndex);
				int i = hud.listIndex;
				int count = HUDIndicator.INDICATOR.activeIndicators.Count;
				while (i < count)
				{
					HUDIndicator.INDICATOR.activeIndicators[i].listIndex = i;
					i++;
				}
			}
			finally
			{
				hud.listIndex = -1;
			}
		}

		// Token: 0x0400167E RID: 5758
		public static List<HUDIndicator> activeIndicators = new List<HUDIndicator>();
	}
}
