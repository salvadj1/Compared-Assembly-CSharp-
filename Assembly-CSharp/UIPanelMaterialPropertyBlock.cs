using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020007A6 RID: 1958
public class UIPanelMaterialPropertyBlock
{
	// Token: 0x060046CF RID: 18127 RVA: 0x0011C890 File Offset: 0x0011AA90
	private static UIPanelMaterialPropertyBlock.Node NewNode(UIPanelMaterialPropertyBlock block, int prop, UIPanelMaterialPropertyBlock.PropType type)
	{
		UIPanelMaterialPropertyBlock.Node node;
		if (UIPanelMaterialPropertyBlock.dumpCount > 0)
		{
			node = UIPanelMaterialPropertyBlock.dump;
			UIPanelMaterialPropertyBlock.dump = node.prev;
			UIPanelMaterialPropertyBlock.dumpCount--;
			node.disposed = false;
		}
		else
		{
			node = new UIPanelMaterialPropertyBlock.Node();
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

	// Token: 0x060046D0 RID: 18128 RVA: 0x0011C974 File Offset: 0x0011AB74
	private static UIPanelMaterialPropertyBlock.Node NewNode(UIPanelMaterialPropertyBlock block, int prop, ref Vector4 value)
	{
		UIPanelMaterialPropertyBlock.Node node = UIPanelMaterialPropertyBlock.NewNode(block, prop, UIPanelMaterialPropertyBlock.PropType.Vector);
		node.value.VECTOR.x = value.x;
		node.value.VECTOR.y = value.y;
		node.value.VECTOR.z = value.z;
		node.value.VECTOR.w = value.w;
		return node;
	}

	// Token: 0x060046D1 RID: 18129 RVA: 0x0011C9E4 File Offset: 0x0011ABE4
	private static UIPanelMaterialPropertyBlock.Node NewNode(UIPanelMaterialPropertyBlock block, int prop, ref Color value)
	{
		UIPanelMaterialPropertyBlock.Node node = UIPanelMaterialPropertyBlock.NewNode(block, prop, UIPanelMaterialPropertyBlock.PropType.Color);
		node.value.COLOR.r = value.r;
		node.value.COLOR.g = value.g;
		node.value.COLOR.b = value.b;
		node.value.COLOR.a = value.a;
		return node;
	}

	// Token: 0x060046D2 RID: 18130 RVA: 0x0011CA54 File Offset: 0x0011AC54
	private static UIPanelMaterialPropertyBlock.Node NewNode(UIPanelMaterialPropertyBlock block, int prop, ref float value)
	{
		UIPanelMaterialPropertyBlock.Node node = UIPanelMaterialPropertyBlock.NewNode(block, prop, UIPanelMaterialPropertyBlock.PropType.Float);
		node.value.FLOAT = value;
		return node;
	}

	// Token: 0x060046D3 RID: 18131 RVA: 0x0011CA78 File Offset: 0x0011AC78
	private static UIPanelMaterialPropertyBlock.Node NewNode(UIPanelMaterialPropertyBlock block, int prop, ref Matrix4x4 value)
	{
		UIPanelMaterialPropertyBlock.Node node = UIPanelMaterialPropertyBlock.NewNode(block, prop, UIPanelMaterialPropertyBlock.PropType.Matrix);
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

	// Token: 0x060046D4 RID: 18132 RVA: 0x0011CBF0 File Offset: 0x0011ADF0
	public void Set(string property, Color value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x060046D5 RID: 18133 RVA: 0x0011CC00 File Offset: 0x0011AE00
	public void Set(string property, Vector4 value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x060046D6 RID: 18134 RVA: 0x0011CC10 File Offset: 0x0011AE10
	public void Set(string property, float value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x060046D7 RID: 18135 RVA: 0x0011CC20 File Offset: 0x0011AE20
	public void Set(string property, Matrix4x4 value)
	{
		this.Set(Shader.PropertyToID(property), value);
	}

	// Token: 0x060046D8 RID: 18136 RVA: 0x0011CC30 File Offset: 0x0011AE30
	public void Set(int property, Color value)
	{
		UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x060046D9 RID: 18137 RVA: 0x0011CC3C File Offset: 0x0011AE3C
	public void Set(int property, Vector4 value)
	{
		UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x060046DA RID: 18138 RVA: 0x0011CC48 File Offset: 0x0011AE48
	public void Set(int property, float value)
	{
		UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x060046DB RID: 18139 RVA: 0x0011CC54 File Offset: 0x0011AE54
	public void Set(int property, Matrix4x4 value)
	{
		UIPanelMaterialPropertyBlock.NewNode(this, property, ref value);
	}

	// Token: 0x060046DC RID: 18140 RVA: 0x0011CC60 File Offset: 0x0011AE60
	public void Clear()
	{
		if (this.count > 0)
		{
			this.first.prev = UIPanelMaterialPropertyBlock.dump;
			UIPanelMaterialPropertyBlock.dump = this.last;
			if (UIPanelMaterialPropertyBlock.dumpCount > 0)
			{
				this.first.prev.next = this.first;
				this.first.prev.hasNext = true;
				this.first.hasPrev = true;
			}
			this.first = (this.last = null);
			UIPanelMaterialPropertyBlock.dumpCount += this.count;
			this.count = 0;
		}
	}

	// Token: 0x060046DD RID: 18141 RVA: 0x0011CCFC File Offset: 0x0011AEFC
	public void AddToMaterialPropertyBlock(MaterialPropertyBlock block)
	{
		UIPanelMaterialPropertyBlock.Node next = this.first;
		int num = this.count;
		while (num-- > 0)
		{
			switch (next.type)
			{
			case UIPanelMaterialPropertyBlock.PropType.Float:
				block.AddFloat(next.property, next.value.FLOAT);
				break;
			case UIPanelMaterialPropertyBlock.PropType.Vector:
				block.AddVector(next.property, next.value.VECTOR);
				break;
			case UIPanelMaterialPropertyBlock.PropType.Color:
				block.AddColor(next.property, next.value.COLOR);
				break;
			case UIPanelMaterialPropertyBlock.PropType.Matrix:
				block.AddMatrix(next.property, next.value.MATRIX);
				break;
			}
			next = next.next;
		}
	}

	// Token: 0x040026E6 RID: 9958
	private UIPanelMaterialPropertyBlock.Node first;

	// Token: 0x040026E7 RID: 9959
	private UIPanelMaterialPropertyBlock.Node last;

	// Token: 0x040026E8 RID: 9960
	private int count;

	// Token: 0x040026E9 RID: 9961
	private static UIPanelMaterialPropertyBlock.Node dump;

	// Token: 0x040026EA RID: 9962
	private static int dumpCount;

	// Token: 0x020007A7 RID: 1959
	private enum PropType : byte
	{
		// Token: 0x040026EC RID: 9964
		Float,
		// Token: 0x040026ED RID: 9965
		Vector,
		// Token: 0x040026EE RID: 9966
		Color,
		// Token: 0x040026EF RID: 9967
		Matrix
	}

	// Token: 0x020007A8 RID: 1960
	[StructLayout(LayoutKind.Explicit, Size = 64)]
	private struct PropValue
	{
		// Token: 0x040026F0 RID: 9968
		[FieldOffset(0)]
		public Color COLOR;

		// Token: 0x040026F1 RID: 9969
		[FieldOffset(0)]
		public float FLOAT;

		// Token: 0x040026F2 RID: 9970
		[FieldOffset(0)]
		public Vector4 VECTOR;

		// Token: 0x040026F3 RID: 9971
		[FieldOffset(0)]
		public Matrix4x4 MATRIX;
	}

	// Token: 0x020007A9 RID: 1961
	private class Node
	{
		// Token: 0x040026F4 RID: 9972
		public UIPanelMaterialPropertyBlock.Node prev;

		// Token: 0x040026F5 RID: 9973
		public UIPanelMaterialPropertyBlock.Node next;

		// Token: 0x040026F6 RID: 9974
		public int property;

		// Token: 0x040026F7 RID: 9975
		public UIPanelMaterialPropertyBlock.PropValue value;

		// Token: 0x040026F8 RID: 9976
		public UIPanelMaterialPropertyBlock.PropType type;

		// Token: 0x040026F9 RID: 9977
		public bool hasNext;

		// Token: 0x040026FA RID: 9978
		public bool hasPrev;

		// Token: 0x040026FB RID: 9979
		public bool disposed;
	}
}
