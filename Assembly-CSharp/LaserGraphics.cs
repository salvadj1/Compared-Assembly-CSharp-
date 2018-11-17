using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
[ExecuteInEditMode]
[AddComponentMenu("")]
public sealed class LaserGraphics : MonoBehaviour
{
	// Token: 0x06003AD1 RID: 15057 RVA: 0x000CE300 File Offset: 0x000CC500
	private static void UpdateBeam(ref global::LaserBeam.FrameData frame, global::LaserBeam beam)
	{
		Transform transform = beam.transform;
		frame.origin = transform.position;
		frame.direction = transform.forward;
		frame.direction.Normalize();
		int num = beam.beamLayers;
		RaycastHit raycastHit2;
		if (num == 0)
		{
			frame.hit = false;
		}
		else if (beam.isViewModel)
		{
			RaycastHit2 raycastHit;
			if (frame.hit = Physics2.Raycast2(frame.origin, frame.direction, ref raycastHit, beam.beamMaxDistance, num))
			{
				frame.hitPoint = raycastHit.point;
				frame.hitNormal = raycastHit.normal;
			}
		}
		else if (frame.hit = Physics.Raycast(frame.origin, frame.direction, ref raycastHit2, beam.beamMaxDistance, num))
		{
			frame.hitPoint = raycastHit2.point;
			frame.hitNormal = raycastHit2.normal;
		}
		if (!frame.hit)
		{
			frame.didHit = false;
			frame.point.x = frame.origin.x + frame.direction.x * beam.beamMaxDistance;
			frame.point.y = frame.origin.y + frame.direction.y * beam.beamMaxDistance;
			frame.point.z = frame.origin.z + frame.direction.z * beam.beamMaxDistance;
			frame.distance = beam.beamMaxDistance;
			frame.distanceFraction = 1f;
			frame.pointWidth = beam.beamWidthEnd;
		}
		else
		{
			frame.point = frame.hitPoint;
			frame.didHit = true;
			frame.distance = frame.direction.x * frame.point.x + frame.direction.y * frame.point.y + frame.direction.z * frame.point.z - (frame.direction.x * frame.origin.x + frame.direction.y * frame.origin.y + frame.direction.z * frame.origin.z);
			frame.distanceFraction = frame.distance / beam.beamMaxDistance;
			frame.pointWidth = Mathf.Lerp(beam.beamWidthStart, beam.beamWidthEnd, frame.distanceFraction);
			frame.dotRadius = Mathf.Lerp(beam.dotRadiusStart, beam.dotRadiusEnd, frame.distanceFraction);
		}
		frame.originWidth = beam.beamWidthStart;
		Vector3 vector;
		vector.x = (vector.y = (vector.z = frame.originWidth));
		frame.bounds = new Bounds(frame.origin, vector);
		vector.x = (vector.y = (vector.z = frame.pointWidth));
		frame.bounds.Encapsulate(new Bounds(frame.point, vector));
		frame.beamsLayer = 1 << beam.gameObject.layer;
		global::LaserGraphics.allBeamsMask |= frame.beamsLayer;
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x000CE640 File Offset: 0x000CC840
	public static void EnsureGraphicsExist()
	{
		if (!global::LaserGraphics.singleton)
		{
			GameObject gameObject = GameObject.Find("__LASER_GRAPHICS__");
			if (!gameObject)
			{
				gameObject = new GameObject
				{
					hideFlags = 12,
					name = "__LASER_GRAPHICS__"
				};
				global::LaserGraphics.singleton = gameObject.AddComponent<global::LaserGraphics>();
				global::LaserGraphics.singleton.hideFlags = 12;
			}
			else
			{
				global::LaserGraphics.singleton = gameObject.GetComponent<global::LaserGraphics>();
				if (!global::LaserGraphics.singleton)
				{
					global::LaserGraphics.singleton = gameObject.AddComponent<global::LaserGraphics>();
					global::LaserGraphics.singleton.hideFlags = 12;
				}
			}
		}
	}

	// Token: 0x06003AD3 RID: 15059 RVA: 0x000CE6DC File Offset: 0x000CC8DC
	private void RenderLasers(Camera camera)
	{
		if (!this.madeLists)
		{
			this.beams = new List<global::LaserBeam>();
			this.willRender = new List<global::LaserBeam>();
			this.madeLists = true;
		}
		int cullingMask = camera.cullingMask;
		if (this.beams == null)
		{
			this.beams = new List<global::LaserBeam>(global::LaserBeam.Collect());
		}
		else
		{
			this.beams.Clear();
			this.beams.AddRange(global::LaserBeam.Collect());
		}
		global::LaserGraphics.allBeamsMask = 0;
		foreach (global::LaserBeam laserBeam in this.beams)
		{
			global::LaserGraphics.UpdateBeam(ref laserBeam.frame, laserBeam);
		}
		if ((cullingMask & global::LaserGraphics.allBeamsMask) != 0 && this.beams.Count > 0)
		{
			Plane[] array = GeometryUtility.CalculateFrustumPlanes(camera);
			foreach (global::LaserBeam laserBeam2 in this.beams)
			{
				if (laserBeam2.isViewModel || ((cullingMask & laserBeam2.frame.beamsLayer) == laserBeam2.frame.beamsLayer && GeometryUtility.TestPlanesAABB(array, laserBeam2.frame.bounds)))
				{
					this.willRender.Add(laserBeam2);
				}
			}
			if (this.willRender.Count > 0)
			{
				global::LaserGraphics.world2Cam = camera.worldToCameraMatrix;
				global::LaserGraphics.cam2World = camera.cameraToWorldMatrix;
				global::LaserGraphics.camProj = camera.projectionMatrix;
				try
				{
					foreach (global::LaserBeam laserBeam3 in this.willRender)
					{
						global::LaserGraphics.RenderBeam(array, camera, laserBeam3, ref laserBeam3.frame);
					}
					foreach (global::LaserGraphics.MeshBuffer meshBuffer in global::LaserGraphics.Computation.beams)
					{
						bool rebindVertexLayout = meshBuffer.Resize();
						int num = 0;
						global::LaserGraphics.VertexBuffer buffer = meshBuffer.buffer;
						Vector3 min;
						min.x = (min.y = (min.z = float.PositiveInfinity));
						Vector3 max;
						max.x = (max.y = (max.z = float.NegativeInfinity));
						foreach (global::LaserBeam laserBeam4 in meshBuffer.beams)
						{
							int num2 = num++;
							int num3 = num++;
							int num4 = num++;
							int num5 = num++;
							buffer.v[num2] = laserBeam4.frame.beamVertices.m0;
							buffer.v[num3] = laserBeam4.frame.beamVertices.m1;
							buffer.v[num5] = laserBeam4.frame.beamVertices.m2;
							buffer.v[num4] = laserBeam4.frame.beamVertices.m3;
							buffer.n[num2] = laserBeam4.frame.beamNormals.m0;
							buffer.n[num3] = laserBeam4.frame.beamNormals.m1;
							buffer.n[num5] = laserBeam4.frame.beamNormals.m2;
							buffer.n[num4] = laserBeam4.frame.beamNormals.m3;
							buffer.c[num2] = laserBeam4.frame.beamColor.m0;
							buffer.c[num3] = laserBeam4.frame.beamColor.m1;
							buffer.c[num5] = laserBeam4.frame.beamColor.m2;
							buffer.c[num4] = laserBeam4.frame.beamColor.m3;
							buffer.t[num2] = laserBeam4.frame.beamUVs.m0;
							buffer.t[num3] = laserBeam4.frame.beamUVs.m1;
							buffer.t[num5] = laserBeam4.frame.beamUVs.m2;
							buffer.t[num4] = laserBeam4.frame.beamUVs.m3;
							for (int i = num2; i <= num4; i++)
							{
								if (buffer.v[i].x < min.x)
								{
									min.x = buffer.v[i].x;
								}
								if (buffer.v[i].x > max.x)
								{
									max.x = buffer.v[i].x;
								}
								if (buffer.v[i].y < min.y)
								{
									min.y = buffer.v[i].y;
								}
								if (buffer.v[i].y > max.y)
								{
									max.y = buffer.v[i].y;
								}
								if (buffer.v[i].z < min.z)
								{
									min.z = buffer.v[i].z;
								}
								if (buffer.v[i].z > max.z)
								{
									max.z = buffer.v[i].z;
								}
							}
							laserBeam4.frame.bufBeam = null;
						}
						meshBuffer.beams.Clear();
						meshBuffer.BindMesh(rebindVertexLayout, min, max);
						Graphics.DrawMesh(meshBuffer.mesh, Matrix4x4.identity, meshBuffer.material, 1, camera, 0, null, false, false);
					}
					foreach (global::LaserGraphics.MeshBuffer meshBuffer2 in global::LaserGraphics.Computation.dots)
					{
						bool flag = meshBuffer2.Resize();
						int num6 = 0;
						global::LaserGraphics.VertexBuffer buffer2 = meshBuffer2.buffer;
						Vector3 min2;
						min2.x = (min2.y = (min2.z = float.PositiveInfinity));
						Vector3 max2;
						max2.x = (max2.y = (max2.z = float.NegativeInfinity));
						foreach (global::LaserBeam laserBeam5 in meshBuffer2.beams)
						{
							int num7 = num6++;
							int num8 = num6++;
							int num9 = num6++;
							int num10 = num6++;
							buffer2.v[num7] = laserBeam5.frame.dotVertices1.m0;
							buffer2.v[num8] = laserBeam5.frame.dotVertices1.m1;
							buffer2.v[num10] = laserBeam5.frame.dotVertices1.m2;
							buffer2.v[num9] = laserBeam5.frame.dotVertices1.m3;
							buffer2.n[num7] = laserBeam5.frame.beamNormals.m0;
							buffer2.n[num8] = laserBeam5.frame.beamNormals.m1;
							buffer2.n[num10] = laserBeam5.frame.beamNormals.m2;
							buffer2.n[num9] = laserBeam5.frame.beamNormals.m3;
							buffer2.c[num7] = laserBeam5.frame.dotColor1.m0;
							buffer2.c[num8] = laserBeam5.frame.dotColor1.m1;
							buffer2.c[num10] = laserBeam5.frame.dotColor1.m2;
							buffer2.c[num9] = laserBeam5.frame.dotColor1.m3;
							buffer2.t[num7] = global::LaserGraphics.uv[0];
							buffer2.t[num8] = global::LaserGraphics.uv[1];
							buffer2.t[num10] = global::LaserGraphics.uv[2];
							buffer2.t[num9] = global::LaserGraphics.uv[3];
							for (int j = num7; j <= num9; j++)
							{
								if (buffer2.v[j].x < min2.x)
								{
									min2.x = buffer2.v[j].x;
								}
								if (buffer2.v[j].x > max2.x)
								{
									max2.x = buffer2.v[j].x;
								}
								if (buffer2.v[j].y < min2.y)
								{
									min2.y = buffer2.v[j].y;
								}
								if (buffer2.v[j].y > max2.y)
								{
									max2.y = buffer2.v[j].y;
								}
								if (buffer2.v[j].z < min2.z)
								{
									min2.z = buffer2.v[j].z;
								}
								if (buffer2.v[j].z > max2.z)
								{
									max2.z = buffer2.v[j].z;
								}
							}
							num7 = num6++;
							num8 = num6++;
							num9 = num6++;
							num10 = num6++;
							buffer2.v[num7] = laserBeam5.frame.dotVertices2.m0;
							buffer2.v[num8] = laserBeam5.frame.dotVertices2.m1;
							buffer2.v[num10] = laserBeam5.frame.dotVertices2.m2;
							buffer2.v[num9] = laserBeam5.frame.dotVertices2.m3;
							buffer2.n[num7] = laserBeam5.frame.beamNormals.m0;
							buffer2.n[num8] = laserBeam5.frame.beamNormals.m1;
							buffer2.n[num10] = laserBeam5.frame.beamNormals.m2;
							buffer2.n[num9] = laserBeam5.frame.beamNormals.m3;
							buffer2.c[num7] = laserBeam5.frame.dotColor2.m0;
							buffer2.c[num8] = laserBeam5.frame.dotColor2.m1;
							buffer2.c[num10] = laserBeam5.frame.dotColor2.m2;
							buffer2.c[num9] = laserBeam5.frame.dotColor2.m3;
							buffer2.t[num7] = global::LaserGraphics.uv[0];
							buffer2.t[num8] = global::LaserGraphics.uv[1];
							buffer2.t[num10] = global::LaserGraphics.uv[2];
							buffer2.t[num9] = global::LaserGraphics.uv[3];
							for (int k = num7; k <= num9; k++)
							{
								if (buffer2.v[k].x < min2.x)
								{
									min2.x = buffer2.v[k].x;
								}
								if (buffer2.v[k].x > max2.x)
								{
									max2.x = buffer2.v[k].x;
								}
								if (buffer2.v[k].y < min2.y)
								{
									min2.y = buffer2.v[k].y;
								}
								if (buffer2.v[k].y > max2.y)
								{
									max2.y = buffer2.v[k].y;
								}
								if (buffer2.v[k].z < min2.z)
								{
									min2.z = buffer2.v[k].z;
								}
								if (buffer2.v[k].z > max2.z)
								{
									max2.z = buffer2.v[k].z;
								}
							}
							laserBeam5.frame.bufDot = null;
						}
						meshBuffer2.beams.Clear();
						if (flag)
						{
							meshBuffer2.mesh.Clear(false);
							meshBuffer2.mesh.vertices = buffer2.v;
							meshBuffer2.mesh.normals = buffer2.n;
							meshBuffer2.mesh.colors = buffer2.c;
							meshBuffer2.mesh.uv = buffer2.t;
							meshBuffer2.mesh.SetIndices(buffer2.i, 2, 0);
						}
						else
						{
							meshBuffer2.mesh.vertices = buffer2.v;
							meshBuffer2.mesh.normals = buffer2.n;
							meshBuffer2.mesh.colors = buffer2.c;
							meshBuffer2.mesh.uv = buffer2.t;
						}
						meshBuffer2.BindMesh(flag, min2, max2);
						Graphics.DrawMesh(meshBuffer2.mesh, Matrix4x4.identity, meshBuffer2.material, 1, camera, 0, null, false, false);
					}
				}
				finally
				{
					this.willRender.Clear();
					global::LaserGraphics.Computation.beams.Clear();
					global::LaserGraphics.Computation.dots.Clear();
					global::LaserGraphics.MeshBuffer.Reset();
				}
			}
		}
	}

	// Token: 0x06003AD4 RID: 15060 RVA: 0x000CF80C File Offset: 0x000CDA0C
	private static Color RangeBeamColor(Color input)
	{
		float num;
		if (input.r > input.g)
		{
			if (input.b > input.r)
			{
				num = input.b;
			}
			else
			{
				num = input.r;
			}
		}
		else if (input.b > input.g)
		{
			num = input.b;
		}
		else
		{
			num = input.g;
		}
		if (num != 0f)
		{
			input.r /= num;
			input.g /= num;
			input.b /= num;
			input.a = num / 1f;
		}
		else
		{
			input.a = 1f;
		}
		return input;
	}

	// Token: 0x06003AD5 RID: 15061 RVA: 0x000CF8DC File Offset: 0x000CDADC
	private static Color RangeDotColor(Color input)
	{
		float num;
		if (input.r > input.g)
		{
			if (input.b > input.r)
			{
				num = input.b;
			}
			else
			{
				num = input.r;
			}
		}
		else if (input.b > input.g)
		{
			num = input.b;
		}
		else
		{
			num = input.g;
		}
		if (num != 0f)
		{
			input.r /= num;
			input.g /= num;
			input.b /= num;
			input.a = num / 12f;
		}
		else
		{
			input.a = 0.0833333358f;
		}
		return input;
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x000CF9AC File Offset: 0x000CDBAC
	private static void RenderBeam(Plane[] frustum, Camera camera, global::LaserBeam beam, ref global::LaserBeam.FrameData frame)
	{
		Vector3 vector = global::LaserGraphics.world2Cam.MultiplyPoint(frame.origin);
		Vector3 vector2 = global::LaserGraphics.world2Cam.MultiplyPoint(frame.point);
		Vector3 vector3 = vector2 - vector;
		vector3.Normalize();
		float num = 1f - (1f - Mathf.Abs(vector3.z)) * beam.beamOutput;
		Quaternion quaternion = Quaternion.LookRotation(vector3, vector2);
		Quaternion quaternion2 = Quaternion.LookRotation(vector3, vector);
		Vector3 vector4 = quaternion2 * new Vector3(frame.originWidth, 0f, 0f);
		Vector3 vector5 = quaternion * new Vector3(frame.pointWidth, 0f, 0f);
		frame.beamVertices.m0 = global::LaserGraphics.cam2World.MultiplyPoint(vector4 * 0.5f + vector);
		frame.beamVertices.m2 = global::LaserGraphics.cam2World.MultiplyPoint(vector5 * 0.5f + vector2);
		frame.beamVertices.m1 = global::LaserGraphics.cam2World.MultiplyPoint(vector4 * -0.5f + vector);
		frame.beamVertices.m3 = global::LaserGraphics.cam2World.MultiplyPoint(vector5 * -0.5f + vector2);
		frame.beamNormals.m0.x = frame.originWidth;
		frame.beamNormals.m2.x = frame.pointWidth;
		frame.beamNormals.m1.x = -frame.originWidth;
		frame.beamNormals.m3.x = -frame.pointWidth;
		frame.beamNormals.m0.y = -frame.distance;
		frame.beamNormals.m1.y = -frame.distance;
		frame.beamNormals.m2.y = -frame.distance;
		frame.beamNormals.m3.y = -frame.distance;
		frame.beamNormals.m0.z = (frame.beamNormals.m1.z = 0f);
		frame.beamNormals.m2.z = (frame.beamNormals.m3.z = frame.distanceFraction);
		frame.beamColor.m0 = (frame.beamColor.m1 = (frame.beamColor.m2 = (frame.beamColor.m3 = global::LaserGraphics.RangeBeamColor(beam.beamColor * num))));
		frame.beamUVs.m0 = global::LaserGraphics.uv[0];
		frame.beamUVs.m0.x = frame.beamUVs.m0.x * frame.distanceFraction;
		frame.beamUVs.m1 = global::LaserGraphics.uv[1];
		frame.beamUVs.m1.x = frame.beamUVs.m1.x * frame.distanceFraction;
		frame.beamUVs.m2 = global::LaserGraphics.uv[2];
		frame.beamUVs.m2.x = frame.beamUVs.m2.x * frame.distanceFraction;
		frame.beamUVs.m3 = global::LaserGraphics.uv[3];
		frame.beamUVs.m3.x = frame.beamUVs.m3.x * frame.distanceFraction;
		frame.bufBeam = global::LaserGraphics.MeshBuffer.ForBeamMaterial(beam.beamMaterial);
		if (global::LaserGraphics.Computation.beams.Add(frame.bufBeam))
		{
			frame.bufBeam.measureSize = 1;
		}
		else
		{
			frame.bufBeam.measureSize++;
		}
		frame.bufBeam.beams.Add(beam);
		if (frame.didHit)
		{
			Vector3 vector6 = global::LaserGraphics.world2Cam.MultiplyVector(-frame.hitNormal);
			if (vector6.z < 0f)
			{
				Vector3 vector7 = global::LaserGraphics.cam2World.MultiplyPoint(Vector3.zero);
				if (!Physics.Linecast(vector7, Vector3.Lerp(vector7, frame.point, 0.95f), beam.cullLayers))
				{
					Vector3 vector8 = global::LaserGraphics.world2Cam.MultiplyPoint(frame.point);
					Quaternion quaternion3 = Quaternion.LookRotation(vector8, Vector3.up);
					frame.dotVertices1.m0 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, -frame.dotRadius, 0f));
					frame.dotVertices1.m1 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, frame.dotRadius, 0f));
					frame.dotVertices1.m2 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, -frame.dotRadius, 0f));
					frame.dotVertices1.m3 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, frame.dotRadius, 0f));
					quaternion3 = Quaternion.LookRotation(vector6, Vector3.up);
					frame.dotVertices2.m0 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, -frame.dotRadius, -0.01f));
					frame.dotVertices2.m1 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, frame.dotRadius, -0.01f));
					frame.dotVertices2.m2 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, -frame.dotRadius, -0.01f));
					frame.dotVertices2.m3 = global::LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, frame.dotRadius, -0.01f));
					frame.dotColor1.m0 = (frame.dotColor1.m1 = (frame.dotColor1.m2 = (frame.dotColor1.m3 = (frame.dotColor2.m0 = (frame.dotColor2.m1 = (frame.dotColor2.m2 = (frame.dotColor2.m3 = global::LaserGraphics.RangeDotColor(beam.dotColor))))))));
					frame.bufDot = global::LaserGraphics.MeshBuffer.ForDotMaterial(beam.dotMaterial);
					if (global::LaserGraphics.Computation.dots.Add(frame.bufDot))
					{
						frame.bufDot.measureSize = 2;
					}
					else
					{
						frame.bufDot.measureSize += 2;
					}
					frame.bufDot.beams.Add(beam);
					frame.drawDot = true;
				}
				else
				{
					frame.bufDot = null;
					frame.drawDot = false;
				}
			}
			else
			{
				frame.bufDot = null;
				frame.drawDot = false;
			}
		}
		else
		{
			frame.bufDot = null;
			frame.drawDot = false;
		}
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x000D011C File Offset: 0x000CE31C
	internal static void RenderLasersOnCamera(Camera camera)
	{
		if (global::LaserGraphics.singleton)
		{
			global::LaserGraphics.singleton.RenderLasers(camera);
		}
	}

	// Token: 0x04001D13 RID: 7443
	private const float kNormalPushBack = -0.01f;

	// Token: 0x04001D14 RID: 7444
	private const float kDotMaxAlpha = 12f;

	// Token: 0x04001D15 RID: 7445
	private const float kBeamMaxAlpha = 1f;

	// Token: 0x04001D16 RID: 7446
	private const string singletonName = "__LASER_GRAPHICS__";

	// Token: 0x04001D17 RID: 7447
	[NonSerialized]
	private List<global::LaserBeam> beams;

	// Token: 0x04001D18 RID: 7448
	[NonSerialized]
	private List<global::LaserBeam> willRender;

	// Token: 0x04001D19 RID: 7449
	private static int allBeamsMask;

	// Token: 0x04001D1A RID: 7450
	private static Matrix4x4 world2Cam;

	// Token: 0x04001D1B RID: 7451
	private static Matrix4x4 cam2World;

	// Token: 0x04001D1C RID: 7452
	private static Matrix4x4 camProj;

	// Token: 0x04001D1D RID: 7453
	private static readonly Vector2[] uv = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 0f),
		new Vector2(1f, 1f)
	};

	// Token: 0x04001D1E RID: 7454
	[NonSerialized]
	private bool madeLists;

	// Token: 0x04001D1F RID: 7455
	private static global::LaserGraphics singleton;

	// Token: 0x020006C7 RID: 1735
	internal class VertexBuffer
	{
		// Token: 0x06003AD8 RID: 15064 RVA: 0x000D0138 File Offset: 0x000CE338
		private VertexBuffer(int quadCount)
		{
			this.quadCount = quadCount;
			this.vertexCount = quadCount * 4;
			if (this.vertexCount > 0)
			{
				this.v = new Vector3[this.vertexCount];
				this.t = new Vector2[this.vertexCount];
				this.n = new Vector3[this.vertexCount];
				this.c = new Color[this.vertexCount];
				this.i = new int[this.vertexCount];
			}
			for (int i = 0; i < this.vertexCount; i++)
			{
				this.i[i] = i;
			}
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x000D01DC File Offset: 0x000CE3DC
		public static global::LaserGraphics.VertexBuffer Size(int i)
		{
			global::LaserGraphics.VertexBuffer result;
			if (!global::LaserGraphics.VertexBuffer.Register.all.TryGetValue(i, out result))
			{
				global::LaserGraphics.VertexBuffer.Register.all.Add(i, result = new global::LaserGraphics.VertexBuffer(i));
			}
			return result;
		}

		// Token: 0x04001D20 RID: 7456
		public readonly int quadCount;

		// Token: 0x04001D21 RID: 7457
		public readonly int vertexCount;

		// Token: 0x04001D22 RID: 7458
		public readonly Vector3[] v;

		// Token: 0x04001D23 RID: 7459
		public readonly Vector2[] t;

		// Token: 0x04001D24 RID: 7460
		public readonly Vector3[] n;

		// Token: 0x04001D25 RID: 7461
		public readonly Color[] c;

		// Token: 0x04001D26 RID: 7462
		public readonly int[] i;

		// Token: 0x020006C8 RID: 1736
		private static class Register
		{
			// Token: 0x04001D27 RID: 7463
			public static readonly Dictionary<int, global::LaserGraphics.VertexBuffer> all = new Dictionary<int, global::LaserGraphics.VertexBuffer>();
		}
	}

	// Token: 0x020006C9 RID: 1737
	internal sealed class MeshBuffer : IDisposable, IEquatable<global::LaserGraphics.MeshBuffer>
	{
		// Token: 0x06003ADB RID: 15067 RVA: 0x000D021C File Offset: 0x000CE41C
		private MeshBuffer(Material material)
		{
			this.instanceID = material.GetInstanceID();
			this.mesh = new Mesh
			{
				hideFlags = 4
			};
			this.mesh.MarkDynamic();
			this.material = material;
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000D026C File Offset: 0x000CE46C
		public bool Resize()
		{
			return this.SetSize(this.measureSize);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000D027C File Offset: 0x000CE47C
		private bool SetSize(int size)
		{
			if (this.quadCount == size)
			{
				return false;
			}
			if (size == 0)
			{
				this.buffer = null;
			}
			else
			{
				this.buffer = global::LaserGraphics.VertexBuffer.Size(size);
			}
			this.quadCount = size;
			return true;
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000D02C0 File Offset: 0x000CE4C0
		public void Dispose()
		{
			if (this.mesh)
			{
				Object.DestroyImmediate(this.mesh);
			}
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000D02E0 File Offset: 0x000CE4E0
		public override int GetHashCode()
		{
			return this.instanceID;
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000D02E8 File Offset: 0x000CE4E8
		public override bool Equals(object obj)
		{
			return obj is global::LaserGraphics.MeshBuffer && this.instanceID == ((global::LaserGraphics.MeshBuffer)obj).instanceID;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x000D030C File Offset: 0x000CE50C
		public bool Equals(global::LaserGraphics.MeshBuffer buf)
		{
			return !object.ReferenceEquals(buf, null) && this.instanceID == buf.instanceID;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x000D032C File Offset: 0x000CE52C
		private static global::LaserGraphics.MeshBuffer ForMaterial(Dictionary<Material, global::LaserGraphics.MeshBuffer> all, Material material)
		{
			global::LaserGraphics.MeshBuffer meshBuffer;
			if (!all.TryGetValue(material, out meshBuffer))
			{
				meshBuffer = new global::LaserGraphics.MeshBuffer(material);
				all.Add(material, meshBuffer);
			}
			return meshBuffer;
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x000D0358 File Offset: 0x000CE558
		public static global::LaserGraphics.MeshBuffer ForBeamMaterial(Material material)
		{
			if (!global::LaserGraphics.MeshBuffer.Register.hasBeam || global::LaserGraphics.MeshBuffer.Register.lastBeam.material != material)
			{
				global::LaserGraphics.MeshBuffer.Register.lastBeam = global::LaserGraphics.MeshBuffer.ForMaterial(global::LaserGraphics.MeshBuffer.Register.beams, material);
				global::LaserGraphics.MeshBuffer.Register.hasBeam = true;
			}
			return global::LaserGraphics.MeshBuffer.Register.lastBeam;
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x000D03A0 File Offset: 0x000CE5A0
		public static global::LaserGraphics.MeshBuffer ForDotMaterial(Material material)
		{
			if (!global::LaserGraphics.MeshBuffer.Register.hasDot || global::LaserGraphics.MeshBuffer.Register.lastDot.material != material)
			{
				global::LaserGraphics.MeshBuffer.Register.lastDot = global::LaserGraphics.MeshBuffer.ForMaterial(global::LaserGraphics.MeshBuffer.Register.dots, material);
				global::LaserGraphics.MeshBuffer.Register.hasDot = true;
			}
			return global::LaserGraphics.MeshBuffer.Register.lastDot;
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x000D03E8 File Offset: 0x000CE5E8
		public static void Reset()
		{
			global::LaserGraphics.MeshBuffer.Register.lastDot = (global::LaserGraphics.MeshBuffer.Register.lastBeam = null);
			global::LaserGraphics.MeshBuffer.Register.hasDot = (global::LaserGraphics.MeshBuffer.Register.hasBeam = false);
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x000D0404 File Offset: 0x000CE604
		public void BindMesh(bool rebindVertexLayout, Vector3 min, Vector3 max)
		{
			if (rebindVertexLayout)
			{
				this.mesh.Clear(false);
				this.mesh.vertices = this.buffer.v;
				this.mesh.normals = this.buffer.n;
				this.mesh.colors = this.buffer.c;
				this.mesh.uv = this.buffer.t;
				this.mesh.SetIndices(this.buffer.i, 2, 0);
			}
			else
			{
				this.mesh.vertices = this.buffer.v;
				this.mesh.normals = this.buffer.n;
				this.mesh.colors = this.buffer.c;
				this.mesh.uv = this.buffer.t;
			}
			Bounds bounds;
			bounds..ctor(Vector3.zero, Vector3.zero);
			bounds.SetMinMax(min, max);
			this.mesh.bounds = bounds;
		}

		// Token: 0x04001D28 RID: 7464
		public Mesh mesh;

		// Token: 0x04001D29 RID: 7465
		public readonly Material material;

		// Token: 0x04001D2A RID: 7466
		private int quadCount;

		// Token: 0x04001D2B RID: 7467
		internal global::LaserGraphics.VertexBuffer buffer;

		// Token: 0x04001D2C RID: 7468
		public int measureSize;

		// Token: 0x04001D2D RID: 7469
		private readonly int instanceID;

		// Token: 0x04001D2E RID: 7470
		public readonly List<global::LaserBeam> beams = new List<global::LaserBeam>();

		// Token: 0x020006CA RID: 1738
		private static class Register
		{
			// Token: 0x04001D2F RID: 7471
			public static readonly Dictionary<Material, global::LaserGraphics.MeshBuffer> beams = new Dictionary<Material, global::LaserGraphics.MeshBuffer>();

			// Token: 0x04001D30 RID: 7472
			public static readonly Dictionary<Material, global::LaserGraphics.MeshBuffer> dots = new Dictionary<Material, global::LaserGraphics.MeshBuffer>();

			// Token: 0x04001D31 RID: 7473
			public static global::LaserGraphics.MeshBuffer lastBeam;

			// Token: 0x04001D32 RID: 7474
			public static global::LaserGraphics.MeshBuffer lastDot;

			// Token: 0x04001D33 RID: 7475
			public static bool hasBeam;

			// Token: 0x04001D34 RID: 7476
			public static bool hasDot;
		}
	}

	// Token: 0x020006CB RID: 1739
	private static class Computation
	{
		// Token: 0x04001D35 RID: 7477
		public static readonly HashSet<global::LaserGraphics.MeshBuffer> beams = new HashSet<global::LaserGraphics.MeshBuffer>();

		// Token: 0x04001D36 RID: 7478
		public static readonly HashSet<global::LaserGraphics.MeshBuffer> dots = new HashSet<global::LaserGraphics.MeshBuffer>();
	}
}
