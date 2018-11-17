using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000891 RID: 2193
public class UIPanelMaterialPropertyBlock
{
	// Token: 0x06004B54 RID: 19284 RVA: 0x00126210 File Offset: 0x00124410
	private static global::UIPanelMaterialPropertyBlock.Node NewNode(global::UIPanelMaterialPropertyBlock block, int prop, global::UIPanelMaterialPropertyBlock.PropType type)
	{
		global::UIPanelMaterialPropertyBlock.Node node;
		if (global::UIPanelMaterialPropertyBlock.dumpCount > 0)
		{
			node = global::UIPanelMaterialPropertyBlock.dump;
			global::UIPanelMaterialPropertyBlock.dump = node.prev;
			global::UIPanelMaterialPropertyBlock.dumpCount--;
			node.disposed = false;
		}
		else
		{
			node = new global::UIPanelMaterialPropertyBlock.Node();
		}
		node.property = prop;
		node.type = type;
		if (block.count++ == 0)
		{
			block.first = (block.last = node);
			node.hasNext = (node.hasPrev = false);
			node.next = (node.prev = null);
		}
		else
		{
			node.prev = block.last;
			node.hasPrev = true;
			node.next = null;
			node.hasNext = false;
			block.last = node;
			node.prev.next = node;
			node.prev.hasNext = true;
		}
		return node;
	}

	// Token: 0x06004B55 RID: 19285 RVA: 0x001262F4 File Offset: 0x001244F4
	private static global::UIPanelMaterialPropertyBlock.Node NewNode(global::UIPanelMaterialPropertyBlock block, int prop, ref Vector4 value)
	{
		global::UIPanelMaterialPropertyBlock.Node node = global::UIPanelMaterialPropertyBlock.NewNode(block, prop, global::UIPanelMaterialPropertyBlock.PropType.Vector);
		node.value.VECTOR.x = value.x;
		node.value.VECTOR.y = value.y;
		node.value.VECTOR.z = value.z;
		node.value.VECTOR.w = value.w;
		return node;
	}

	// Token: 0x06004B56 RID: 19286 RVA: 0x00126364 File Offset: 0x00124564
	private static global::UIPanelMaterialPropertyBlock.Node NewNode(global::UIPanelMaterialPropertyBlock block, int prop, ref Color value)
	{
		global::UIPanelMaterialPropertyBlock.Node node = global::UIPanelMaterialPropertyBlock.NewNode(block, prop, global::UIPanelMaterialPropertyBlock.PropType.Color);
		node.value.COLOR.r = value.r;
		node.value.COLOR.g = value.g;
		node.value.COLOR.b = value.b;
		node.value.COLOR.a = value.a;
		return node;
	}

	// Token: 0x06004B57 RID: 19287 RVA: 0x001263D4 File Offset: 0x001245D4
	private static global::UIPanelMaterialPropertyBlock.Node NewNode(global::UIPanelMaterialPropertyBlock block, int prop, ref float value)
	{
		global::UIPanelMaterialPropertyBlock.Node node = global::UIPanelMaterialPropertyBlock.NewNode(block, prop, global::UIPanelMaterialPropertyBlock.PropType.Float);
		node.value.FLOAT = value;
		return node;
	}

	// Token: 0x06004B58 RID: 19288 RVA: 0x001263F8 File Offset: 0x001245F8
	private static global::UIPanelMaterialPropertyBlock.Node NewNode(global::UIPanelMaterialPropertyBlock block, int prop, ref Matrix4x4 value)
	{
		global::UIPanelMaterialPropertyBlock.Node node = global::UIPanelMaterialPropertyBlock.NewNode(block, prop, global::UIPanelMaterialPropertyBlock.PropType.Matrix);
		node.value.MATRIX.m00 = value.m00;
		node.value.MATRIX.m10 = value.m10;
		node.value.MATRIX.m20 = value.m20;
		node.value.MATRIX.m30 = value.m30;
		node.value.MATRIX.m01 = value.m01;
		node.value.MATRIX.m11 = value.m11;
		node.value.MATRIX.m21 = value.m21;
		node.value.MATRIX.m31 = value.m31;
		node.value.MATRIX.m02 = value.m02;
		node.value.MATRIX.m12 = value.m12;
		node.value.MATRIX.m22 = value.m22;
		node.value.MATRIX.m32 = value.m32;
		node.value.MATRIX.m03 = value.m03;
		node.value.MATRIX.m13 = value.m13;
		node.value.MATRIX.m23 = value.m23;
		node.value.MATRIX.m33 = value.m33;
		return node;
	}

	// Token: 0x06004B59 RID: 19289 RVA: 0x00126570 File Offset: 0x00124770
	public void Set(string property, Color value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x06004B5A RID: 19290 RVA: 0x00126580 File Offset: 0x00124780
	public void Set(string property, Vector4 value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x06004B5B RID: 19291 RVA: 0x00126590 File Offset: 0x00124790
	public void Set(string property, float value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x06004B5C RID: 19292 RVA: 0x001265A0 File Offset: 0x001247A0
	public void Set(string property, Matrix4x4 value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x06004B5D RID: 19293 RVA: 0x001265B0 File Offset: 0x001247B0
	public void Set(int property, Color value)
	{
		global::UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x06004B5E RID: 19294 RVA: 0x001265BC File Offset: 0x001247BC
	public void Set(int property, Vector4 value)
	{
		global::UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x06004B5F RID: 19295 RVA: 0x001265C8 File Offset: 0x001247C8
	public void Set(int property, float value)
	{
		global::UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x06004B60 RID: 19296 RVA: 0x001265D4 File Offset: 0x001247D4
	public void Set(int property, Matrix4x4 value)
	{
		global::UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x06004B61 RID: 19297 RVA: 0x001265E0 File Offset: 0x001247E0
	public void Clear()
	{
		if (this.count > 0)
		{
			this.first.prev = global::UIPanelMaterialPropertyBlock.dump;
			global::UIPanelMaterialPropertyBlock.dump = this.last;
			if (global::UIPanelMaterialPropertyBlock.dumpCount > 0)
			{
				this.first.prev.next = this.first;
				this.first.prev.hasNext = true;
				this.first.hasPrev = true;
			}
			this.first = (this.last = null);
			global::UIPanelMaterialPropertyBlock.dumpCount += this.count;
			this.count = 0;
		}
	}

	// Token: 0x06004B62 RID: 19298 RVA: 0x0012667C File Offset: 0x0012487C
	public void AddToMaterialPropertyBlock(MaterialPropertyBlock block)
	{
		global::UIPanelMaterialPropertyBlock.Node next = this.first;
		int num = this.count;
		while (num-- > 0)
		{
			switch (next.type)
			{
			case global::UIPanelMaterialPropertyBlock.PropType.Float:
				block.AddFloat(next.property, next.value.FLOAT);
				break;
			case global::UIPanelMaterialPropertyBlock.PropType.Vector:
				block.AddVector(next.property, next.value.VECTOR);
				break;
			case global::UIPanelMaterialPropertyBlock.PropType.Color:
				block.AddColor(next.property, next.value.COLOR);
				break;
			case global::UIPanelMaterialPropertyBlock.PropType.Matrix:
				block.AddMatrix(next.property, next.value.MATRIX);
				break;
			}
			next = next.next;
		}
	}

	// Token: 0x0400291D RID: 10525
	private global::UIPanelMaterialPropertyBlock.Node first;

	// Token: 0x0400291E RID: 10526
	private global::UIPanelMaterialPropertyBlock.Node last;

	// Token: 0x0400291F RID: 10527
	private int count;

	// Token: 0x04002920 RID: 10528
	private static global::UIPanelMaterialPropertyBlock.Node dump;

	// Token: 0x04002921 RID: 10529
	private static int dumpCount;

	// Token: 0x02000892 RID: 2194
	private enum PropType : byte
	{
		// Token: 0x04002923 RID: 10531
		Float,
		// Token: 0x04002924 RID: 10532
		Vector,
		// Token: 0x04002925 RID: 10533
		Color,
		// Token: 0x04002926 RID: 10534
		Matrix
	}

	// Token: 0x02000893 RID: 2195
	[StructLayout(LayoutKind.Explicit, Size = 64)]
	private struct PropValue
	{
		// Token: 0x04002927 RID: 10535
		[FieldOffset(0)]
		public Color COLOR;

		// Token: 0x04002928 RID: 10536
		[FieldOffset(0)]
		public float FLOAT;

		// Token: 0x04002929 RID: 10537
		[FieldOffset(0)]
		public Vector4 VECTOR;

		// Token: 0x0400292A RID: 10538
		[FieldOffset(0)]
		public Matrix4x4 MATRIX;
	}

	// Token: 0x02000894 RID: 2196
	private class Node
	{
		// Token: 0x0400292B RID: 10539
		public global::UIPanelMaterialPropertyBlock.Node prev;

		// Token: 0x0400292C RID: 10540
		public global::UIPanelMaterialPropertyBlock.Node next;

		// Token: 0x0400292D RID: 10541
		public int property;

		// Token: 0x0400292E RID: 10542
		public global::UIPanelMaterialPropertyBlock.PropValue value;

		// Token: 0x0400292F RID: 10543
		public global::UIPanelMaterialPropertyBlock.PropType type;

		// Token: 0x04002930 RID: 10544
		public bool hasNext;

		// Token: 0x04002931 RID: 10545
		public bool hasPrev;

		// Token: 0x04002932 RID: 10546
		public bool disposed;
	}
}
