using System;
using UnityEngine;

// Token: 0x0200056D RID: 1389
public class GameGizmoWaveAnimationCarrier : global::GameGizmoWaveAnimation
{
	// Token: 0x06002DE0 RID: 11744 RVA: 0x000AD36C File Offset: 0x000AB56C
	protected override global::GameGizmo.Instance ConstructInstance()
	{
		return new global::GameGizmoWaveAnimationCarrier.Instance(this);
	}

	// Token: 0x040017E2 RID: 6114
	[SerializeField]
	protected Material[] carrierMaterials;

	// Token: 0x040017E3 RID: 6115
	[SerializeField]
	protected bool hideArrowWhenCarrierExists;

	// Token: 0x0200056E RID: 1390
	public new class Instance : global::GameGizmoWaveAnimation.Instance
	{
		// Token: 0x06002DE1 RID: 11745 RVA: 0x000AD374 File Offset: 0x000AB574
		protected internal Instance(global::GameGizmoWaveAnimationCarrier gameGizmo) : base(gameGizmo)
		{
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000AD380 File Offset: 0x000AB580
		protected override void Render(bool useCamera, Camera camera)
		{
			if (global::gizmos.carrier && this.carrierRenderer && this.carrierRenderer.enabled)
			{
				Material[] carrierMaterials = ((global::GameGizmoWaveAnimationCarrier)this.gameGizmo).carrierMaterials;
				if (carrierMaterials != null && carrierMaterials.Length > 0)
				{
					MeshFilter component = this.carrierRenderer.GetComponent<MeshFilter>();
					if (component)
					{
						Mesh sharedMesh = component.sharedMesh;
						if (sharedMesh)
						{
							try
							{
								this.hideMesh = ((global::GameGizmoWaveAnimationCarrier)this.gameGizmo).hideArrowWhenCarrierExists;
								base.Render(useCamera, camera);
							}
							finally
							{
								this.hideMesh = false;
							}
							foreach (Material material in carrierMaterials)
							{
								if (material)
								{
									int num = sharedMesh.subMeshCount;
									while (--num >= 0)
									{
										Graphics.DrawMesh(sharedMesh, this.carrierRenderer.localToWorldMatrix, material, base.layer, camera, num, this.propertyBlock, base.castShadows, base.receiveShadows);
									}
								}
							}
							return;
						}
					}
				}
			}
			base.Render(useCamera, camera);
		}
	}
}
