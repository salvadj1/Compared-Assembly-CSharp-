using System;
using UnityEngine;

// Token: 0x02000508 RID: 1288
public class CameraSettings : MonoBehaviour
{
	// Token: 0x06002C08 RID: 11272 RVA: 0x000A5254 File Offset: 0x000A3454
	protected void Awake()
	{
		global::GameEvent.QualitySettingsRefresh += this.RefreshSettings;
	}

	// Token: 0x06002C09 RID: 11273 RVA: 0x000A5268 File Offset: 0x000A3468
	protected void OnDestroy()
	{
		global::GameEvent.QualitySettingsRefresh -= this.RefreshSettings;
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x000A527C File Offset: 0x000A347C
	private void RefreshSettings()
	{
		global::CameraLayerDepths component = base.GetComponent<global::CameraLayerDepths>();
		if (component)
		{
			foreach (global::CameraSettings.ViewDistanceLayer viewDistanceLayer in this.ViewDistanceLayers)
			{
				component[viewDistanceLayer.Index] = viewDistanceLayer.MinimumValue + global::render.distance * viewDistanceLayer.Range;
			}
		}
	}

	// Token: 0x04001604 RID: 5636
	public global::CameraSettings.ViewDistanceLayer[] ViewDistanceLayers;

	// Token: 0x02000509 RID: 1289
	[Serializable]
	public class ViewDistanceLayer
	{
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000A52E4 File Offset: 0x000A34E4
		public float Range
		{
			get
			{
				return this.MaximumValue - this.MinimumValue;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000A52F4 File Offset: 0x000A34F4
		public int Index
		{
			get
			{
				return LayerMask.NameToLayer(this.Name);
			}
		}

		// Token: 0x04001605 RID: 5637
		public string Name;

		// Token: 0x04001606 RID: 5638
		public float MinimumValue;

		// Token: 0x04001607 RID: 5639
		public float MaximumValue;
	}
}
