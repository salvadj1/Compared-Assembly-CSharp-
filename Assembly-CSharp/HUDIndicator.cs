using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000577 RID: 1399
public abstract class HUDIndicator : MonoBehaviour
{
	// Token: 0x170009D8 RID: 2520
	// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000ADEB0 File Offset: 0x000AC0B0
	protected static double stepTime
	{
		get
		{
			return global::HUDIndicator._stepTime;
		}
	}

	// Token: 0x170009D9 RID: 2521
	// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000ADEB8 File Offset: 0x000AC0B8
	protected static double time
	{
		get
		{
			return (!global::DebugInput.GetKey(46)) ? (global::HUDIndicator._lastTime = global::NetCull.time) : global::HUDIndicator._lastTime;
		}
	}

	// Token: 0x06002DFE RID: 11774
	protected abstract bool Continue();

	// Token: 0x06002DFF RID: 11775 RVA: 0x000ADEDC File Offset: 0x000AC0DC
	protected Vector3 GetPoint(global::HUDIndicator.PlacementSpace space, Vector3 input)
	{
		switch (space)
		{
		case global::HUDIndicator.PlacementSpace.World:
		{
			Camera camera = global::HUDIndicator.Target.camera;
			Vector3? vector = global::CameraFX.World2Screen(input);
			input = camera.ScreenToWorldPoint((vector == null) ? Vector3.zero : vector.Value);
			break;
		}
		case global::HUDIndicator.PlacementSpace.Screen:
			input = global::HUDIndicator.Target.camera.ScreenToWorldPoint(input);
			break;
		case global::HUDIndicator.PlacementSpace.Viewport:
			input = global::HUDIndicator.Target.camera.ViewportToWorldPoint(input);
			break;
		case global::HUDIndicator.PlacementSpace.Anchor:
			input = this.anchor.transform.TransformPoint(input);
			break;
		}
		return input;
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x000ADF80 File Offset: 0x000AC180
	private static global::HUDIndicator InstantiateIndicator(ref global::HUDIndicator.Target target, global::HUDIndicator prefab, global::HUDIndicator.PlacementSpace space, Vector3 position, float rotation)
	{
		global::UIAnchor uianchor = target.anchor;
		Quaternion quaternion = Quaternion.AngleAxis(rotation, Vector3.back);
		switch (space)
		{
		case global::HUDIndicator.PlacementSpace.World:
		{
			Camera camera = global::HUDIndicator.Target.camera;
			Vector3? vector = global::CameraFX.World2Screen(position);
			position = camera.ScreenToWorldPoint((vector == null) ? Vector3.zero : vector.Value);
			break;
		}
		case global::HUDIndicator.PlacementSpace.Screen:
			position = global::HUDIndicator.Target.camera.ScreenToWorldPoint(position);
			break;
		case global::HUDIndicator.PlacementSpace.Viewport:
			position = global::HUDIndicator.Target.camera.ViewportToWorldPoint(position);
			break;
		case global::HUDIndicator.PlacementSpace.Anchor:
			position = uianchor.transform.TransformPoint(position);
			quaternion = uianchor.transform.rotation * quaternion;
			break;
		}
		position.z = uianchor.transform.position.z;
		global::HUDIndicator hudindicator = (global::HUDIndicator)Object.Instantiate(prefab, position, quaternion);
		hudindicator.transform.parent = uianchor.transform;
		hudindicator.transform.localScale = Vector3.one;
		hudindicator.anchor = target.anchor;
		return hudindicator;
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x000AE09C File Offset: 0x000AC29C
	protected static global::HUDIndicator InstantiateIndicator(global::HUDIndicator.ScratchTarget target, global::HUDIndicator prefab, global::HUDIndicator.PlacementSpace space, Vector3 position, float rotation)
	{
		if (target == global::HUDIndicator.ScratchTarget.CenteredAuto)
		{
			return global::HUDIndicator.InstantiateIndicator(ref global::HUDIndicator.CenterAuto, prefab, space, position, rotation);
		}
		if (target != global::HUDIndicator.ScratchTarget.CenteredFixed3000Tall)
		{
			throw new ArgumentOutOfRangeException("target");
		}
		return global::HUDIndicator.InstantiateIndicator(ref global::HUDIndicator.CenterFixed3000Tall, prefab, space, position, rotation);
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x000AE0E8 File Offset: 0x000AC2E8
	protected static global::HUDIndicator InstantiateIndicator(global::HUDIndicator.ScratchTarget target, global::HUDIndicator prefab, global::HUDIndicator.PlacementSpace space, Vector3 position)
	{
		return global::HUDIndicator.InstantiateIndicator(target, prefab, space, position, 0f);
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x000AE0F8 File Offset: 0x000AC2F8
	protected static global::HUDIndicator InstantiateIndicator(global::HUDIndicator.ScratchTarget target, global::HUDIndicator prefab)
	{
		return global::HUDIndicator.InstantiateIndicator(target, prefab, global::HUDIndicator.PlacementSpace.Anchor, Vector3.zero, 0f);
	}

	// Token: 0x06002E04 RID: 11780 RVA: 0x000AE10C File Offset: 0x000AC30C
	protected void Start()
	{
		if (!this.Continue())
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			global::HUDIndicator.INDICATOR.Add(this);
		}
	}

	// Token: 0x06002E05 RID: 11781 RVA: 0x000AE130 File Offset: 0x000AC330
	protected void OnDestroy()
	{
		if (this.listIndex != -1)
		{
			global::HUDIndicator.INDICATOR.Remove(this);
		}
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x000AE144 File Offset: 0x000AC344
	internal static void Step()
	{
		global::HUDIndicator._stepTime = global::HUDIndicator.time;
		Camera main = Camera.main;
		if (main)
		{
			global::HUDIndicator.worldToCameraLocalMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * main.worldToCameraMatrix;
		}
		int num = global::HUDIndicator.INDICATOR.activeIndicators.Count;
		for (int i = num - 1; i >= 0; i--)
		{
			if (!global::HUDIndicator.INDICATOR.activeIndicators[i].Continue())
			{
				int j = i;
				do
				{
					global::HUDIndicator hudindicator = global::HUDIndicator.INDICATOR.activeIndicators[i];
					global::HUDIndicator.INDICATOR.activeIndicators.RemoveAt(i);
					hudindicator.listIndex = -1;
					Object.Destroy(hudindicator.gameObject);
					num--;
					if (--i >= 0)
					{
						while (global::HUDIndicator.INDICATOR.activeIndicators[i].Continue())
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
					global::HUDIndicator.INDICATOR.activeIndicators[j].listIndex = j;
					j++;
				}
				break;
			}
		}
	}

	// Token: 0x04001821 RID: 6177
	private static double _stepTime;

	// Token: 0x04001822 RID: 6178
	protected static Matrix4x4 worldToCameraLocalMatrix = Matrix4x4.identity;

	// Token: 0x04001823 RID: 6179
	private static double _lastTime;

	// Token: 0x04001824 RID: 6180
	private global::UIAnchor anchor;

	// Token: 0x04001825 RID: 6181
	private int listIndex = -1;

	// Token: 0x04001826 RID: 6182
	private static global::HUDIndicator.Target CenterFixed3000Tall = new global::HUDIndicator.Target("HUD_INDICATOR_CENTER_3000", 3000, global::UIAnchor.Side.Center);

	// Token: 0x04001827 RID: 6183
	private static global::HUDIndicator.Target CenterAuto = new global::HUDIndicator.Target("HUD_INDICATOR_CENTER_AUTO", global::UIAnchor.Side.Center);

	// Token: 0x02000578 RID: 1400
	private struct Target
	{
		// Token: 0x06002E07 RID: 11783 RVA: 0x000AE260 File Offset: 0x000AC460
		public Target(string name)
		{
			this = new global::HUDIndicator.Target(name, true, 1000, global::UIAnchor.Side.Center);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000AE270 File Offset: 0x000AC470
		public Target(string name, int manualSize)
		{
			this = new global::HUDIndicator.Target(name, false, manualSize, global::UIAnchor.Side.Center);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000AE27C File Offset: 0x000AC47C
		public Target(string name, global::UIAnchor.Side side)
		{
			this = new global::HUDIndicator.Target(name, true, 1000, side);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000AE28C File Offset: 0x000AC48C
		public Target(string name, int manualSize, global::UIAnchor.Side side)
		{
			this = new global::HUDIndicator.Target(name, false, manualSize, side);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000AE298 File Offset: 0x000AC498
		private Target(string name, bool automatic, int manualSize, global::UIAnchor.Side side)
		{
			this.automatic = automatic;
			this.manualSize = manualSize;
			this.name = name;
			this.side = side;
			this._root = null;
			this._anchor = null;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000AE2C8 File Offset: 0x000AC4C8
		public static global::UICamera uiCamera
		{
			get
			{
				if (!global::HUDIndicator.Target._uiCamera)
				{
					global::HUDIndicator.Target._uiCamera = global::UICamera.FindCameraForLayer(global::HUDIndicator.Target.g.layer);
				}
				return global::HUDIndicator.Target._uiCamera;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000AE2F0 File Offset: 0x000AC4F0
		public static Camera camera
		{
			get
			{
				global::UICamera uiCamera = global::HUDIndicator.Target.uiCamera;
				return (!uiCamera) ? null : uiCamera.cachedCamera;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000AE31C File Offset: 0x000AC51C
		public global::UIRoot root
		{
			get
			{
				if (!this._root)
				{
					this._root = new GameObject(this.name, new Type[]
					{
						typeof(global::UIRoot)
					})
					{
						layer = global::HUDIndicator.Target.g.layer
					}.GetComponent<global::UIRoot>();
					this._root.automatic = this.automatic;
					this._root.manualHeight = this.manualSize;
				}
				return this._root;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000AE398 File Offset: 0x000AC598
		public global::UIAnchor anchor
		{
			get
			{
				if (!this._anchor)
				{
					global::UIRoot root = this.root;
					this._anchor = new GameObject("ANCHOR", new Type[]
					{
						typeof(global::UIAnchor)
					})
					{
						layer = global::HUDIndicator.Target.g.layer
					}.GetComponent<global::UIAnchor>();
					this._anchor.transform.parent = root.transform;
					this._anchor.side = this.side;
					this._anchor.uiCamera = global::HUDIndicator.Target.camera;
				}
				return this._anchor;
			}
		}

		// Token: 0x04001828 RID: 6184
		private const int kDefaultManualSize = 1000;

		// Token: 0x04001829 RID: 6185
		private const global::UIAnchor.Side kDefaultSide = global::UIAnchor.Side.Center;

		// Token: 0x0400182A RID: 6186
		private global::UIRoot _root;

		// Token: 0x0400182B RID: 6187
		private global::UIAnchor _anchor;

		// Token: 0x0400182C RID: 6188
		public readonly string name;

		// Token: 0x0400182D RID: 6189
		public readonly bool automatic;

		// Token: 0x0400182E RID: 6190
		public readonly int manualSize;

		// Token: 0x0400182F RID: 6191
		public readonly global::UIAnchor.Side side;

		// Token: 0x04001830 RID: 6192
		private static global::UICamera _uiCamera;

		// Token: 0x02000579 RID: 1401
		private static class g
		{
			// Token: 0x04001831 RID: 6193
			public static readonly int layer = LayerMask.NameToLayer("NGUILayer2D");
		}
	}

	// Token: 0x0200057A RID: 1402
	protected enum ScratchTarget
	{
		// Token: 0x04001833 RID: 6195
		CenteredAuto,
		// Token: 0x04001834 RID: 6196
		CenteredFixed3000Tall
	}

	// Token: 0x0200057B RID: 1403
	protected enum PlacementSpace
	{
		// Token: 0x04001836 RID: 6198
		World,
		// Token: 0x04001837 RID: 6199
		Screen,
		// Token: 0x04001838 RID: 6200
		Viewport,
		// Token: 0x04001839 RID: 6201
		Anchor,
		// Token: 0x0400183A RID: 6202
		DoNotModify
	}

	// Token: 0x0200057C RID: 1404
	private static class INDICATOR
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x000AE450 File Offset: 0x000AC650
		public static void Add(global::HUDIndicator hud)
		{
			if (hud.listIndex != -1)
			{
				return;
			}
			hud.listIndex = global::HUDIndicator.INDICATOR.activeIndicators.Count;
			global::HUDIndicator.INDICATOR.activeIndicators.Add(hud);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x000AE488 File Offset: 0x000AC688
		public static void Remove(global::HUDIndicator hud)
		{
			if (hud.listIndex == -1)
			{
				return;
			}
			try
			{
				global::HUDIndicator.INDICATOR.activeIndicators.RemoveAt(hud.listIndex);
				int i = hud.listIndex;
				int count = global::HUDIndicator.INDICATOR.activeIndicators.Count;
				while (i < count)
				{
					global::HUDIndicator.INDICATOR.activeIndicators[i].listIndex = i;
					i++;
				}
			}
			finally
			{
				hud.listIndex = -1;
			}
		}

		// Token: 0x0400183B RID: 6203
		public static List<global::HUDIndicator> activeIndicators = new List<global::HUDIndicator>();
	}
}
