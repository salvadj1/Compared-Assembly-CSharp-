using System;
using UnityEngine;

// Token: 0x02000771 RID: 1905
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Drag Handle")]
[Serializable]
public class dfDragHandle : global::dfControl
{
	// Token: 0x0600400F RID: 16399 RVA: 0x000EAD60 File Offset: 0x000E8F60
	public override void Start()
	{
		base.Start();
		if (base.Size.magnitude <= 1.401298E-45f)
		{
			if (base.Parent != null)
			{
				base.Size = new Vector2(base.Parent.Width, 30f);
				base.Anchor = (global::dfAnchorStyle.Top | global::dfAnchorStyle.Left | global::dfAnchorStyle.Right);
				base.RelativePosition = Vector2.zero;
			}
			else
			{
				base.Size = new Vector2(200f, 25f);
			}
		}
	}

	// Token: 0x06004010 RID: 16400 RVA: 0x000EADEC File Offset: 0x000E8FEC
	protected internal override void OnMouseDown(global::dfMouseEventArgs args)
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

	// Token: 0x06004011 RID: 16401 RVA: 0x000EAE7C File Offset: 0x000E907C
	protected internal override void OnMouseMove(global::dfMouseEventArgs args)
	{
		args.Use();
		if (args.Buttons.IsSet(global::dfMouseButtons.Left))
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

	// Token: 0x06004012 RID: 16402 RVA: 0x000EAF60 File Offset: 0x000E9160
	protected internal override void OnMouseUp(global::dfMouseEventArgs args)
	{
		base.OnMouseUp(args);
		base.Parent.MakePixelPerfect(true);
	}

	// Token: 0x04002182 RID: 8578
	private Vector3 lastPosition;
}
