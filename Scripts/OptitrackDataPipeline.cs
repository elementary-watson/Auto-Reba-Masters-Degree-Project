using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaturalPoint.NatNetLib;

public class OptitrackDataPipeline : MonoBehaviour
{
    public OptitrackStreamingClient optiClient;
    string skeletonName = "SkeletonV";
    OptitrackRigidBodyState rigit_body_l_shoulder_state;
    OptitrackSkeletonState optiSkeletonState;
    OptitrackSkeletonDefinition optiSkeletonDefinition;
    OptitrackBoneNameConvention optiBoneNameConvention;

    int skeletonID;

    public List<GameObject> gameObjects; // List of GameObjects to inspect

    // Start is called before the first frame update
    void Start()
    {
        optiSkeletonDefinition = optiClient.GetSkeletonDefinitionByName(skeletonName);
        skeletonID = optiSkeletonDefinition.Id;

        //Prints out the bone hierarchy
        //startPrintingBoneHierarchy();

    }

    public void startPrintingBoneHierarchy()
    {
        // Find the root bone (which has no parent)
        OptitrackSkeletonDefinition.BoneDefinition rootBone = optiSkeletonDefinition.Bones.Find(b => b.ParentId == 0);

        // Print out the hierarchy of the skeleton
        string output = "";
        PrintBoneHierarchy(rootBone, 0, ref output);
        Debug.Log(output);

        // Print out the hierarchy of each GameObject
        foreach (GameObject go in gameObjects)
        {
            output = "";
            PrintGameObjectHierarchy(go.transform, 0, ref output);
            Debug.Log(output); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //optiSkeletonDefinition.
    }

    void PrintBoneHierarchy(OptitrackSkeletonDefinition.BoneDefinition bone, int depth, ref string output)
    {
        // Indent the bone name based on its depth in the hierarchy.
        for (int i = 0; i < depth; ++i)
        {
            output += "  ";
        }

        // Append the bone name.
        output += bone.Name + "\n";

        // Recursively print the children.
        foreach (var childBone in GetChildBones(bone))
        {
            PrintBoneHierarchy(childBone, depth + 1, ref output);
        }
    }

    List<OptitrackSkeletonDefinition.BoneDefinition> GetChildBones(OptitrackSkeletonDefinition.BoneDefinition parentBone)
    {
        return optiSkeletonDefinition.Bones.FindAll(b => b.ParentId == parentBone.Id);
    }

    void PrintGameObjectHierarchy(Transform transform, int depth, ref string output)
    {
        // Indent the GameObject name based on its depth in the hierarchy.
        for (int i = 0; i < depth; ++i)
        {
            output += "  ";
        }

        // Append the GameObject name.
        output += transform.name + "\n";

        // Recursively print the children.
        foreach (Transform child in transform)
        {
            PrintGameObjectHierarchy(child, depth + 1, ref output);
        }
    }
}
