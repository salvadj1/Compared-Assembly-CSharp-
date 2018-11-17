using System;
using UnityEngine;

// Token: 0x020007B8 RID: 1976
[AddComponentMenu("Daikon Forge/User Interface/Containers/Panel")]
[ExecuteInEditMode]
[Serializable]
public class dfPanel : global::dfControl
{
	// Token: 0x17000CDF RID: 3295
	// (get) Token: 0x06004336 RID: 17206 RVA: 0x000F867C File Offset: 0x000F687C
	// (set) Token: 0x06004337 RID: 17207 RVA: 0x000F86C4 File Offset: 0x000F68C4
	public global::dfAtlas Atlas
	{
		get
		{
			if (this.atlas == null)
			{
				global::dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					return this.atlas = manager.DefaultAtlas;
				}
			}
			return this.atlas;
		}
		set
		{
			if (!global::dfAtlas.Equals(value, this.atlas))
			{
				this.atlas = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000CE0 RID: 3296
	// (get) Token: 0x06004338 RID: 17208 RVA: 0x000F86E4 File Offset: 0x000F68E4
	// (set) Token: 0x06004339 RID: 17209 RVA: 0x000F86EC File Offset: 0x000F68EC
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

	// Token: 0x17000CE1 RID: 3297
	// (get) Token: 0x0600433A RID: 17210 RVA: 0x000F8718 File Offset: 0x000F6918
	// (set) Token: 0x0600433B RID: 17211 RVA: 0x000F8720 File Offset: 0x000F6920
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

	// Token: 0x17000CE2 RID: 3298
	// (get) Token: 0x0600433C RID: 17212 RVA: 0x000F8758 File Offset: 0x000F6958
	// (set) Token: 0x0600433D RID: 17213 RVA: 0x000F8778 File Offset: 0x000F6978
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

	// Token: 0x0600433E RID: 17214 RVA: 0x000F87AC File Offset: 0x000F69AC
	protected internal override void OnLocalize()
	{
		base.OnLocalize();
		this.BackgroundSprite = base.getLocalizedValue(this.backgroundSprite);
	}

	// Token: 0x0600433F RID: 17215 RVA: 0x000F87C8 File Offset: 0x000F69C8
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

	// Token: 0x06004340 RID: 17216 RVA: 0x000F8994 File Offset: 0x000F6B94
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

	// Token: 0x06004341 RID: 17217 RVA: 0x000F89F8 File Offset: 0x000F6BF8
	protected override void OnRebuildRenderData()
	{
		if (this.Atlas == null || string.IsNullOrEmpty(this.backgroundSprite))
		{
			return;
		}
		global::dfAtlas.ItemInfo itemInfo = this.Atlas[this.backgroundSprite];
		if (itemInfo == null)
		{
			return;
		}
		this.renderData.Material = this.Atlas.Material;
		Color32 color = base.ApplyOpacity(this.BackgroundColor);
		global::dfSprite.RenderOptions options = new global::dfSprite.RenderOptions
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
			global::dfSprite.renderSprite(this.renderData, options);
		}
		else
		{
			global::dfSlicedSprite.renderSprite(this.renderData, options);
		}
	}

	// Token: 0x06004342 RID: 17218 RVA: 0x000F8B10 File Offset: 0x000F6D10
	public void FitToContents()
	{
		if (this.controls.Count == 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			Vector2 vector2 = dfControl.RelativePosition + dfControl.Size;
			vector = Vector2.Max(vector, vector2);
		}
		base.Size = vector + new Vector2((float)this.padding.right, (float)this.padding.bottom);
	}

	// Token: 0x06004343 RID: 17219 RVA: 0x000F8BA8 File Offset: 0x000F6DA8
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
			global::dfControl dfControl = this.controls[i];
			Vector2 vector3 = dfControl.RelativePosition;
			Vector2 vector4 = vector3 + dfControl.Size;
			vector = Vector2.Min(vector, vector3);
			vector2 = Vector2.Max(vector2, vector4);
		}
		Vector2 vector5 = vector2 - vector;
		Vector2 vector6 = (base.Size - vector5) * 0.5f;
		for (int j = 0; j < this.controls.Count; j++)
		{
			global::dfControl dfControl2 = this.controls[j];
			dfControl2.RelativePosition = dfControl2.RelativePosition - vector + vector6;
		}
	}

	// Token: 0x040023C6 RID: 9158
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040023C7 RID: 9159
	[SerializeField]
	protected string backgroundSprite;

	// Token: 0x040023C8 RID: 9160
	[SerializeField]
	protected Color32 backgroundColor = UnityEngine.Color.white;

	// Token: 0x040023C9 RID: 9161
	[SerializeField]
	protected RectOffset padding = new RectOffset();
}
