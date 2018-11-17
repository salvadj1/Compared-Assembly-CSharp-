using System;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
public class GameGizmoWaveAnimationCarrier : GameGizmoWaveAnimation
{
	// Token: 0x06002A2E RID: 10798 RVA: 0x000A55D4 File Offset: 0x000A37D4
	protected override GameGizmo.Instance ConstructInstance()
	{
		return new GameGizmoWaveAnimationCarrier.Instance(this);
	}

	// Token: 0x04001625 RID: 5669
	[SerializeField]
	protected Material[] carrierMaterials;

	// Token: 0x04001626 RID: 5670
	[SerializeField]
	protected bool hideArrowWhenCarrierExists;

	// Token: 0x020004B3 RID: 1203
	public new class Instance : GameGizmoWaveAnimation.Instance
	{
		// Token: 0x06002A2F RID: 10799 RVA: 0x000A55DC File Offset: 0x000A37DC
		protected internal Instance(GameGizmoWaveAnimationCarrier gameGizmo) : base(gameGizmo)
		{
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000A55E8 File Offset: 0x000A37E8
		protected override void Render(bool useCamera, Camera camera)
		{
			if (gizmos.carrier && this.carrierRenderer && this.carrierRenderer.enabled)
			{
				Material[] carrierMaterials = ((GameGizmoWaveAnimationCarrier)this.gameGizmo).carrierMaterials;
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
								this.hideMesh = ((GameGizmoWaveAnimationCarrier)this.gameGizmo).hideArrowWhenCarrierExists;
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
