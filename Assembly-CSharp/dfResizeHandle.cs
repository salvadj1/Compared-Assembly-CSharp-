using System;
using UnityEngine;

// Token: 0x020007BD RID: 1981
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Resize Handle")]
[Serializable]
public class dfResizeHandle : global::dfControl
{
	// Token: 0x17000CFD RID: 3325
	// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000FA7B0 File Offset: 0x000F89B0
	// (set) Token: 0x060043A6 RID: 17318 RVA: 0x000FA7F8 File Offset: 0x000F89F8
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

	// Token: 0x17000CFE RID: 3326
	// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000FA818 File Offset: 0x000F8A18
	// (set) Token: 0x060043A8 RID: 17320 RVA: 0x000FA820 File Offset: 0x000F8A20
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

	// Token: 0x17000CFF RID: 3327
	// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000FA840 File Offset: 0x000F8A40
	// (set) Token: 0x060043AA RID: 17322 RVA: 0x000FA848 File Offset: 0x000F8A48
	public global::dfResizeHandle.ResizeEdge Edges
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

	// Token: 0x060043AB RID: 17323 RVA: 0x000FA854 File Offset: 0x000F8A54
	public override void Start()
	{
		base.Start();
		if (base.Size.magnitude <= 1.401298E-45f)
		{
			base.Size = new Vector2(25f, 25f);
			if (base.Parent != null)
			{
				base.RelativePosition = base.Parent.Size - base.Size;
				base.Anchor = (global::dfAnchorStyle.Bottom | global::dfAnchorStyle.Right);
			}
		}
	}

	// Token: 0x060043AC RID: 17324 RVA: 0x000FA8D0 File Offset: 0x000F8AD0
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
		Color32 color = base.ApplyOpacity((!base.IsEnabled) ? this.disabledColor : this.color);
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

	// Token: 0x060043AD RID: 17325 RVA: 0x000FA9FC File Offset: 0x000F8BFC
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
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
		if ((this.Edges & global::dfResizeHandle.ResizeEdge.Left) == global::dfResizeHandle.ResizeEdge.Left)
		{
			this.minEdgePos.x = this.maxEdgePos.x - vector2.x;
			this.maxEdgePos.x = this.maxEdgePos.x - vector.x;
		}
		else if ((this.Edges & global::dfResizeHandle.ResizeEdge.Right) == global::dfResizeHandle.ResizeEdge.Right)
		{
			this.minEdgePos.x = this.startPosition.x + vector.x;
			this.maxEdgePos.x = this.startPosition.x + vector2.x;
		}
		if ((this.Edges & global::dfResizeHandle.ResizeEdge.Top) == global::dfResizeHandle.ResizeEdge.Top)
		{
			this.minEdgePos.y = this.maxEdgePos.y - vector2.y;
			this.maxEdgePos.y = this.maxEdgePos.y - vector.y;
		}
		else if ((this.Edges & global::dfResizeHandle.ResizeEdge.Bottom) == global::dfResizeHandle.ResizeEdge.Bottom)
		{
			this.minEdgePos.y = this.startPosition.y + vector.y;
			this.maxEdgePos.y = this.startPosition.y + vector2.y;
		}
	}

	// Token: 0x060043AE RID: 17326 RVA: 0x000FAC34 File Offset: 0x000F8E34
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		if (!args.Buttons.IsSet(global::dfMouseButtons.Left) || this.Edges == global::dfResizeHandle.ResizeEdge.None)
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
		if ((this.Edges & global::dfResizeHandle.ResizeEdge.Left) == global::dfResizeHandle.ResizeEdge.Left)
		{
			num3 = Mathf.Min(this.maxEdgePos.x, Mathf.Max(this.minEdgePos.x, num3 + vector3.x));
		}
		else if ((this.Edges & global::dfResizeHandle.ResizeEdge.Right) == global::dfResizeHandle.ResizeEdge.Right)
		{
			num5 = Mathf.Min(this.maxEdgePos.x, Mathf.Max(this.minEdgePos.x, num5 + vector3.x));
		}
		if ((this.Edges & global::dfResizeHandle.ResizeEdge.Top) == global::dfResizeHandle.ResizeEdge.Top)
		{
			num4 = Mathf.Min(this.maxEdgePos.y, Mathf.Max(this.minEdgePos.y, num4 + vector3.y));
		}
		else if ((this.Edges & global::dfResizeHandle.ResizeEdge.Bottom) == global::dfResizeHandle.ResizeEdge.Bottom)
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

	// Token: 0x060043AF RID: 17327 RVA: 0x000FAE6C File Offset: 0x000F906C
	protected internal override void OnMouseUp(global::dfMouseEventArgs args)
	{
		base.Parent.MakePixelPerfect(true);
		args.Use();
		base.OnMouseUp(args);
	}

	// Token: 0x040023E8 RID: 9192
	[SerializeField]
	protected global::dfAtlas atlas;

	// Token: 0x040023E9 RID: 9193
	[SerializeField]
	protected string backgroundSprite = string.Empty;

	// Token: 0x040023EA RID: 9194
	[SerializeField]
	protected global::dfResizeHandle.ResizeEdge edges = global::dfResizeHandle.ResizeEdge.Right | global::dfResizeHandle.ResizeEdge.Bottom;

	// Token: 0x040023EB RID: 9195
	private Vector3 mouseAnchorPos;

	// Token: 0x040023EC RID: 9196
	private Vector3 startPosition;

	// Token: 0x040023ED RID: 9197
	private Vector2 startSize;

	// Token: 0x040023EE RID: 9198
	private Vector2 minEdgePos;

	// Token: 0x040023EF RID: 9199
	private Vector2 maxEdgePos;

	// Token: 0x020007BE RID: 1982
	[Flags]
	public enum ResizeEdge
	{
		// Token: 0x040023F1 RID: 9201
		None = 0,
		// Token: 0x040023F2 RID: 9202
		Left = 1,
		// Token: 0x040023F3 RID: 9203
		Right = 2,
		// Token: 0x040023F4 RID: 9204
		Top = 4,
		// Token: 0x040023F5 RID: 9205
		Bottom = 8
	}
}
