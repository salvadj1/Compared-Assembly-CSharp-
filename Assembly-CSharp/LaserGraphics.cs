using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000606 RID: 1542
[AddComponentMenu("")]
[ExecuteInEditMode]
public sealed class LaserGraphics : MonoBehaviour
{
	// Token: 0x060036F9 RID: 14073 RVA: 0x000C5DD0 File Offset: 0x000C3FD0
	private static void UpdateBeam(ref LaserBeam.FrameData frame, LaserBeam beam)
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
		LaserGraphics.allBeamsMask |= frame.beamsLayer;
	}

	// Token: 0x060036FA RID: 14074 RVA: 0x000C6110 File Offset: 0x000C4310
	public static void EnsureGraphicsExist()
	{
		if (!LaserGraphics.singleton)
		{
			GameObject gameObject = GameObject.Find("__LASER_GRAPHICS__");
			if (!gameObject)
			{
				gameObject = new GameObject
				{
					hideFlags = 12,
					name = "__LASER_GRAPHICS__"
				};
				LaserGraphics.singleton = gameObject.AddComponent<LaserGraphics>();
				LaserGraphics.singleton.hideFlags = 12;
			}
			else
			{
				LaserGraphics.singleton = gameObject.GetComponent<LaserGraphics>();
				if (!LaserGraphics.singleton)
				{
					LaserGraphics.singleton = gameObject.AddComponent<LaserGraphics>();
					LaserGraphics.singleton.hideFlags = 12;
				}
			}
		}
	}

	// Token: 0x060036FB RID: 14075 RVA: 0x000C61AC File Offset: 0x000C43AC
	private void RenderLasers(Camera camera)
	{
		if (!this.madeLists)
		{
			this.beams = new List<LaserBeam>();
			this.willRender = new List<LaserBeam>();
			this.madeLists = true;
		}
		int cullingMask = camera.cullingMask;
		if (this.beams == null)
		{
			this.beams = new List<LaserBeam>(LaserBeam.Collect());
		}
		else
		{
			this.beams.Clear();
			this.beams.AddRange(LaserBeam.Collect());
		}
		LaserGraphics.allBeamsMask = 0;
		foreach (LaserBeam laserBeam in this.beams)
		{
			LaserGraphics.UpdateBeam(ref laserBeam.frame, laserBeam);
		}
		if ((cullingMask & LaserGraphics.allBeamsMask) != 0 && this.beams.Count > 0)
		{
			Plane[] array = GeometryUtility.CalculateFrustumPlanes(camera);
			foreach (LaserBeam laserBeam2 in this.beams)
			{
				if (laserBeam2.isViewModel || ((cullingMask & laserBeam2.frame.beamsLayer) == laserBeam2.frame.beamsLayer && GeometryUtility.TestPlanesAABB(array, laserBeam2.frame.bounds)))
				{
					this.willRender.Add(laserBeam2);
				}
			}
			if (this.willRender.Count > 0)
			{
				LaserGraphics.world2Cam = camera.worldToCameraMatrix;
				LaserGraphics.cam2World = camera.cameraToWorldMatrix;
				LaserGraphics.camProj = camera.projectionMatrix;
				try
				{
					foreach (LaserBeam laserBeam3 in this.willRender)
					{
						LaserGraphics.RenderBeam(array, camera, laserBeam3, ref laserBeam3.frame);
					}
					foreach (LaserGraphics.MeshBuffer meshBuffer in LaserGraphics.Computation.beams)
					{
						bool rebindVertexLayout = meshBuffer.Resize();
						int num = 0;
						LaserGraphics.VertexBuffer buffer = meshBuffer.buffer;
						Vector3 min;
						min.x = (min.y = (min.z = float.PositiveInfinity));
						Vector3 max;
						max.x = (max.y = (max.z = float.NegativeInfinity));
						foreach (LaserBeam laserBeam4 in meshBuffer.beams)
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
					foreach (LaserGraphics.MeshBuffer meshBuffer2 in LaserGraphics.Computation.dots)
					{
						bool flag = meshBuffer2.Resize();
						int num6 = 0;
						LaserGraphics.VertexBuffer buffer2 = meshBuffer2.buffer;
						Vector3 min2;
						min2.x = (min2.y = (min2.z = float.PositiveInfinity));
						Vector3 max2;
						max2.x = (max2.y = (max2.z = float.NegativeInfinity));
						foreach (LaserBeam laserBeam5 in meshBuffer2.beams)
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
							buffer2.t[num7] = LaserGraphics.uv[0];
							buffer2.t[num8] = LaserGraphics.uv[1];
							buffer2.t[num10] = LaserGraphics.uv[2];
							buffer2.t[num9] = LaserGraphics.uv[3];
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
							buffer2.t[num7] = LaserGraphics.uv[0];
							buffer2.t[num8] = LaserGraphics.uv[1];
							buffer2.t[num10] = LaserGraphics.uv[2];
							buffer2.t[num9] = LaserGraphics.uv[3];
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
					LaserGraphics.Computation.beams.Clear();
					LaserGraphics.Computation.dots.Clear();
					LaserGraphics.MeshBuffer.Reset();
				}
			}
		}
	}

	// Token: 0x060036FC RID: 14076 RVA: 0x000C72DC File Offset: 0x000C54DC
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

	// Token: 0x060036FD RID: 14077 RVA: 0x000C73AC File Offset: 0x000C55AC
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

	// Token: 0x060036FE RID: 14078 RVA: 0x000C747C File Offset: 0x000C567C
	private static void RenderBeam(Plane[] frustum, Camera camera, LaserBeam beam, ref LaserBeam.FrameData frame)
	{
		Vector3 vector = LaserGraphics.world2Cam.MultiplyPoint(frame.origin);
		Vector3 vector2 = LaserGraphics.world2Cam.MultiplyPoint(frame.point);
		Vector3 vector3 = vector2 - vector;
		vector3.Normalize();
		float num = 1f - (1f - Mathf.Abs(vector3.z)) * beam.beamOutput;
		Quaternion quaternion = Quaternion.LookRotation(vector3, vector2);
		Quaternion quaternion2 = Quaternion.LookRotation(vector3, vector);
		Vector3 vector4 = quaternion2 * new Vector3(frame.originWidth, 0f, 0f);
		Vector3 vector5 = quaternion * new Vector3(frame.pointWidth, 0f, 0f);
		frame.beamVertices.m0 = LaserGraphics.cam2World.MultiplyPoint(vector4 * 0.5f + vector);
		frame.beamVertices.m2 = LaserGraphics.cam2World.MultiplyPoint(vector5 * 0.5f + vector2);
		frame.beamVertices.m1 = LaserGraphics.cam2World.MultiplyPoint(vector4 * -0.5f + vector);
		frame.beamVertices.m3 = LaserGraphics.cam2World.MultiplyPoint(vector5 * -0.5f + vector2);
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
		frame.beamColor.m0 = (frame.beamColor.m1 = (frame.beamColor.m2 = (frame.beamColor.m3 = LaserGraphics.RangeBeamColor(beam.beamColor * num))));
		frame.beamUVs.m0 = LaserGraphics.uv[0];
		frame.beamUVs.m0.x = frame.beamUVs.m0.x * frame.distanceFraction;
		frame.beamUVs.m1 = LaserGraphics.uv[1];
		frame.beamUVs.m1.x = frame.beamUVs.m1.x * frame.distanceFraction;
		frame.beamUVs.m2 = LaserGraphics.uv[2];
		frame.beamUVs.m2.x = frame.beamUVs.m2.x * frame.distanceFraction;
		frame.beamUVs.m3 = LaserGraphics.uv[3];
		frame.beamUVs.m3.x = frame.beamUVs.m3.x * frame.distanceFraction;
		frame.bufBeam = LaserGraphics.MeshBuffer.ForBeamMaterial(beam.beamMaterial);
		if (LaserGraphics.Computation.beams.Add(frame.bufBeam))
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
			Vector3 vector6 = LaserGraphics.world2Cam.MultiplyVector(-frame.hitNormal);
			if (vector6.z < 0f)
			{
				Vector3 vector7 = LaserGraphics.cam2World.MultiplyPoint(Vector3.zero);
				if (!Physics.Linecast(vector7, Vector3.Lerp(vector7, frame.point, 0.95f), beam.cullLayers))
				{
					Vector3 vector8 = LaserGraphics.world2Cam.MultiplyPoint(frame.point);
					Quaternion quaternion3 = Quaternion.LookRotation(vector8, Vector3.up);
					frame.dotVertices1.m0 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, -frame.dotRadius, 0f));
					frame.dotVertices1.m1 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, frame.dotRadius, 0f));
					frame.dotVertices1.m2 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, -frame.dotRadius, 0f));
					frame.dotVertices1.m3 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, frame.dotRadius, 0f));
					quaternion3 = Quaternion.LookRotation(vector6, Vector3.up);
					frame.dotVertices2.m0 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, -frame.dotRadius, -0.01f));
					frame.dotVertices2.m1 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(frame.dotRadius, frame.dotRadius, -0.01f));
					frame.dotVertices2.m2 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, -frame.dotRadius, -0.01f));
					frame.dotVertices2.m3 = LaserGraphics.cam2World.MultiplyPoint(vector8 + quaternion3 * new Vector3(-frame.dotRadius, frame.dotRadius, -0.01f));
					frame.dotColor1.m0 = (frame.dotColor1.m1 = (frame.dotColor1.m2 = (frame.dotColor1.m3 = (frame.dotColor2.m0 = (frame.dotColor2.m1 = (frame.dotColor2.m2 = (frame.dotColor2.m3 = LaserGraphics.RangeDotColor(beam.dotColor))))))));
					frame.bufDot = LaserGraphics.MeshBuffer.ForDotMaterial(beam.dotMaterial);
					if (LaserGraphics.Computation.dots.Add(frame.bufDot))
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

	// Token: 0x060036FF RID: 14079 RVA: 0x000C7BEC File Offset: 0x000C5DEC
	internal static void RenderLasersOnCamera(Camera camera)
	{
		if (LaserGraphics.singleton)
		{
			LaserGraphics.singleton.RenderLasers(camera);
		}
	}

	// Token: 0x04001B2D RID: 6957
	private const float kNormalPushBack = -0.01f;

	// Token: 0x04001B2E RID: 6958
	private const float kDotMaxAlpha = 12f;

	// Token: 0x04001B2F RID: 6959
	private const float kBeamMaxAlpha = 1f;

	// Token: 0x04001B30 RID: 6960
	private const string singletonName = "__LASER_GRAPHICS__";

	// Token: 0x04001B31 RID: 6961
	[NonSerialized]
	private List<LaserBeam> beams;

	// Token: 0x04001B32 RID: 6962
	[NonSerialized]
	private List<LaserBeam> willRender;

	// Token: 0x04001B33 RID: 6963
	private static int allBeamsMask;

	// Token: 0x04001B34 RID: 6964
	private static Matrix4x4 world2Cam;

	// Token: 0x04001B35 RID: 6965
	private static Matrix4x4 cam2World;

	// Token: 0x04001B36 RID: 6966
	private static Matrix4x4 camProj;

	// Token: 0x04001B37 RID: 6967
	private static readonly Vector2[] uv = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 0f),
		new Vector2(1f, 1f)
	};

	// Token: 0x04001B38 RID: 6968
	[NonSerialized]
	private bool madeLists;

	// Token: 0x04001B39 RID: 6969
	private static LaserGraphics singleton;

	// Token: 0x02000607 RID: 1543
	internal class VertexBuffer
	{
		// Token: 0x06003700 RID: 14080 RVA: 0x000C7C08 File Offset: 0x000C5E08
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

		// Token: 0x06003701 RID: 14081 RVA: 0x000C7CAC File Offset: 0x000C5EAC
		public static LaserGraphics.VertexBuffer Size(int i)
		{
			LaserGraphics.VertexBuffer result;
			if (!LaserGraphics.VertexBuffer.Register.all.TryGetValue(i, out result))
			{
				LaserGraphics.VertexBuffer.Register.all.Add(i, result = new LaserGraphics.VertexBuffer(i));
			}
			return result;
		}

		// Token: 0x04001B3A RID: 6970
		public readonly int quadCount;

		// Token: 0x04001B3B RID: 6971
		public readonly int vertexCount;

		// Token: 0x04001B3C RID: 6972
		public readonly Vector3[] v;

		// Token: 0x04001B3D RID: 6973
		public readonly Vector2[] t;

		// Token: 0x04001B3E RID: 6974
		public readonly Vector3[] n;

		// Token: 0x04001B3F RID: 6975
		public readonly Color[] c;

		// Token: 0x04001B40 RID: 6976
		public readonly int[] i;

		// Token: 0x02000608 RID: 1544
		private static class Register
		{
			// Token: 0x04001B41 RID: 6977
			public static readonly Dictionary<int, LaserGraphics.VertexBuffer> all = new Dictionary<int, LaserGraphics.VertexBuffer>();
		}
	}

	// Token: 0x02000609 RID: 1545
	internal sealed class MeshBuffer : IDisposable, IEquatable<LaserGraphics.MeshBuffer>
	{
		// Token: 0x06003703 RID: 14083 RVA: 0x000C7CEC File Offset: 0x000C5EEC
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

		// Token: 0x06003704 RID: 14084 RVA: 0x000C7D3C File Offset: 0x000C5F3C
		public bool Resize()
		{
			return this.SetSize(this.measureSize);
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000C7D4C File Offset: 0x000C5F4C
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
				this.buffer = LaserGraphics.VertexBuffer.Size(size);
			}
			this.quadCount = size;
			return true;
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000C7D90 File Offset: 0x000C5F90
		public void Dispose()
		{
			if (this.mesh)
			{
				Object.DestroyImmediate(this.mesh);
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000C7DB0 File Offset: 0x000C5FB0
		public override int GetHashCode()
		{
			return this.instanceID;
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000C7DB8 File Offset: 0x000C5FB8
		public override bool Equals(object obj)
		{
			return obj is LaserGraphics.MeshBuffer && this.instanceID == ((LaserGraphics.MeshBuffer)obj).instanceID;
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000C7DDC File Offset: 0x000C5FDC
		public bool Equals(LaserGraphics.MeshBuffer buf)
		{
			return !object.ReferenceEquals(buf, null) && this.instanceID == buf.instanceID;
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000C7DFC File Offset: 0x000C5FFC
		private static LaserGraphics.MeshBuffer ForMaterial(Dictionary<Material, LaserGraphics.MeshBuffer> all, Material material)
		{
			LaserGraphics.MeshBuffer meshBuffer;
			if (!all.TryGetValue(material, out meshBuffer))
			{
				meshBuffer = new LaserGraphics.MeshBuffer(material);
				all.Add(material, meshBuffer);
			}
			return meshBuffer;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000C7E28 File Offset: 0x000C6028
		public static LaserGraphics.MeshBuffer ForBeamMaterial(Material material)
		{
			if (!LaserGraphics.MeshBuffer.Register.hasBeam || LaserGraphics.MeshBuffer.Register.lastBeam.material != material)
			{
				LaserGraphics.MeshBuffer.Register.lastBeam = LaserGraphics.MeshBuffer.ForMaterial(LaserGraphics.MeshBuffer.Register.beams, material);
				LaserGraphics.MeshBuffer.Register.hasBeam = true;
			}
			return LaserGraphics.MeshBuffer.Register.lastBeam;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000C7E70 File Offset: 0x000C6070
		public static LaserGraphics.MeshBuffer ForDotMaterial(Material material)
		{
			if (!LaserGraphics.MeshBuffer.Register.hasDot || LaserGraphics.MeshBuffer.Register.lastDot.material != material)
			{
				LaserGraphics.MeshBuffer.Register.lastDot = LaserGraphics.MeshBuffer.ForMaterial(LaserGraphics.MeshBuffer.Register.dots, material);
				LaserGraphics.MeshBuffer.Register.hasDot = true;
			}
			return LaserGraphics.MeshBuffer.Register.lastDot;
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000C7EB8 File Offset: 0x000C60B8
		public static void Reset()
		{
			LaserGraphics.MeshBuffer.Register.lastDot = (LaserGraphics.MeshBuffer.Register.lastBeam = null);
			LaserGraphics.MeshBuffer.Register.hasDot = (LaserGraphics.MeshBuffer.Register.hasBeam = false);
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000C7ED4 File Offset: 0x000C60D4
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

		// Token: 0x04001B42 RID: 6978
		public Mesh mesh;

		// Token: 0x04001B43 RID: 6979
		public readonly Material material;

		// Token: 0x04001B44 RID: 6980
		private int quadCount;

		// Token: 0x04001B45 RID: 6981
		internal LaserGraphics.VertexBuffer buffer;

		// Token: 0x04001B46 RID: 6982
		public int measureSize;

		// Token: 0x04001B47 RID: 6983
		private readonly int instanceID;

		// Token: 0x04001B48 RID: 6984
		public readonly List<LaserBeam> beams = new List<LaserBeam>();

		// Token: 0x0200060A RID: 1546
		private static class Register
		{
			// Token: 0x04001B49 RID: 6985
			public static readonly Dictionary<Material, LaserGraphics.MeshBuffer> beams = new Dictionary<Material, LaserGraphics.MeshBuffer>();

			// Token: 0x04001B4A RID: 6986
			public static readonly Dictionary<Material, LaserGraphics.MeshBuffer> dots = new Dictionary<Material, LaserGraphics.MeshBuffer>();

			// Token: 0x04001B4B RID: 6987
			public static LaserGraphics.MeshBuffer lastBeam;

			// Token: 0x04001B4C RID: 6988
			public static LaserGraphics.MeshBuffer lastDot;

			// Token: 0x04001B4D RID: 6989
			public static bool hasBeam;

			// Token: 0x04001B4E RID: 6990
			public static bool hasDot;
		}
	}

	// Token: 0x0200060B RID: 1547
	private static class Computation
	{
		// Token: 0x04001B4F RID: 6991
		public static readonly HashSet<LaserGraphics.MeshBuffer> beams = new HashSet<LaserGraphics.MeshBuffer>();

		// Token: 0x04001B50 RID: 6992
		public static readonly HashSet<LaserGraphics.MeshBuffer> dots = new HashSet<LaserGraphics.MeshBuffer>();
	}
}
