using System;
using UnityEngine;

// Token: 0x020006A8 RID: 1704
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Drag Handle")]
[Serializable]
public class dfDragHandle : dfControl
{
	// Token: 0x06003C0B RID: 15371 RVA: 0x000E2238 File Offset: 0x000E0438
	public override void Start()
	{
		base.Start();
		if (base.Size.magnitude <= 1.401298E-45f)
		{
			if (base.Parent != null)
			{
				base.Size = new Vector2(base.Parent.Width, 30f);
				base.Anchor = (dfAnchorStyle.Top | dfAnchorStyle.Left | dfAnchorStyle.Right);
				base.RelativePosition = Vector2.zero;
			}
			else
			{
				base.Size = new Vector2(200f, 25f);
			}
		}
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x000E22C4 File Offset: 0x000E04C4
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		base.GetRootContainer().BringToFront();
		args.Use();
		Plane plane;
		plane..ctor(this.parent.transform.TransformDirection(Vector3.back), this.parent.transform.position);
		Ray ray = args.Ray;
		float num = 0f;
		plane.Raycast(args.Ray, ref num);
		this.lastPosition = ray.origin + ray.direction * num;
		base.OnMouseDown(args);
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x000E2354 File Offset: 0x000E0554
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		args.Use();
		if (args.Buttons.IsSet(dfMouseButtons.Left))
		{
			Ray ray = args.Ray;
			float num = 0f;
			Vector3 vector = base.GetCamera().transform.TransformDirection(Vector3.back);
			Plane plane;
			plane..ctor(vector, this.lastPosition);
			plane.Raycast(ray, ref num);
			Vector3 vector2 = (ray.origin + ray.direction * num).Quantize(this.parent.PixelsToUnits());
			Vector3 vector3 = vector2 - this.lastPosition;
			Vector3 position = (this.parent.transform.position + vector3).Quantize(this.parent.PixelsToUnits());
			this.parent.transform.position = position;
			this.lastPosition = vector2;
		}
		base.OnMouseMove(args);
	}

	// Token: 0x06003C0E RID: 15374 RVA: 0x000E2438 File Offset: 0x000E0638
	protected internal override void OnMouseUp(dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		base.Parent.MakePixelPerfect(true);
	}

	// Token: 0x04001F82 RID: 8066
	private Vector3 lastPosition;
}
