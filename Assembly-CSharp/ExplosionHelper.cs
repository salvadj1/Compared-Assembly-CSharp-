using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch.MeshBatch;
using Facepunch.MeshBatch.Extensions;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public static class ExplosionHelper
{
	// Token: 0x06002AA4 RID: 10916 RVA: 0x000A9E80 File Offset: 0x000A8080
	public static ExplosionHelper.Surface[] OverlapExplosion(Vector3 point, float explosionRadius, int findLayerMask = -1, int occludingLayerMask = -1, IDMain ignore = null)
	{
		ExplosionHelper.Point point2 = new ExplosionHelper.Point(point, explosionRadius, findLayerMask, occludingLayerMask, ignore);
		return point2.ToArray();
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x000A9EA4 File Offset: 0x000A80A4
	public static ExplosionHelper.Surface[] OverlapExplosionSorted(Vector3 point, float explosionRadius, int findLayerMask = -1, int occludingLayerMask = -1, IDMain ignore = null)
	{
		ExplosionHelper.Surface[] array = ExplosionHelper.OverlapExplosion(point, explosionRadius, findLayerMask, occludingLayerMask, ignore);
		if (array.Length > 1)
		{
			Array.Sort<ExplosionHelper.Surface>(array);
		}
		return array;
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x000A9ED0 File Offset: 0x000A80D0
	public static ExplosionHelper.Surface[] OverlapExplosionUnique(Vector3 point, float explosionRadius, int findLayerMask = -1, int occludingLayerMask = -1, IDMain ignore = null)
	{
		ExplosionHelper.Surface[] array = ExplosionHelper.OverlapExplosion(point, explosionRadius, findLayerMask, occludingLayerMask, ignore);
		int num = array.Length;
		if (num > 1)
		{
			Array.Sort<ExplosionHelper.Surface>(array);
			if (ExplosionHelper.Unique.Filter(array, ref num))
			{
				Array.Resize<ExplosionHelper.Surface>(ref array, num);
			}
		}
		return array;
	}

	// Token: 0x04001727 RID: 5927
	private const float kMaxZero = 1E-05f;

	// Token: 0x020004D4 RID: 1236
	public struct Surface : IEquatable<ExplosionHelper.Surface>, IComparable<ExplosionHelper.Surface>
	{
		// Token: 0x06002AA7 RID: 10919 RVA: 0x000A9F10 File Offset: 0x000A8110
		public override bool Equals(object obj)
		{
			return obj is ExplosionHelper.Surface && this.Equals((ExplosionHelper.Surface)obj);
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000A9F2C File Offset: 0x000A812C
		public bool Equals(ExplosionHelper.Surface surface)
		{
			return this.blocked == surface.blocked && this.bounds == surface.bounds && this.idBase == surface.idBase && this.idMain == surface.idMain && this.work.Equals(ref surface.work);
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000A9FA8 File Offset: 0x000A81A8
		public bool Equals(ref ExplosionHelper.Surface surface)
		{
			return this.blocked == surface.blocked && this.bounds == surface.bounds && this.idBase == surface.idBase && this.idMain == surface.idMain && this.work.Equals(ref surface.work);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000AA01C File Offset: 0x000A821C
		public override string ToString()
		{
			return "Surface";
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000AA024 File Offset: 0x000A8224
		public override int GetHashCode()
		{
			return this.bounds.GetHashCode() ^ ((!this.idBase) ? 0 : this.idBase.GetHashCode());
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000AA060 File Offset: 0x000A8260
		public int CompareTo(ExplosionHelper.Surface other)
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

		// Token: 0x04001728 RID: 5928
		public IDBase idBase;

		// Token: 0x04001729 RID: 5929
		public IDMain idMain;

		// Token: 0x0400172A RID: 5930
		public Bounds bounds;

		// Token: 0x0400172B RID: 5931
		public ExplosionHelper.Work work;

		// Token: 0x0400172C RID: 5932
		public bool blocked;
	}

	// Token: 0x020004D5 RID: 1237
	public struct Work
	{
		// Token: 0x06002AAD RID: 10925 RVA: 0x000AA0EC File Offset: 0x000A82EC
		public bool Equals(ref ExplosionHelper.Work w)
		{
			return this.squareDistanceToCenter == w.squareDistanceToCenter && this.boundsSquareDistance == w.boundsSquareDistance && this.boundsExtentSquareMagnitude == w.boundsExtentSquareMagnitude && this.distanceToCenter == w.distanceToCenter && ((!this.rayTest) ? (!w.rayTest) : (w.rayTest && this.squareRayDistance == w.squareRayDistance && this.rayDistance == w.rayDistance && this.rayDir == w.rayDir)) && this.center == w.center && this.boundsExtent == w.boundsExtent;
		}

		// Token: 0x0400172D RID: 5933
		public Vector3 center;

		// Token: 0x0400172E RID: 5934
		public Vector3 rayDir;

		// Token: 0x0400172F RID: 5935
		public Vector3 boundsExtent;

		// Token: 0x04001730 RID: 5936
		public float boundsExtentSquareMagnitude;

		// Token: 0x04001731 RID: 5937
		public float boundsSquareDistance;

		// Token: 0x04001732 RID: 5938
		public float distanceToCenter;

		// Token: 0x04001733 RID: 5939
		public float squareDistanceToCenter;

		// Token: 0x04001734 RID: 5940
		public float rayDistance;

		// Token: 0x04001735 RID: 5941
		public float squareRayDistance;

		// Token: 0x04001736 RID: 5942
		public bool rayTest;
	}

	// Token: 0x020004D6 RID: 1238
	public struct Point : IEnumerable, IEnumerable<ExplosionHelper.Surface>
	{
		// Token: 0x06002AAE RID: 10926 RVA: 0x000AA1C8 File Offset: 0x000A83C8
		public Point(Vector3 point, float blastRadius, int overlapLayerMask, int raycastLayerMask, IDMain skip)
		{
			this.point = point;
			this.blastRadius = blastRadius;
			this.overlapLayerMask = overlapLayerMask;
			this.raycastLayerMask = raycastLayerMask;
			this.skip = skip;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000AA1F0 File Offset: 0x000A83F0
		IEnumerator<ExplosionHelper.Surface> IEnumerable<ExplosionHelper.Surface>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000AA200 File Offset: 0x000A8400
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000AA210 File Offset: 0x000A8410
		private bool BoundsWork(ref Bounds bounds, ref ExplosionHelper.Work w)
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

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000AA55C File Offset: 0x000A875C
		private bool SurfaceForMeshBatchInstance(MeshBatchInstance instance, ref ExplosionHelper.Surface surface)
		{
			surface.idBase = instance;
			surface.idMain = surface.idBase.idMain;
			if (!surface.idMain || surface.idMain == this.skip)
			{
				surface = default(ExplosionHelper.Surface);
				return false;
			}
			surface.bounds = instance.physicalBounds;
			if (this.BoundsWork(ref surface.bounds, ref surface.work))
			{
				if (surface.work.rayTest)
				{
					bool flag;
					MeshBatchInstance meshBatchInstance;
					if (this.raycastLayerMask != 0 && MeshBatchPhysics.Raycast(this.point, surface.work.rayDir, surface.work.rayDistance, this.raycastLayerMask, ref flag, ref meshBatchInstance))
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
			surface = default(ExplosionHelper.Surface);
			return false;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000AA678 File Offset: 0x000A8878
		private bool SurfaceForCollider(Collider collider, ref ExplosionHelper.Surface surface)
		{
			if (!collider.enabled)
			{
				surface = default(ExplosionHelper.Surface);
				return false;
			}
			surface.idBase = IDBase.Get(collider);
			if (!surface.idBase)
			{
				surface = default(ExplosionHelper.Surface);
				return false;
			}
			surface.idMain = surface.idBase.idMain;
			if (!surface.idMain || surface.idMain == this.skip)
			{
				surface = default(ExplosionHelper.Surface);
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

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000AA7C8 File Offset: 0x000A89C8
		public ExplosionHelper.Point.Enumerator GetEnumerator()
		{
			return new ExplosionHelper.Point.Enumerator(ref this, false);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000AA7D4 File Offset: 0x000A89D4
		public ExplosionHelper.Surface[] ToArray()
		{
			ExplosionHelper.Point.Enumerator enumerator = new ExplosionHelper.Point.Enumerator(ref this, true);
			return ExplosionHelper.Point.EnumeratorToArray.Build(ref enumerator);
		}

		// Token: 0x04001737 RID: 5943
		public readonly Vector3 point;

		// Token: 0x04001738 RID: 5944
		public readonly float blastRadius;

		// Token: 0x04001739 RID: 5945
		public readonly int overlapLayerMask;

		// Token: 0x0400173A RID: 5946
		public readonly int raycastLayerMask;

		// Token: 0x0400173B RID: 5947
		public readonly IDMain skip;

		// Token: 0x020004D7 RID: 1239
		public struct Enumerator : IDisposable, IEnumerator, IEnumerator<ExplosionHelper.Surface>
		{
			// Token: 0x06002AB6 RID: 10934 RVA: 0x000AA7F4 File Offset: 0x000A89F4
			internal Enumerator(ref ExplosionHelper.Point point, bool immediate)
			{
				this._immediate = immediate;
				this.IN = point;
				this.colliderIndex = -1;
				this.inInstanceEnumerator = false;
				this.overlapEnumerator = null;
				this.output = null;
				this.overlap = null;
				this.current = default(ExplosionHelper.Surface);
			}

			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000AA848 File Offset: 0x000A8A48
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000AA858 File Offset: 0x000A8A58
			public ExplosionHelper.Surface Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06002AB9 RID: 10937 RVA: 0x000AA860 File Offset: 0x000A8A60
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
							if (MeshBatchExtenders.GetMeshBatchPhysicalOutput<MeshBatchPhysicalOutput>(this.overlap[this.colliderIndex], ref this.output))
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
				this.current = default(ExplosionHelper.Surface);
				return false;
			}

			// Token: 0x06002ABA RID: 10938 RVA: 0x000AAA30 File Offset: 0x000A8C30
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
				this.current = default(ExplosionHelper.Surface);
			}

			// Token: 0x06002ABB RID: 10939 RVA: 0x000AAA88 File Offset: 0x000A8C88
			public void Reset()
			{
				this.Dispose();
			}

			// Token: 0x0400173C RID: 5948
			private readonly ExplosionHelper.Point IN;

			// Token: 0x0400173D RID: 5949
			private int colliderIndex;

			// Token: 0x0400173E RID: 5950
			private bool inInstanceEnumerator;

			// Token: 0x0400173F RID: 5951
			private MeshBatchPhysicalOutput output;

			// Token: 0x04001740 RID: 5952
			private IEnumerator<MeshBatchInstance> overlapEnumerator;

			// Token: 0x04001741 RID: 5953
			private Collider[] overlap;

			// Token: 0x04001742 RID: 5954
			public ExplosionHelper.Surface current;

			// Token: 0x04001743 RID: 5955
			private readonly bool _immediate;
		}

		// Token: 0x020004D8 RID: 1240
		private struct EnumeratorToArray
		{
			// Token: 0x06002ABC RID: 10940 RVA: 0x000AAA90 File Offset: 0x000A8C90
			private void RecurseInStackHeapToArray()
			{
				if (this.enumerator.MoveNext())
				{
					ExplosionHelper.Surface current = this.enumerator.current;
					this.length++;
					this.RecurseInStackHeapToArray();
					this.array[--this.length] = current;
				}
				else
				{
					this.array = new ExplosionHelper.Surface[this.length];
				}
			}

			// Token: 0x06002ABD RID: 10941 RVA: 0x000AAB08 File Offset: 0x000A8D08
			public static ExplosionHelper.Surface[] Build(ref ExplosionHelper.Point.Enumerator point_enumerator)
			{
				ExplosionHelper.Point.EnumeratorToArray enumeratorToArray;
				enumeratorToArray.enumerator = point_enumerator;
				enumeratorToArray.length = 0;
				enumeratorToArray.array = null;
				enumeratorToArray.RecurseInStackHeapToArray();
				return enumeratorToArray.array;
			}

			// Token: 0x04001744 RID: 5956
			private ExplosionHelper.Point.Enumerator enumerator;

			// Token: 0x04001745 RID: 5957
			private ExplosionHelper.Surface[] array;

			// Token: 0x04001746 RID: 5958
			private int length;
		}
	}

	// Token: 0x020004D9 RID: 1241
	private static class Unique
	{
		// Token: 0x06002ABF RID: 10943 RVA: 0x000AAB4C File Offset: 0x000A8D4C
		public static bool Filter(ExplosionHelper.Surface[] array, ref int length)
		{
			int num = array.Length;
			try
			{
				for (int i = 0; i < num; i++)
				{
					IDMain idMain = array[i].idMain;
					if (idMain && !ExplosionHelper.Unique.Set.Add(idMain))
					{
						int num2 = i;
						while (++i < num)
						{
							idMain = array[i].idMain;
							if (!array[i].idMain || ExplosionHelper.Unique.Set.Add(idMain))
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
				ExplosionHelper.Unique.Set.Clear();
			}
			return false;
		}

		// Token: 0x04001747 RID: 5959
		private static readonly HashSet<IDMain> Set = new HashSet<IDMain>();
	}
}
