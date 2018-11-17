using System;
using UnityEngine;

// Token: 0x020006EB RID: 1771
[AddComponentMenu("Daikon Forge/User Interface/Resize Handle")]
[ExecuteInEditMode]
[Serializable]
public class dfResizeHandle : dfControl
{
	// Token: 0x17000C79 RID: 3193
	// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000F1BAC File Offset: 0x000EFDAC
	// (set) Token: 0x06003F8A RID: 16266 RVA: 0x000F1BF4 File Offset: 0x000EFDF4
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

	// Token: 0x17000C7A RID: 3194
	// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000F1C14 File Offset: 0x000EFE14
	// (set) Token: 0x06003F8C RID: 16268 RVA: 0x000F1C1C File Offset: 0x000EFE1C
	public string BackgroundSprite
	{
		get
		{
			return this.backgroundSprite;
		}
		set
		{
			if (value != this.backgroundSprite)
			{
				this.backgroundSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000C7B RID: 3195
	// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000F1C3C File Offset: 0x000EFE3C
	// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000F1C44 File Offset: 0x000EFE44
	public dfResizeHandle.ResizeEdge Edges
	{
		get
		{
			return this.edges;
		}
		set
		{
			this.edges = value;
		}
	}

	// Token: 0x06003F8F RID: 16271 RVA: 0x000F1C50 File Offset: 0x000EFE50
	public override void Start()
	{
		base.Start();
		if (base.Size.magnitude <= 1.401298E-45f)
		{
			base.Size = new Vector2(25f, 25f);
			if (base.Parent != null)
			{
				base.RelativePosition = base.Parent.Size - base.Size;
				base.Anchor = (dfAnchorStyle.Bottom | dfAnchorStyle.Right);
			}
		}
	}

	// Token: 0x06003F90 RID: 16272 RVA: 0x000F1CCC File Offset: 0x000EFECC
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
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
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

	// Token: 0x06003F91 RID: 16273 RVA: 0x000F1DF8 File Offset: 0x000EFFF8
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		args.Use();
		Plane plane;
		plane..ctor(this.parent.transform.TransformDirection(Vector3.back), this.parent.transform.position);
		Ray ray = args.Ray;
		float num = 0f;
		plane.Raycast(args.Ray, ref num);
		this.mouseAnchorPos = ray.origin + ray.direction * num;
		this.startSize = this.parent.Size;
		this.startPosition = this.parent.RelativePosition;
		this.minEdgePos = this.startPosition;
		this.maxEdgePos = this.startPosition + this.startSize;
		Vector2 vector = this.parent.CalculateMinimumSize();
		Vector2 vector2 = this.parent.MaximumSize;
		if (vector2.magnitude <= 1.401298E-45f)
		{
			vector2 = Vector2.one * 2048f;
		}
		if ((this.Edges & dfResizeHandle.ResizeEdge.Left) == dfResizeHandle.ResizeEdge.Left)
		{
			this.minEdgePos.x = this.maxEdgePos.x - vector2.x;
			this.maxEdgePos.x = this.maxEdgePos.x - vector.x;
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Right) == dfResizeHandle.ResizeEdge.Right)
		{
			this.minEdgePos.x = this.startPosition.x + vector.x;
			this.maxEdgePos.x = this.startPosition.x + vector2.x;
		}
		if ((this.Edges & dfResizeHandle.ResizeEdge.Top) == dfResizeHandle.ResizeEdge.Top)
		{
			this.minEdgePos.y = this.maxEdgePos.y - vector2.y;
			this.maxEdgePos.y = this.maxEdgePos.y - vector.y;
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Bottom) == dfResizeHandle.ResizeEdge.Bottom)
		{
			this.minEdgePos.y = this.startPosition.y + vector.y;
			this.maxEdgePos.y = this.startPosition.y + vector2.y;
		}
	}

	// Token: 0x06003F92 RID: 16274 RVA: 0x000F2030 File Offset: 0x000F0230
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(dfMouseButtons.Left) || this.Edges == dfResizeHandle.ResizeEdge.None)
		{
			return;
		}
		args.Use();
		Ray ray = args.Ray;
		float num = 0f;
		Vector3 vector = base.GetCamera().transform.TransformDirection(Vector3.back);
		Plane plane;
		plane..ctor(vector, this.mouseAnchorPos);
		plane.Raycast(ray, ref num);
		float num2 = base.PixelsToUnits();
		Vector3 vector2 = ray.origin + ray.direction * num;
		Vector3 vector3 = (vector2 - this.mouseAnchorPos) / num2;
		vector3.y *= -1f;
		float num3 = this.startPosition.x;
		float num4 = this.startPosition.y;
		float num5 = num3 + this.startSize.x;
		float num6 = num4 + this.startSize.y;
		if ((this.Edges & dfResizeHandle.ResizeEdge.Left) == dfResizeHandle.ResizeEdge.Left)
		{
			num3 = Mathf.Min(this.maxEdgePos.x, Mathf.Max(this.minEdgePos.x, num3 + vector3.x));
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Right) == dfResizeHandle.ResizeEdge.Right)
		{
			num5 = Mathf.Min(this.maxEdgePos.x, Mathf.Max(this.minEdgePos.x, num5 + vector3.x));
		}
		if ((this.Edges & dfResizeHandle.ResizeEdge.Top) == dfResizeHandle.ResizeEdge.Top)
		{
			num4 = Mathf.Min(this.maxEdgePos.y, Mathf.Max(this.minEdgePos.y, num4 + vector3.y));
		}
		else if ((this.Edges & dfResizeHandle.ResizeEdge.Bottom) == dfResizeHandle.ResizeEdge.Bottom)
		{
			num6 = Mathf.Min(this.maxEdgePos.y, Mathf.Max(this.minEdgePos.y, num6 + vector3.y));
		}
		this.parent.Size = new Vector2(num5 - num3, num6 - num4);
		this.parent.RelativePosition = new Vector3(num3, num4, 0f);
		if (this.parent.GetManager().PixelPerfectMode)
		{
			this.parent.MakePixelPerfect(true);
		}
	}

	// Token: 0x06003F93 RID: 16275 RVA: 0x000F2268 File Offset: 0x000F0468
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.Parent.MakePixelPerfect(true);
		args.Use();
		base.OnMouseUp(args);
	}

	// Token: 0x040021DF RID: 8671
	[SerializeField]
	protected dfAtlas atlas;

	// Token: 0x040021E0 RID: 8672
	[SerializeField]
	protected string backgroundSprite = string.Empty;

	// Token: 0x040021E1 RID: 8673
	[SerializeField]
	protected dfResizeHandle.ResizeEdge edges = dfResizeHandle.ResizeEdge.Right | dfResizeHandle.ResizeEdge.Bottom;

	// Token: 0x040021E2 RID: 8674
	private Vector3 mouseAnchorPos;

	// Token: 0x040021E3 RID: 8675
	private Vector3 startPosition;

	// Token: 0x040021E4 RID: 8676
	private Vector2 startSize;

	// Token: 0x040021E5 RID: 8677
	private Vector2 minEdgePos;

	// Token: 0x040021E6 RID: 8678
	private Vector2 maxEdgePos;

	// Token: 0x020006EC RID: 1772
	[Flags]
	public enum ResizeEdge
	{
		// Token: 0x040021E8 RID: 8680
		None = 0,
		// Token: 0x040021E9 RID: 8681
		Left = 1,
		// Token: 0x040021EA RID: 8682
		Right = 2,
		// Token: 0x040021EB RID: 8683
		Top = 4,
		// Token: 0x040021EC RID: 8684
		Bottom = 8
	}
}
