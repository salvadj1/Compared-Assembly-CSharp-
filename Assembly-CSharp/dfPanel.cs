using System;
using UnityEngine;

// Token: 0x020006E6 RID: 1766
[AddComponentMenu("Daikon Forge/User Interface/Containers/Panel")]
[ExecuteInEditMode]
[Serializable]
public class dfPanel : dfControl
{
	// Token: 0x17000C5B RID: 3163
	// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000EFA78 File Offset: 0x000EDC78
	// (set) Token: 0x06003F1B RID: 16155 RVA: 0x000EFAC0 File Offset: 0x000EDCC0
	public dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C5C RID: 3164
	// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
	// (set) Token: 0x06003F1D RID: 16157 RVA: 0x000EFAE8 File Offset: 0x000EDCE8
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			value = base.getLocalizedValue(value);
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C5D RID: 3165
	// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000EFB14 File Offset: 0x000EDD14
	// (set) Token: 0x06003F1F RID: 16159 RVA: 0x000EFB1C File Offset: 0x000EDD1C
	public Color32 BackgroundColor
	{
		get
		{
			return this.backgroundColor;
		}
		set
		{
			if (!object.Equals(value, this.backgroundColor))
			{
				this.backgroundColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C5E RID: 3166
	// (get) Token: 0x06003F20 RID: 16160 RVA: 0x000EFB54 File Offset: 0x000EDD54
	// (set) Token: 0x06003F21 RID: 16161 RVA: 0x000EFB74 File Offset: 0x000EDD74
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x06003F22 RID: 16162 RVA: 0x000EFBA8 File Offset: 0x000EDDA8
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.BackgroundSprite = base.getLocalizedValue(this.backgroundSprite);
	}

	// Token: 0x06003F23 RID: 16163 RVA: 0x000EFBC4 File Offset: 0x000EDDC4
	protected internal override Plane[] GetClippingPlanes()
	{
		if (!base.ClipChildren)
		{
			return null;
		}
		Vector3[] corners = base.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		float num = base.PixelsToUnits();
		RectOffset rectOffset = this.Padding;
		corners[0] += vector * (float)rectOffset.left * num + vector4 * (float)rectOffset.top * num;
		corners[1] += vector2 * (float)rectOffset.right * num + vector4 * (float)rectOffset.top * num;
		corners[2] += vector * (float)rectOffset.left * num + vector3 * (float)rectOffset.bottom * num;
		return new Plane[]
		{
			new Plane(vector, corners[0]),
			new Plane(vector2, corners[1]),
			new Plane(vector3, corners[2]),
			new Plane(vector4, corners[0])
		};
	}

	// Token: 0x06003F24 RID: 16164 RVA: 0x000EFD90 File Offset: 0x000EDF90
	public override void OnEnable()
	{
		base.OnEnable();
		if (this.size == Vector2.zero)
		{
			this.SuspendLayout();
			Camera camera = base.GetCamera();
			base.Size = new Vector3(camera.pixelWidth / 2f, camera.pixelHeight / 2f);
			this.ResumeLayout();
		}
	}

	// Token: 0x06003F25 RID: 16165 RVA: 0x000EFDF4 File Offset: 0x000EDFF4
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null || string.IsNullOrEmpty(this.backgroundSprite))
		{
			return;
		}
		dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		Color32 color = base.ApplyOpacity(this.BackgroundColor);
		dfSprite.RenderOptions options = new dfSprite.RenderOptions
		{
			atlas = this.atlas,
			color = color,
			fillAmount = 1f,
			offset = this.pivot.TransformToUpperLeft(base.Size),
			pixelsToUnits = base.PixelsToUnits(),
			size = base.Size,
			spriteInfo = itemInfo
		};
		if (itemInfo.border.horizontal == 0 && itemInfo.border.vertical == 0)
		{
			dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06003F26 RID: 16166 RVA: 0x000EFF0C File Offset: 0x000EE10C
	public void FitToContents()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 vector2 = dfControl.RelativePosition + dfControl.Size;
			vector = Vector2.Max(vector, vector2);
		}
		base.Size = vector + new Vector2((float)this.padding.right, (float)this.padding.bottom);
	}

	// Token: 0x06003F27 RID: 16167 RVA: 0x000EFFA4 File Offset: 0x000EE1A4
	public void CenterChildControls()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.one * float.MaxValue;
		Vector2 vector2 = Vector2.one * float.MinValue;
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			Vector2 vector3 = dfControl.RelativePosition;
			Vector2 vector4 = vector3 + dfControl.Size;
			vector = Vector2.Min(vector, vector3);
			vector2 = Vector2.Max(vector2, vector4);
		}
		Vector2 vector5 = vector2 - vector;
		Vector2 vector6 = (base.Size - vector5) * 0.5f;
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j];
			dfControl2.RelativePosition = dfControl2.RelativePosition - vector + vector6;
		}
	}

	// Token: 0x040021BD RID: 8637
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040021BE RID: 8638
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040021BF RID: 8639
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040021C0 RID: 8640
	[SerializeField]
	protected RectOffset padding = new RectOffset();
}
