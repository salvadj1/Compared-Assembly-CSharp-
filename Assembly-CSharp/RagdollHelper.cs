using System;
using Facepunch.Actor;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class RagdollHelper : MonoBehaviour
{
	// Token: 0x06000E1E RID: 3614 RVA: 0x000361C0 File Offset: 0x000343C0
	private void Start()
	{
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x000361C4 File Offset: 0x000343C4
	private void Update()
	{
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x000361C8 File Offset: 0x000343C8
	private static void _RecursiveLinkTransformsByName(Transform ragdoll, Transform body)
	{
		for (int i = 0; i < ragdoll.childCount; i++)
		{
			Transform childAtIndex = global::FindChildHelper.GetChildAtIndex(ragdoll, i);
			Transform transform = global::FindChildHelper.FindChildByName(childAtIndex.name, body);
			if (transform)
			{
				childAtIndex.position = transform.position;
				childAtIndex.rotation = transform.rotation;
			}
			global::RagdollHelper._RecursiveLinkTransformsByName(childAtIndex, body);
		}
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0003622C File Offset: 0x0003442C
	private static void _RecursiveLinkTransformsByName(Transform ragdoll, Transform body, Transform bodyMatchTransform, ref Transform ragdollMatchTransform, ref bool foundMatch)
	{
		ragdollMatchTransform = null;
		for (int i = 0; i < ragdoll.childCount; i++)
		{
			Transform childAtIndex = global::FindChildHelper.GetChildAtIndex(ragdoll, i);
			Transform transform = global::FindChildHelper.FindChildByName(childAtIndex.name, body);
			if (transform)
			{
				childAtIndex.position = transform.position;
				childAtIndex.rotation = transform.rotation;
				if (!foundMatch && transform == bodyMatchTransform)
				{
					foundMatch = true;
					ragdollMatchTransform = childAtIndex;
				}
				if (foundMatch)
				{
					global::RagdollHelper._RecursiveLinkTransformsByName(childAtIndex, transform);
				}
				else
				{
					global::RagdollHelper._RecursiveLinkTransformsByName(childAtIndex, transform, bodyMatchTransform, ref ragdollMatchTransform, ref foundMatch);
				}
			}
		}
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x000362C8 File Offset: 0x000344C8
	public static void RecursiveLinkTransformsByName(Transform ragdoll, Transform body)
	{
		BoneStructure component = body.GetComponent<BoneStructure>();
		if (component)
		{
			BoneStructure component2 = ragdoll.GetComponent<BoneStructure>();
			if (component2)
			{
				using (BoneStructure.ParentDownOrdered.Enumerator enumerator = component.parentDown.GetEnumerator())
				{
					using (BoneStructure.ParentDownOrdered.Enumerator enumerator2 = component2.parentDown.GetEnumerator())
					{
						while (enumerator.MoveNext() && enumerator2.MoveNext())
						{
							Transform transform = enumerator.Current;
							Transform transform2 = enumerator2.Current;
							transform2.position = transform.position;
							transform2.rotation = transform.rotation;
						}
					}
				}
				return;
			}
		}
		global::RagdollHelper._RecursiveLinkTransformsByName(ragdoll, body);
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x000363C4 File Offset: 0x000345C4
	public static bool RecursiveLinkTransformsByName(Transform ragdoll, Transform body, Transform bodyMatchTransform, out Transform ragdollMatchTransform)
	{
		if (!bodyMatchTransform)
		{
			ragdollMatchTransform = null;
			global::RagdollHelper.RecursiveLinkTransformsByName(ragdoll, body);
			return false;
		}
		if (body == bodyMatchTransform)
		{
			ragdollMatchTransform = ragdoll;
			global::RagdollHelper.RecursiveLinkTransformsByName(ragdoll, body);
			return true;
		}
		BoneStructure component = body.GetComponent<BoneStructure>();
		if (component)
		{
			BoneStructure component2 = ragdoll.GetComponent<BoneStructure>();
			if (component2)
			{
				using (BoneStructure.ParentDownOrdered.Enumerator enumerator = component.parentDown.GetEnumerator())
				{
					using (BoneStructure.ParentDownOrdered.Enumerator enumerator2 = component2.parentDown.GetEnumerator())
					{
						while (enumerator.MoveNext() && enumerator2.MoveNext())
						{
							Transform transform = enumerator.Current;
							Transform transform2 = enumerator2.Current;
							transform2.position = transform.position;
							transform2.rotation = transform.rotation;
							if (transform == bodyMatchTransform)
							{
								ragdollMatchTransform = transform2;
								while (enumerator.MoveNext() && enumerator2.MoveNext())
								{
									transform = enumerator.Current;
									transform2 = enumerator2.Current;
									transform2.position = transform.position;
									transform2.rotation = transform.rotation;
								}
								return true;
							}
						}
					}
				}
				ragdollMatchTransform = null;
				return false;
			}
		}
		bool result = false;
		ragdollMatchTransform = null;
		global::RagdollHelper._RecursiveLinkTransformsByName(ragdoll, body, bodyMatchTransform, ref ragdollMatchTransform, ref result);
		return result;
	}
}
