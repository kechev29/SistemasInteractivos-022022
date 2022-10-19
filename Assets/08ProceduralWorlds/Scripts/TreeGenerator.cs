using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    private const int INDEX_OF_CHILD_STICK = 0;
    private const int INDEX_OF_CHILD_CIRCLE = 1;

    [SerializeField] private GameObject branchPrefab;
    [Range(0, 10)][SerializeField] int treeLevels;
    [SerializeField] float initialSize = 1;
    [Range(0f, 1f)][SerializeField] float reductionPerLevel = 0.1f;

    Queue<GameObject> branchQueue = new Queue<GameObject>();

    float currentSize;
    int currentLevel;

    void Start()
    {
        currentSize = initialSize;
        currentLevel = 1;

        GameObject root = Instantiate(branchPrefab, transform);
        ChangeBranchSize(root, currentSize);
        branchQueue.Enqueue(root);

        GenerateTree();
    }

    private void GenerateTree()
    {
        if (currentLevel >= treeLevels) return;
        ++currentLevel;

        float newSize = Mathf.Max(currentSize - currentSize * reductionPerLevel, 0.5f);
        currentSize = newSize;

        List<GameObject> currentLevelBranches = new List<GameObject>();

        while (branchQueue.Count > 0)
        {
            GameObject currentBranch = branchQueue.Dequeue();

            GameObject leftBranch = CreateBranch(currentBranch, Random.Range(10, 30));
            GameObject rightBranch = CreateBranch(currentBranch, Random.Range(-30, -10));

            ChangeBranchSize(leftBranch, currentSize);
            ChangeBranchSize(rightBranch, currentSize);

            currentLevelBranches.Add(leftBranch);
            currentLevelBranches.Add(rightBranch);
        }

        foreach(GameObject branch in currentLevelBranches)
        {
            branchQueue.Enqueue(branch);
        }

        GenerateTree();
    }

    private void ChangeBranchSize(GameObject branch, float newSize)
    {
        Transform stick = branch.transform.GetChild(INDEX_OF_CHILD_STICK);
        Transform circle = branch.transform.GetChild(INDEX_OF_CHILD_CIRCLE);

        stick.localScale = new Vector3(stick.localScale.x, newSize, stick.localScale.z);
        stick.localPosition = new Vector3(stick.localPosition.x, newSize / 2, stick.localPosition.z);
        circle.localPosition = new Vector3(circle.localPosition.x, newSize, circle.localPosition.z);

    }

    private float GetBranchLength(GameObject branch)
    {
        Transform stick = branch.transform.GetChild(INDEX_OF_CHILD_STICK);
        return stick.localScale.y;
    }


    private GameObject CreateBranch(GameObject prevBranch, float relativeAngle)
    {
        //create new branch
        GameObject newBranch = Instantiate(branchPrefab, this.transform);

        //set position
        newBranch.transform.localPosition = prevBranch.transform.localPosition + prevBranch.transform.up * GetBranchLength(prevBranch);

        //set rotation
        newBranch.transform.localRotation = prevBranch.transform.localRotation * Quaternion.Euler(0, 0, relativeAngle);

        return newBranch;
    }

}
