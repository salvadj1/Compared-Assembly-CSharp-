using System;
using UnityEngine;

// Token: 0x02000452 RID: 1106
public class CameraSettings : MonoBehaviour
{
	// Token: 0x06002878 RID: 10360 RVA: 0x0009F2D4 File Offset: 0x0009D4D4
	protected void Awake()
	{
		GameEvent.QualitySettingsRefresh += this.RefreshSettings;
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x0009F2E8 File Offset: 0x0009D4E8
	protected void OnDestroy()
	{
		GameEvent.QualitySettingsRefresh -= this.RefreshSettings;
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x0009F2FC File Offset: 0x0009D4FC
	private void RefreshSettings()
	{
		CameraLayerDepths component = base.GetComponent<CameraLayerDepths>();
		if (component)
		{
			foreach (CameraSettings.ViewDistanceLayer viewDistanceLayer in this.ViewDistanceLayers)
			{
				component[viewDistanceLayer.Index] = viewDistanceLayer.MinimumValue + render.distance * viewDistanceLayer.Range;
			}
		}
	}

	// Token: 0x04001481 RID: 5249
	public CameraSettings.ViewDistanceLayer[] ViewDistanceLayers;

	// Token: 0x02000453 RID: 1107
	[Serializable]
	public class ViewDistanceLayer
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x0009F364 File Offset: 0x0009D564
		public float Range
		{
			get
			{
				return this.MaximumValue - this.MinimumValue;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0009F374 File Offset: 0x0009D574
		public int Index
		{
			get
			{
				return LayerMask.NameToLayer(this.Name);
			}
		}

		// Token: 0x04001482 RID: 5250
		public string Name;

		// Token: 0x04001483 RID: 5251
		public float MinimumValue;

		// Token: 0x04001484 RID: 5252
		public float MaximumValue;
	}
}
