using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class BlendShapeController : MonoBehaviour
{
    [System.Serializable]
    public class BlendShapeReference
    {
        public string name;
        public float weight;
    }

    public List<GameObject> blendShapeObjects;
    public List<BlendShapeReference> blendShapes;

    //reference to all blendshapes from taken from SkinnedMeshRenderer (unique blendshape names)
    private Dictionary<string, List<SkinnedMeshRenderer>> blendShapeDictionary;

    private void Awake()
    {
        blendShapeDictionary = new Dictionary<string, List<SkinnedMeshRenderer>>();

        // loop through gameobjects with the blendshapes
        foreach (var obj in blendShapeObjects)
        {
            var meshRenderer = obj.GetComponent<SkinnedMeshRenderer>();
            if (meshRenderer != null)
            {
                for (int i = 0; i < meshRenderer.sharedMesh.blendShapeCount; i++)
                {
                    string blendShapeName = meshRenderer.sharedMesh.GetBlendShapeName(i);
                    if (!blendShapeDictionary.ContainsKey(blendShapeName))
                    {
                        blendShapeDictionary.Add(blendShapeName, new List<SkinnedMeshRenderer>());
                        blendShapes.Add(new BlendShapeReference { name = blendShapeName, weight = 0 });
                    }
                    blendShapeDictionary[blendShapeName].Add(meshRenderer);
                }
            }
        }
    }

    private void Update()
    {
        foreach (var blendShape in blendShapes)
        {
            if (blendShapeDictionary.ContainsKey(blendShape.name))
            {
                foreach (var meshRenderer in blendShapeDictionary[blendShape.name])
                {
                    int blendShapeIndex = meshRenderer.sharedMesh.GetBlendShapeIndex(blendShape.name);
                    meshRenderer.SetBlendShapeWeight(blendShapeIndex, blendShape.weight);
                }
            }
        }
    }
}
