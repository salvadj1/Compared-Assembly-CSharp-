using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x0200079B RID: 1947
public class UIGeometry
{
	// Token: 0x17000DA0 RID: 3488
	// (get) Token: 0x0600466A RID: 18026 RVA: 0x00118184 File Offset: 0x00116384
	public bool hasVertices
	{
		get
		{
			return this.meshBuffer.vSize > 0;
		}
	}

	// Token: 0x17000DA1 RID: 3489
	// (get) Token: 0x0600466B RID: 18027 RVA: 0x00118194 File Offset: 0x00116394
	public bool hasTransformed
	{
		get
		{
			return this.vertsTransformed || this.meshBuffer.vSize == 0;
		}
	}

	// Token: 0x0600466C RID: 18028 RVA: 0x001181B4 File Offset: 0x001163B4
	public void Clear()
	{
		this.meshBuffer.Clear();
		this.vertsTransformed = false;
	}

	// Token: 0x0600466D RID: 18029 RVA: 0x001181C8 File Offset: 0x001163C8
	public void Apply(ref Matrix4x4 widgetToPanel)
	{
		if (!this.vertsTransformed)
		{
			Debug.LogWarning("This overload of apply suggests you have called the other overload once before");
		}
		this.Apply(ref this.lastPivotOffset, ref widgetToPanel);
	}

	// Token: 0x0600466E RID: 18030 RVA: 0x001181F8 File Offset: 0x001163F8
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

	// Token: 0x0600466F RID: 18031 RVA: 0x00118468 File Offset: 0x00116668
	public void WriteToBuffers(MeshBuffer m)
	{
		this.meshBuffer.WriteBuffers(m);
	}

	// Token: 0x040026A0 RID: 9888
	[NonSerialized]
	public MeshBuffer meshBuffer = new MeshBuffer();

	// Token: 0x040026A1 RID: 9889
	private bool vertsTransformed;

	// Token: 0x040026A2 RID: 9890
	private Vector3 lastPivotOffset;

	// Token: 0x040026A3 RID: 9891
	private Matrix4x4 lastWidgetToPanel;
}
