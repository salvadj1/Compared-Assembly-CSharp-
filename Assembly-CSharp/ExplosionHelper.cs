using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.MeshBatch;
using Facepunch.MeshBatch.Extensions;
using UnityEngine;

// Token: 0x0200058E RID: 1422
public static class ExplosionHelper
{
	// Token: 0x06002E56 RID: 11862 RVA: 0x000B1C18 File Offset: 0x000AFE18
	public static global::ExplosionHelper.Surface[] OverlapExplosion(Vector3 point, float explosionRadius, int findLayerMask = -1, int occludingLayerMask = -1, IDMain ignore = null)
	{
		global::ExplosionHelper.Point point2 = new global::ExplosionHelper.Point(point, explosionRadius, findLayerMask, occludingLayerMask, ignore);
		return point2.ToArray();
	}

	// Token: 0x06002E57 RID: 11863 RVA: 0x000B1C3C File Offset: 0x000AFE3C
	public static global::ExplosionHelper.Surface[] OverlapExplosionSorted(Vector3 point, float explosionRadius, int findLayerMask = -1, int occludingLayerMask = -1, IDMain ignore = null)
	{
		global::ExplosionHelper.Surface[] array = global::ExplosionHelper.OverlapExplosion(point, explosionRadius, findLayerMask, occludingLayerMask, ignore);
		if (array.Length > 1)
		{
			Array.Sort<global::ExplosionHelper.Surface>(array);
		}
		return array;
	}

	// Token: 0x06002E58 RID: 11864 RVA: 0x000B1C68 File Offset: 0x000AFE68
	public static global::ExplosionHelper.Surface[] OverlapExplosionUnique(Vector3 point, float explosionRadius, int findLayerMask = -1, int occludingLayerMask = -1, IDMain ignore = null)
	{
		global::ExplosionHelper.Surface[] array = global::ExplosionHelper.OverlapExplosion(point, explosionRadius, findLayerMask, occludingLayerMask, ignore);
		int num = array.Length;
		if (num > 1)
		{
			Array.Sort<global::ExplosionHelper.Surface>(array);
			if (global::ExplosionHelper.Unique.Filter(array, ref num))
			{
				Array.Resize<global::ExplosionHelper.Surface>(ref array, num);
			}
		}
		return array;
	}

	// Token: 0x040018E4 RID: 6372
	private const float kMaxZero = 1E-05f;

	// Token: 0x0200058F RID: 1423
	public struct Surface : IEquatable<global::ExplosionHelper.Surface>, IComparable<global::ExplosionHelper.Surface>
	{
		// Token: 0x06002E59 RID: 11865 RVA: 0x000B1CA8 File Offset: 0x000AFEA8
		public override bool Equals(object obj)
		{
			return obj is global::ExplosionHelper.Surface && this.Equals((global::ExplosionHelper.Surface)obj);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000B1CC4 File Offset: 0x000AFEC4
		public bool Equals(global::ExplosionHelper.Surface surface)
		{
			return this.blocked == surface.blocked && this.bounds == surface.bounds && this.idBase == surface.idBase && this.idMain == surface.idMain && this.work.Equals(ref surface.work);
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000B1D40 File Offset: 0x000AFF40
		public bool Equals(ref global::ExplosionHelper.Surface surface)
		{
			return this.blocked == surface.blocked && this.bounds == surface.bounds && this.idBase == surface.idBase && this.idMain == surface.idMain && this.work.Equals(ref surface.work);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000B1DB4 File Offset: 0x000AFFB4
		public override string ToString()
		{
			return "Surface";
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000B1DBC File Offset: 0x000AFFBC
		public override int GetHashCode()
		{
			return this.bounds.GetHashCode() ^ ((!this.idBase) ? 0 : this.idBase.GetHashCode());
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000B1DF8 File Offset: 0x000AFFF8
		public int CompareTo(global::ExplosionHelper.Surface other)
		{
			int num = this.blocked.CompareTo(other.blocked);
			if (num == 0)
			{
				num = this.work.distanceToCenter.CompareTo(other.work.distanceToCenter);
				if (num == 0)
				{
					num = this.work.boundsSquareDistance.CompareTo(other.work.squareDistanceToCenter);
					if (num == 0)
					{
						num = this.work.rayDistance.CompareTo(other.work.rayDistance);
					}
				}
			}
			return num;
		}

		// Token: 0x040018E5 RID: 6373
		public IDBase idBase;

		// Token: 0x040018E6 RID: 6374
		public IDMain idMain;

		// Token: 0x040018E7 RID: 6375
		public Bounds bounds;

		// Token: 0x040018E8 RID: 6376
		public global::ExplosionHelper.Work work;

		// Token: 0x040018E9 RID: 6377
		public bool blocked;
	}

	// Token: 0x02000590 RID: 1424
	public struct Work
	{
		// Token: 0x06002E5F RID: 11871 RVA: 0x000B1E84 File Offset: 0x000B0084
		public bool Equals(ref global::ExplosionHelper.Work w)
		{
			return this.squareDistanceToCenter == w.squareDistanceToCenter && this.boundsSquareDistance == w.boundsSquareDistance && this.boundsExtentSquareMagnitude == w.boundsExtentSquareMagnitude && this.distanceToCenter == w.distanceToCenter && ((!this.rayTest) ? (!w.rayTest) : (w.rayTest && this.squareRayDistance == w.squareRayDistance && this.rayDistance == w.rayDistance && this.rayDir == w.rayDir)) && this.center == w.center && this.boundsExtent == w.boundsExtent;
		}

		// Token: 0x040018EA RID: 6378
		public Vector3 center;

		// Token: 0x040018EB RID: 6379
		public Vector3 rayDir;

		// Token: 0x040018EC RID: 6380
		public Vector3 boundsExtent;

		// Token: 0x040018ED RID: 6381
		public float boundsExtentSquareMagnitude;

		// Token: 0x040018EE RID: 6382
		public float boundsSquareDistance;

		// Token: 0x040018EF RID: 6383
		public float distanceToCenter;

		// Token: 0x040018F0 RID: 6384
		public float squareDistanceToCenter;

		// Token: 0x040018F1 RID: 6385
		public float rayDistance;

		// Token: 0x040018F2 RID: 6386
		public float squareRayDistance;

		// Token: 0x040018F3 RID: 6387
		public bool rayTest;
	}

	// Token: 0x02000591 RID: 1425
	public struct Point : IEnumerable, IEnumerable<global::ExplosionHelper.Surface>
	{
		// Token: 0x06002E60 RID: 11872 RVA: 0x000B1F60 File Offset: 0x000B0160
		public Point(Vector3 point, float blastRadius, int overlapLayerMask, int raycastLayerMask, IDMain skip)
		{
			this.point = point;
			this.blastRadius = blastRadius;
			this.overlapLayerMask = overlapLayerMask;
			this.raycastLayerMask = raycastLayerMask;
			this.skip = skip;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000B1F88 File Offset: 0x000B0188
		IEnumerator<global::ExplosionHelper.Surface> IEnumerable<global::ExplosionHelper.Surface>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000B1F98 File Offset: 0x000B0198
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000B1FA8 File Offset: 0x000B01A8
		private bool BoundsWork(ref Bounds bounds, ref global::ExplosionHelper.Work w)
		{
			w.boundsSquareDistance = bounds.SqrDistance(this.point);
			if (w.boundsSquareDistance > this.blastRadius * this.blastRadius)
			{
				return false;
			}
			if (w.boundsSquareDistance <= 1E-05f)
			{
				w.boundsSquareDistance = 0f;
			}
			w.center = bounds.center;
			w.rayDir.x = w.center.x - this.point.x;
			w.rayDir.y = w.center.y - this.point.y;
			w.rayDir.z = w.center.z - this.point.z;
			w.squareDistanceToCenter = w.rayDir.x * w.rayDir.x + w.rayDir.y * w.rayDir.y + w.rayDir.z * w.rayDir.z;
			if (w.squareDistanceToCenter > this.blastRadius * this.blastRadius)
			{
				return false;
			}
			if (w.squareDistanceToCenter <= 9.99999944E-11f)
			{
				w.distanceToCenter = (w.squareDistanceToCenter = 0f);
				w.rayDistance = (w.squareRayDistance = 0f);
				w.rayTest = false;
				w.boundsExtent = bounds.size;
				w.boundsExtent.x = w.boundsExtent.x * 0.5f;
				w.boundsExtent.y = w.boundsExtent.y * 0.5f;
				w.boundsExtent.z = w.boundsExtent.z * 0.5f;
				w.boundsExtentSquareMagnitude = w.boundsExtent.x * w.boundsExtent.x + w.boundsExtent.y * w.boundsExtent.y + w.boundsExtent.z * w.boundsExtent.z;
				return true;
			}
			w.distanceToCenter = Mathf.Sqrt(w.squareDistanceToCenter);
			w.boundsExtent = bounds.size;
			w.boundsExtent.x = w.boundsExtent.x * 0.5f;
			w.boundsExtent.y = w.boundsExtent.y * 0.5f;
			w.boundsExtent.z = w.boundsExtent.z * 0.5f;
			w.boundsExtentSquareMagnitude = w.boundsExtent.x * w.boundsExtent.x + w.boundsExtent.y * w.boundsExtent.y + w.boundsExtent.z * w.boundsExtent.z;
			w.squareRayDistance = w.boundsSquareDistance + w.boundsExtentSquareMagnitude;
			if (w.squareRayDistance > w.squareDistanceToCenter)
			{
				w.squareRayDistance = w.squareDistanceToCenter;
				w.rayDistance = w.distanceToCenter;
			}
			else
			{
				if (w.squareRayDistance <= 9.99999944E-11f)
				{
					w.rayDistance = (w.squareRayDistance = 0f);
					w.rayTest = false;
					return true;
				}
				w.rayDistance = Mathf.Sqrt(w.squareRayDistance);
			}
			w.rayTest = true;
			return true;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000B22F4 File Offset: 0x000B04F4
		private bool SurfaceForMeshBatchInstance(MeshBatchInstance instance, ref global::ExplosionHelper.Surface surface)
		{
			surface.idBase = instance;
			surface.idMain = surface.idBase.idMain;
			if (!surface.idMain || surface.idMain == this.skip)
			{
				surface = default(global::ExplosionHelper.Surface);
				return false;
			}
			surface.bounds = instance.physicalBounds;
			if (this.BoundsWork(ref surface.bounds, ref surface.work))
			{
				if (surface.work.rayTest)
				{
					bool flag;
					MeshBatchInstance meshBatchInstance;
					if (this.raycastLayerMask != 0 && Facepunch.MeshBatch.MeshBatchPhysics.Raycast(this.point, surface.work.rayDir, surface.work.rayDistance, this.raycastLayerMask, ref flag, ref meshBatchInstance))
					{
						if (flag && meshBatchInstance == instance)
						{
							surface.blocked = false;
						}
						else
						{
							surface.blocked = true;
						}
					}
					else
					{
						surface.blocked = false;
					}
				}
				else
				{
					surface.blocked = false;
				}
				return true;
			}
			surface = default(global::ExplosionHelper.Surface);
			return false;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000B2410 File Offset: 0x000B0610
		private bool SurfaceForCollider(Collider collider, ref global::ExplosionHelper.Surface surface)
		{
			if (!collider.enabled)
			{
				surface = default(global::ExplosionHelper.Surface);
				return false;
			}
			surface.idBase = IDBase.Get(collider);
			if (!surface.idBase)
			{
				surface = default(global::ExplosionHelper.Surface);
				return false;
			}
			surface.idMain = surface.idBase.idMain;
			if (!surface.idMain || surface.idMain == this.skip)
			{
				surface = default(global::ExplosionHelper.Surface);
				return false;
			}
			surface.bounds = collider.bounds;
			if (this.BoundsWork(ref surface.bounds, ref surface.work))
			{
				if (this.raycastLayerMask != 0)
				{
					RaycastHit raycastHit;
					surface.blocked = (surface.work.rayTest && collider.Raycast(new Ray(this.point, surface.work.rayDir), ref raycastHit, surface.work.rayDistance) && Physics.Raycast(this.point, surface.work.rayDir, ref raycastHit, raycastHit.distance, this.raycastLayerMask) && raycastHit.collider != collider);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000B2560 File Offset: 0x000B0760
		public global::ExplosionHelper.Point.Enumerator GetEnumerator()
		{
			return new global::ExplosionHelper.Point.Enumerator(ref this, false);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000B256C File Offset: 0x000B076C
		public global::ExplosionHelper.Surface[] ToArray()
		{
			global::ExplosionHelper.Point.Enumerator enumerator = new global::ExplosionHelper.Point.Enumerator(ref this, true);
			return global::ExplosionHelper.Point.EnumeratorToArray.Build(ref enumerator);
		}

		// Token: 0x040018F4 RID: 6388
		public readonly Vector3 point;

		// Token: 0x040018F5 RID: 6389
		public readonly float blastRadius;

		// Token: 0x040018F6 RID: 6390
		public readonly int overlapLayerMask;

		// Token: 0x040018F7 RID: 6391
		public readonly int raycastLayerMask;

		// Token: 0x040018F8 RID: 6392
		public readonly IDMain skip;

		// Token: 0x02000592 RID: 1426
		public struct Enumerator : IDisposable, IEnumerator, IEnumerator<global::ExplosionHelper.Surface>
		{
			// Token: 0x06002E68 RID: 11880 RVA: 0x000B258C File Offset: 0x000B078C
			internal Enumerator(ref global::ExplosionHelper.Point point, bool immediate)
			{
				this._immediate = immediate;
				this.IN = point;
				this.colliderIndex = -1;
				this.inInstanceEnumerator = false;
				this.overlapEnumerator = null;
				this.output = null;
				this.overlap = null;
				this.current = default(global::ExplosionHelper.Surface);
			}

			// Token: 0x170009EA RID: 2538
			// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000B25E0 File Offset: 0x000B07E0
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170009EB RID: 2539
			// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000B25F0 File Offset: 0x000B07F0
			public global::ExplosionHelper.Surface Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06002E6B RID: 11883 RVA: 0x000B25F8 File Offset: 0x000B07F8
			public bool MoveNext()
			{
				for (;;)
				{
					IL_00:
					while (this.inInstanceEnumerator)
					{
						if ((this._immediate || this.output) && this.overlapEnumerator.MoveNext())
						{
							MeshBatchInstance instance = this.overlapEnumerator.Current;
							if (this.IN.SurfaceForMeshBatchInstance(instance, ref this.current))
							{
								return true;
							}
						}
						else
						{
							this.overlapEnumerator.Dispose();
							this.overlapEnumerator = null;
							this.inInstanceEnumerator = false;
							this.output = null;
						}
					}
					if (this.colliderIndex++ == -1)
					{
						this.overlap = Physics.OverlapSphere(this.IN.point, this.IN.blastRadius, this.IN.overlapLayerMask);
					}
					while (this.colliderIndex < this.overlap.Length)
					{
						if (this._immediate || this.overlap[this.colliderIndex])
						{
							if (MeshBatchExtenders.GetMeshBatchPhysicalOutput<Facepunch.MeshBatch.MeshBatchPhysicalOutput>(this.overlap[this.colliderIndex], ref this.output))
							{
								this.inInstanceEnumerator = true;
								this.overlapEnumerator = this.output.EnumerateOverlapSphereInstances(this.IN.point, this.IN.blastRadius).GetEnumerator();
								goto IL_00;
							}
							if (this.IN.SurfaceForCollider(this.overlap[this.colliderIndex], ref this.current))
							{
								return true;
							}
						}
						this.colliderIndex++;
					}
					goto Block_8;
				}
				return true;
				Block_8:
				this.colliderIndex = this.overlap.Length;
				this.current = default(global::ExplosionHelper.Surface);
				return false;
			}

			// Token: 0x06002E6C RID: 11884 RVA: 0x000B27C8 File Offset: 0x000B09C8
			public void Dispose()
			{
				this.colliderIndex = -1;
				if (this.inInstanceEnumerator)
				{
					this.inInstanceEnumerator = false;
					this.overlapEnumerator.Dispose();
				}
				this.overlapEnumerator = null;
				this.output = null;
				this.overlap = null;
				this.current = default(global::ExplosionHelper.Surface);
			}

			// Token: 0x06002E6D RID: 11885 RVA: 0x000B2820 File Offset: 0x000B0A20
			public void Reset()
			{
				this.Dispose();
			}

			// Token: 0x040018F9 RID: 6393
			private readonly global::ExplosionHelper.Point IN;

			// Token: 0x040018FA RID: 6394
			private int colliderIndex;

			// Token: 0x040018FB RID: 6395
			private bool inInstanceEnumerator;

			// Token: 0x040018FC RID: 6396
			private Facepunch.MeshBatch.MeshBatchPhysicalOutput output;

			// Token: 0x040018FD RID: 6397
			private IEnumerator<MeshBatchInstance> overlapEnumerator;

			// Token: 0x040018FE RID: 6398
			private Collider[] overlap;

			// Token: 0x040018FF RID: 6399
			public global::ExplosionHelper.Surface current;

			// Token: 0x04001900 RID: 6400
			private readonly bool _immediate;
		}

		// Token: 0x02000593 RID: 1427
		private struct EnumeratorToArray
		{
			// Token: 0x06002E6E RID: 11886 RVA: 0x000B2828 File Offset: 0x000B0A28
			private void RecurseInStackHeapToArray()
			{
				if (this.enumerator.MoveNext())
				{
					global::ExplosionHelper.Surface current = this.enumerator.current;
					this.length++;
					this.RecurseInStackHeapToArray();
					this.array[--this.length] = current;
				}
				else
				{
					this.array = new global::ExplosionHelper.Surface[this.length];
				}
			}

			// Token: 0x06002E6F RID: 11887 RVA: 0x000B28A0 File Offset: 0x000B0AA0
			public static global::ExplosionHelper.Surface[] Build(ref global::ExplosionHelper.Point.Enumerator point_enumerator)
			{
				global::ExplosionHelper.Point.EnumeratorToArray enumeratorToArray;
				enumeratorToArray.enumerator = point_enumerator;
				enumeratorToArray.length = 0;
				enumeratorToArray.array = null;
				enumeratorToArray.RecurseInStackHeapToArray();
				return enumeratorToArray.array;
			}

			// Token: 0x04001901 RID: 6401
			private global::ExplosionHelper.Point.Enumerator enumerator;

			// Token: 0x04001902 RID: 6402
			private global::ExplosionHelper.Surface[] array;

			// Token: 0x04001903 RID: 6403
			private int length;
		}
	}

	// Token: 0x02000594 RID: 1428
	private static class Unique
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x000B28E4 File Offset: 0x000B0AE4
		public static bool Filter(global::ExplosionHelper.Surface[] array, ref int length)
		{
			int num = array.Length;
			try
			{
				for (int i = 0; i < num; i++)
				{
					IDMain idMain = array[i].idMain;
					if (idMain && !global::ExplosionHelper.Unique.Set.Add(idMain))
					{
						int num2 = i;
						while (++i < num)
						{
							idMain = array[i].idMain;
							if (!array[i].idMain || global::ExplosionHelper.Unique.Set.Add(idMain))
							{
								array[num2++] = array[i];
							}
						}
						length = num2;
						return true;
					}
				}
			}
			finally
			{
				global::ExplosionHelper.Unique.Set.Clear();
			}
			return false;
		}

		// Token: 0x04001904 RID: 6404
		private static readonly HashSet<IDMain> Set = new HashSet<IDMain>();
	}
}
