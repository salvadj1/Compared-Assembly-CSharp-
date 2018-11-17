using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x02000886 RID: 2182
public class UIGeometry
{
	// Token: 0x17000E30 RID: 3632
	// (get) Token: 0x06004AEF RID: 19183 RVA: 0x00121B04 File Offset: 0x0011FD04
	public bool hasVertices
	{
		get
		{
			return this.meshBuffer.vSize > 0;
		}
	}

	// Token: 0x17000E31 RID: 3633
	// (get) Token: 0x06004AF0 RID: 19184 RVA: 0x00121B14 File Offset: 0x0011FD14
	public bool hasTransformed
	{
		get
		{
			return this.vertsTransformed || this.meshBuffer.vSize == 0;
		}
	}

	// Token: 0x06004AF1 RID: 19185 RVA: 0x00121B34 File Offset: 0x0011FD34
	public void Clear()
	{
		this.meshBuffer.Clear();
		this.vertsTransformed = false;
	}

	// Token: 0x06004AF2 RID: 19186 RVA: 0x00121B48 File Offset: 0x0011FD48
	public void Apply(ref Matrix4x4 widgetToPanel)
	{
		if (!this.vertsTransformed)
		{
			Debug.LogWarning("This overload of apply suggests you have called the other overload once before");
		}
		this.Apply(ref this.lastPivotOffset, ref widgetToPanel);
	}

	// Token: 0x06004AF3 RID: 19187 RVA: 0x00121B78 File Offset: 0x0011FD78
	public void Apply(ref Vector3 pivotOffset, ref Matrix4x4 widgetToPanel)
	{
		if (this.vertsTransformed)
		{
			if (pivotOffset == this.lastPivotOffset)
			{
				if (widgetToPanel == this.lastWidgetToPanel)
				{
					return;
				}
				Matrix4x4 matrix4x = this.lastWidgetToPanel.inverse;
				this.lastWidgetToPanel = widgetToPanel;
				matrix4x = widgetToPanel * matrix4x;
				this.meshBuffer.TransformVertices(matrix4x.m00, matrix4x.m10, matrix4x.m20, matrix4x.m01, matrix4x.m11, matrix4x.m21, matrix4x.m02, matrix4x.m12, matrix4x.m22, matrix4x.m03, matrix4x.m13, matrix4x.m23);
			}
			else
			{
				Debug.LogWarning("Verts were transformed more than once");
				Matrix4x4 inverse = this.lastWidgetToPanel.inverse;
				this.meshBuffer.TransformThenOffsetVertices(inverse.m00, inverse.m10, inverse.m20, inverse.m01, inverse.m11, inverse.m21, inverse.m02, inverse.m12, inverse.m22, inverse.m03, inverse.m13, inverse.m23, -this.lastPivotOffset.x, -this.lastPivotOffset.y, -this.lastPivotOffset.z);
				this.meshBuffer.OffsetThenTransformVertices(pivotOffset.x, pivotOffset.y, pivotOffset.z, widgetToPanel.m00, widgetToPanel.m10, widgetToPanel.m20, widgetToPanel.m01, widgetToPanel.m11, widgetToPanel.m21, widgetToPanel.m02, widgetToPanel.m12, widgetToPanel.m22, widgetToPanel.m03, widgetToPanel.m13, widgetToPanel.m23);
				this.lastWidgetToPanel = widgetToPanel;
				this.lastPivotOffset = pivotOffset;
			}
		}
		else
		{
			this.meshBuffer.OffsetThenTransformVertices(pivotOffset.x, pivotOffset.y, pivotOffset.z, widgetToPanel.m00, widgetToPanel.m10, widgetToPanel.m20, widgetToPanel.m01, widgetToPanel.m11, widgetToPanel.m21, widgetToPanel.m02, widgetToPanel.m12, widgetToPanel.m22, widgetToPanel.m03, widgetToPanel.m13, widgetToPanel.m23);
			this.lastWidgetToPanel = widgetToPanel;
			this.lastPivotOffset = pivotOffset;
			this.vertsTransformed = true;
		}
	}

	// Token: 0x06004AF4 RID: 19188 RVA: 0x00121DE8 File Offset: 0x0011FFE8
	public void WriteToBuffers(NGUI.Meshing.MeshBuffer m)
	{
		this.meshBuffer.WriteBuffers(m);
	}

	// Token: 0x040028D7 RID: 10455
	[NonSerialized]
	public NGUI.Meshing.MeshBuffer meshBuffer = new NGUI.Meshing.MeshBuffer();

	// Token: 0x040028D8 RID: 10456
	private bool vertsTransformed;

	// Token: 0x040028D9 RID: 10457
	private Vector3 lastPivotOffset;

	// Token: 0x040028DA RID: 10458
	private Matrix4x4 lastWidgetToPanel;
}
