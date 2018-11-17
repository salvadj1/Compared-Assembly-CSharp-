using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x0200061A RID: 1562
public class MatrixHelper : MonoBehaviour
{
	// Token: 0x0600378E RID: 14222 RVA: 0x000CA354 File Offset: 0x000C8554
	public static bool Project(ref Vector3 obj, ref Matrix4x4 modelview, ref Matrix4x4 projection, ref Vector4 viewport, out Vector3 windowCoordinate)
	{
		Vector4 vector;
		vector.x = modelview.m00 * obj.x + modelview.m10 * obj.y + modelview.m20 * obj.z + modelview.m30;
		vector.y = modelview.m01 * obj.x + modelview.m11 * obj.y + modelview.m21 * obj.z + modelview.m31;
		vector.z = modelview.m02 * obj.x + modelview.m12 * obj.y + modelview.m22 * obj.z + modelview.m32;
		vector.w = modelview.m03 * obj.x + modelview.m13 * obj.y + modelview.m23 * obj.z + modelview.m33;
		Vector4 vector2;
		vector2.x = projection.m00 * vector.x + projection.m10 * vector.y + projection.m20 * vector.z + projection.m30 * vector.w;
		vector2.y = projection.m01 * vector.x + projection.m11 * vector.y + projection.m21 * vector.z + projection.m31 * vector.w;
		vector2.z = projection.m02 * vector.x + projection.m12 * vector.y + projection.m22 * vector.z + projection.m32 * vector.w;
		vector2.w = -vector.z;
		if ((double)vector2.w == 0.0)
		{
			windowCoordinate = default(Vector3);
			return false;
		}
		vector2.w = 1f / vector2.w;
		vector2.x *= vector2.w;
		vector2.y *= vector2.w;
		vector2.z *= vector2.w;
		windowCoordinate.x = (vector2.x * 0.5f + 0.5f) * viewport.z + viewport.x;
		windowCoordinate.y = (vector2.y * 0.5f + 0.5f) * viewport.w + viewport.y;
		windowCoordinate.z = 1f - vector2.z;
		return true;
	}

	// Token: 0x0600378F RID: 14223 RVA: 0x000CA5F4 File Offset: 0x000C87F4
	public static bool UnProject(ref Vector3 win, ref Matrix4x4 modelview, ref Matrix4x4 projection, ref Vector4 viewport, out Vector3 objectCoordinate)
	{
		Matrix4x4 matrix4x = projection * modelview;
		Matrix4x4 matrix4x2;
		if (!MatrixHelper.InvertMatrix(ref matrix4x, out matrix4x2))
		{
			objectCoordinate = default(Vector3);
			return false;
		}
		Vector4 vector;
		vector.x = (win.x - viewport.x) / viewport.z * 2f - 1f;
		vector.y = (win.y - viewport.y) / viewport.w * 2f - 1f;
		vector.z = 1f - win.z;
		vector.w = 1f;
		Vector4 vector2;
		MatrixHelper.MultiplyVector4(out vector2, ref matrix4x2, ref vector);
		if ((double)vector2.w == 0.0)
		{
			objectCoordinate = default(Vector3);
			return false;
		}
		vector2.w = 1f / vector2.w;
		objectCoordinate.x = vector2.x * vector2.w;
		objectCoordinate.y = vector2.y * vector2.w;
		objectCoordinate.z = vector2.z * vector2.w;
		return true;
	}

	// Token: 0x06003790 RID: 14224 RVA: 0x000CA72C File Offset: 0x000C892C
	public static void MultiplyVector4(out Vector4 resultvector, ref Matrix4x4 matrix, ref Vector4 pvector)
	{
		resultvector.x = matrix[0] * pvector[0] + matrix[4] * pvector[1] + matrix[8] * pvector[2] + matrix[12] * pvector[3];
		resultvector.y = matrix[1] * pvector[0] + matrix[5] * pvector[1] + matrix[9] * pvector[2] + matrix[13] * pvector[3];
		resultvector.z = matrix[2] * pvector[0] + matrix[6] * pvector[1] + matrix[10] * pvector[2] + matrix[14] * pvector[3];
		resultvector.w = matrix[3] * pvector[0] + matrix[7] * pvector[1] + matrix[11] * pvector[2] + matrix[15] * pvector[3];
	}

	// Token: 0x06003791 RID: 14225 RVA: 0x000CA854 File Offset: 0x000C8A54
	public static bool InvertMatrix(ref Matrix4x4 m, out Matrix4x4 o)
	{
		Vector4 vector;
		vector.x = m.m00;
		vector.y = m.m01;
		vector.z = m.m02;
		vector.w = m.m03;
		Vector4 vector2;
		vector2.x = m.m10;
		vector2.y = m.m11;
		vector2.z = m.m12;
		vector2.w = m.m13;
		Vector4 vector3;
		vector3.x = m.m20;
		vector3.y = m.m21;
		vector3.z = m.m22;
		vector3.w = m.m23;
		Vector4 vector4;
		vector4.x = m.m30;
		vector4.y = m.m31;
		vector4.z = m.m32;
		vector4.w = m.m33;
		Vector4 vector5;
		Vector4 vector6;
		Vector4 vector7;
		Vector4 vector8;
		vector5.x = (vector6.y = (vector7.z = (vector8.w = 1f)));
		vector5.y = (vector5.z = (vector5.w = (vector6.x = (vector6.z = (vector6.w = (vector7.x = (vector7.y = (vector7.w = (vector8.x = (vector8.y = (vector8.z = 0f)))))))))));
		if (vector4.x * vector4.x > vector3.x * vector3.x)
		{
			Vector4 vector9 = vector4;
			vector4 = vector3;
			vector3 = vector9;
			vector9 = vector8;
			vector8 = vector7;
			vector7 = vector9;
		}
		if (vector3.x * vector3.x > vector2.x * vector2.x)
		{
			Vector4 vector9 = vector2;
			vector2 = vector3;
			vector3 = vector9;
			vector9 = vector6;
			vector6 = vector7;
			vector7 = vector9;
		}
		if (vector2.x * vector3.x > vector.x * vector.x)
		{
			Vector4 vector9 = vector2;
			vector2 = vector;
			vector = vector9;
			vector9 = vector5;
			vector5 = vector6;
			vector6 = vector9;
		}
		if ((double)vector.x == 0.0)
		{
			o = default(Matrix4x4);
			return false;
		}
		float num = vector2.x / vector.x;
		float num2 = vector3.x / vector.x;
		float num3 = vector4.x / vector.x;
		vector2.y -= num * vector.y;
		vector3.y -= num2 * vector.y;
		vector4.y -= num3 * vector.y;
		vector2.z -= num * vector.z;
		vector3.z -= num2 * vector.z;
		vector4.z -= num3 * vector.z;
		vector2.w -= num * vector.w;
		vector3.w -= num2 * vector.w;
		vector4.w -= num3 * vector.w;
		if ((double)vector5.x != 0.0)
		{
			vector6.x -= num * vector5.x;
			vector7.x -= num2 * vector5.x;
			vector8.x -= num3 * vector5.x;
		}
		if ((double)vector5.y != 0.0)
		{
			vector6.y -= num * vector5.y;
			vector7.y -= num2 * vector5.y;
			vector8.y -= num3 * vector5.y;
		}
		if ((double)vector5.z != 0.0)
		{
			vector6.z -= num * vector5.z;
			vector7.z -= num2 * vector5.z;
			vector8.z -= num3 * vector5.z;
		}
		if ((double)vector5.w != 0.0)
		{
			vector6.w -= num * vector5.w;
			vector7.w -= num2 * vector5.w;
			vector8.w -= num3 * vector5.w;
		}
		if (vector4.y * vector4.y > vector3.y * vector3.y)
		{
			Vector4 vector9 = vector4;
			vector4 = vector3;
			vector3 = vector9;
			vector9 = vector8;
			vector8 = vector7;
			vector7 = vector9;
		}
		if (vector3.y * vector3.y > vector2.y * vector2.y)
		{
			Vector4 vector9 = vector2;
			vector2 = vector3;
			vector3 = vector9;
			vector9 = vector6;
			vector6 = vector7;
			vector7 = vector9;
		}
		if ((double)vector2.y == 0.0)
		{
			o = default(Matrix4x4);
			return false;
		}
		num2 = vector3.y / vector2.y;
		num3 = vector4.y / vector2.y;
		vector3.z -= num2 * vector2.z;
		vector4.z -= num3 * vector2.z;
		vector3.w -= num2 * vector2.w;
		vector4.w -= num3 * vector2.w;
		if ((double)vector6.x != 0.0)
		{
			vector7.x -= num2 * vector6.x;
			vector8.x -= num3 * vector6.x;
		}
		if ((double)vector6.y != 0.0)
		{
			vector7.y -= num2 * vector6.y;
			vector8.y -= num3 * vector6.y;
		}
		if ((double)vector6.z != 0.0)
		{
			vector7.z -= num2 * vector6.z;
			vector8.z -= num3 * vector6.z;
		}
		if ((double)vector6.w != 0.0)
		{
			vector7.w -= num2 * vector6.w;
			vector8.w -= num3 * vector6.w;
		}
		if (vector4.y * vector4.y > vector3.y * vector3.y)
		{
			Vector4 vector9 = vector4;
			vector4 = vector3;
			vector3 = vector9;
			vector9 = vector8;
			vector8 = vector7;
			vector7 = vector9;
		}
		if ((double)vector3.z == 0.0)
		{
			o = default(Matrix4x4);
			return false;
		}
		num3 = vector4.z / vector3.z;
		vector4.w -= num3 * vector3.w;
		vector8.x -= num3 * vector7.x;
		vector8.y -= num3 * vector7.y;
		vector8.z -= num3 * vector7.z;
		vector8.w -= num3 * vector7.w;
		if ((double)vector4.w == 0.0)
		{
			o = default(Matrix4x4);
			return false;
		}
		float num4 = 1f / vector4.w;
		vector8.x *= num4;
		vector8.y *= num4;
		vector8.z *= num4;
		vector8.w *= num4;
		num2 = vector3.w;
		num4 = 1f / vector3.z;
		vector7.x = num4 * (vector7.x - vector8.x * num2);
		vector7.y = num4 * (vector7.y - vector8.y * num2);
		vector7.z = num4 * (vector7.z - vector8.z * num2);
		vector7.w = num4 * (vector7.w - vector8.w * num2);
		num = vector2.w;
		vector6.x -= vector8.x * num;
		vector6.y -= vector8.y * num;
		vector6.z -= vector8.z * num;
		vector6.w -= vector8.w * num;
		float num5 = vector.w;
		vector5.x -= vector8.x * num5;
		vector5.y -= vector8.y * num5;
		vector5.z -= vector8.z * num5;
		vector5.w -= vector8.w * num5;
		num = vector2.z;
		num4 = 1f / vector2.y;
		vector6.x = num4 * (vector6.x - vector7.x * num);
		vector6.y = num4 * (vector6.y - vector7.y * num);
		vector6.z = num4 * (vector6.z - vector7.z * num);
		vector6.w = num4 * (vector6.w - vector7.w * num);
		num5 = vector.z;
		vector5.x -= vector7.x * num5;
		vector5.y -= vector7.y * num5;
		vector5.z -= vector7.z * num5;
		vector5.w -= vector7.w * num5;
		num5 = vector.y;
		num4 = 1f / vector.x;
		vector5.x = num4 * (vector5.x - vector6.x * num5);
		vector5.y = num4 * (vector5.y - vector6.y * num5);
		vector5.z = num4 * (vector5.z - vector6.z * num5);
		vector5.w = num4 * (vector5.w - vector6.w * num5);
		o.m00 = vector5.x;
		o.m01 = vector5.y;
		o.m02 = vector5.z;
		o.m03 = vector5.w;
		o.m10 = vector6.x;
		o.m11 = vector6.y;
		o.m12 = vector6.z;
		o.m13 = vector6.w;
		o.m20 = vector7.x;
		o.m21 = vector7.y;
		o.m22 = vector7.z;
		o.m23 = vector7.w;
		o.m30 = vector8.x;
		o.m31 = vector8.y;
		o.m32 = vector8.z;
		o.m33 = vector8.w;
		return true;
	}

	// Token: 0x0200061B RID: 1563
	public struct ProjectHelper
	{
		// Token: 0x06003792 RID: 14226 RVA: 0x000CB474 File Offset: 0x000C9674
		public bool Project(ref Vector3 obj, out Vector3 windowCoordinate)
		{
			Vector4 vector;
			vector.x = this.modelview.m00 * obj.x + this.modelview.m01 * obj.y + this.modelview.m02 * obj.z + this.modelview.m03;
			vector.y = this.modelview.m10 * obj.x + this.modelview.m11 * obj.y + this.modelview.m12 * obj.z + this.modelview.m13;
			vector.z = this.modelview.m20 * obj.x + this.modelview.m21 * obj.y + this.modelview.m22 * obj.z + this.modelview.m23;
			vector.w = this.modelview.m30 * obj.x + this.modelview.m31 * obj.y + this.modelview.m32 * obj.z + this.modelview.m33;
			Vector4 vector2;
			vector2.x = this.projection.m00 * vector.x + this.projection.m01 * vector.y + this.projection.m02 * vector.z + this.projection.m03 * vector.w;
			vector2.y = this.projection.m10 * vector.x + this.projection.m11 * vector.y + this.projection.m12 * vector.z + this.projection.m13 * vector.w;
			vector2.z = this.projection.m20 * vector.x + this.projection.m21 * vector.y + this.projection.m22 * vector.z + this.projection.m23 * vector.w;
			vector2.w = -vector.z;
			if ((double)vector2.w == 0.0)
			{
				windowCoordinate = default(Vector3);
				return false;
			}
			vector2.w = 1f / vector2.w;
			vector2.x *= vector2.w;
			vector2.y *= vector2.w;
			windowCoordinate.x = (vector2.x * 0.5f + 0.5f) * this.size.x + this.offset.x;
			windowCoordinate.y = (vector2.y * 0.5f + 0.5f) * this.size.y + this.offset.y;
			windowCoordinate.z = vector2.z;
			return true;
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000CB794 File Offset: 0x000C9994
		public bool UnProject(ref Vector3 win, out Vector3 objectCoordinate)
		{
			Matrix4x4 matrix4x = this.projection * this.modelview;
			Matrix4x4 matrix4x2;
			if (!MatrixHelper.InvertMatrix(ref matrix4x, out matrix4x2))
			{
				objectCoordinate = default(Vector3);
				return false;
			}
			Vector4 vector;
			vector.x = (win.x - this.offset.x) / this.size.x * 2f - 1f;
			vector.y = (win.y - this.offset.y) / this.size.y * 2f - 1f;
			vector.z = -win.z;
			vector.w = 1f;
			Vector4 vector2;
			MatrixHelper.MultiplyVector4(out vector2, ref matrix4x2, ref vector);
			if ((double)vector2.w == 0.0)
			{
				objectCoordinate = default(Vector3);
				return false;
			}
			vector2.w = 1f / vector2.w;
			objectCoordinate.x = vector2.x * vector2.w;
			objectCoordinate.y = vector2.y * vector2.w;
			objectCoordinate.z = vector2.z * vector2.w;
			return true;
		}

		// Token: 0x04001BAD RID: 7085
		public Matrix4x4 modelview;

		// Token: 0x04001BAE RID: 7086
		public Matrix4x4 projection;

		// Token: 0x04001BAF RID: 7087
		public Vector2 offset;

		// Token: 0x04001BB0 RID: 7088
		public Vector2 size;
	}

	// Token: 0x0200061C RID: 1564
	public struct ProjectHelperG
	{
		// Token: 0x06003794 RID: 14228 RVA: 0x000CB8D8 File Offset: 0x000C9AD8
		public bool Project(ref Vector3G obj, out Vector3G windowCoordinate)
		{
			Vector4G vector4G;
			vector4G.x = this.modelview.m00 * obj.x + this.modelview.m01 * obj.y + this.modelview.m02 * obj.z + this.modelview.m03;
			vector4G.y = this.modelview.m10 * obj.x + this.modelview.m11 * obj.y + this.modelview.m12 * obj.z + this.modelview.m13;
			vector4G.z = this.modelview.m20 * obj.x + this.modelview.m21 * obj.y + this.modelview.m22 * obj.z + this.modelview.m23;
			vector4G.w = this.modelview.m30 * obj.x + this.modelview.m31 * obj.y + this.modelview.m32 * obj.z + this.modelview.m33;
			Vector4G vector4G2;
			vector4G2.x = this.projection.m00 * vector4G.x + this.projection.m01 * vector4G.y + this.projection.m02 * vector4G.z + this.projection.m03 * vector4G.w;
			vector4G2.y = this.projection.m10 * vector4G.x + this.projection.m11 * vector4G.y + this.projection.m12 * vector4G.z + this.projection.m13 * vector4G.w;
			vector4G2.z = this.projection.m20 * vector4G.x + this.projection.m21 * vector4G.y + this.projection.m22 * vector4G.z + this.projection.m23 * vector4G.w;
			vector4G2.w = -vector4G.z;
			if (vector4G2.w == 0.0)
			{
				windowCoordinate = default(Vector3G);
				return false;
			}
			vector4G2.w = 1.0 / vector4G2.w;
			vector4G2.x *= vector4G2.w;
			vector4G2.y *= vector4G2.w;
			windowCoordinate.x = (vector4G2.x * 0.5 + 0.5) * this.size.x + this.offset.x;
			windowCoordinate.y = (vector4G2.y * 0.5 + 0.5) * this.size.y + this.offset.y;
			windowCoordinate.z = vector4G2.z;
			return true;
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000CBC0C File Offset: 0x000C9E0C
		public bool UnProject(ref Vector3G win, out Vector3G objectCoordinate)
		{
			Matrix4x4G matrix4x4G;
			Matrix4x4G.Mult(ref this.projection, ref this.modelview, ref matrix4x4G);
			Matrix4x4G matrix4x4G2;
			if (!Matrix4x4G.Inverse(ref matrix4x4G, ref matrix4x4G2))
			{
				objectCoordinate = default(Vector3G);
				return false;
			}
			Vector4G vector4G;
			vector4G.x = (win.x - this.offset.x) / this.size.x * 2.0 - 1.0;
			vector4G.y = (win.y - this.offset.y) / this.size.y * 2.0 - 1.0;
			vector4G.z = -win.z;
			vector4G.w = 1.0;
			Vector4G vector4G2;
			Matrix4x4G.Mult(ref vector4G, ref matrix4x4G2, ref vector4G2);
			if (vector4G2.w == 0.0)
			{
				objectCoordinate = default(Vector3G);
				return false;
			}
			vector4G2.w = 1.0 / vector4G2.w;
			objectCoordinate.x = vector4G2.x * vector4G2.w;
			objectCoordinate.y = vector4G2.y * vector4G2.w;
			objectCoordinate.z = vector4G2.z * vector4G2.w;
			return true;
		}

		// Token: 0x04001BB1 RID: 7089
		public Matrix4x4G modelview;

		// Token: 0x04001BB2 RID: 7090
		public Matrix4x4G projection;

		// Token: 0x04001BB3 RID: 7091
		public Vector2G offset;

		// Token: 0x04001BB4 RID: 7092
		public Vector2G size;
	}
}
