using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PageDirection
{
    Previous,
    Next
}

public class PageManager : MonoBehaviour
{
    private Player_Motor MOTOR;

    [Header("Player Dummy Data")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerDummy;

    private GameObject previousPagePlayer;
    private GameObject nextPagePlayer;

    [SerializeField] private float previousPagePlayerXDisplacment;
    [SerializeField] private float nextPagePlayerXDisplacment;

    [Header("Current Page Index")]
    [SerializeField] private int currentPage;

    [Header("Previous Page Prefab")]
    [SerializeField] private GameObject previousPagePrefab;

    [Header("Current Page Prefab")]
    [SerializeField] private GameObject currentPagePrefab;

    [Header("Next Page Prefab")]
    [SerializeField] private GameObject nextPagePrefab;

    [Header("Page Slots")]
    [SerializeField] private Transform previousPageSlot;
    [SerializeField] private Transform currentPageSlot;
    [SerializeField] private Transform nextPageSlot;

    private GameObject previousPageObject;
    private GameObject currentPageObject;
    private GameObject nextPageObject;

    [Header("Page Data")]
    public bool atBookEnd;
    public bool PageAnimationFinished;
    [SerializeField] private Animator book;
    [SerializeField] private GameObject[] pages;

    private void Start()
    {
        UpdatePagePrefabs();

        MOTOR = FindAnyObjectByType<Player_Motor>();
    }

    public bool VerifyPageManagerFunctionality(PageDirection direction)
    {
        if (direction == PageDirection.Previous && currentPage <= -1)
            return false;

        if (direction == PageDirection.Next && currentPage >= pages.Length)
            return false;

        return true;
    }

    public void TurnThePage(PageDirection direction)
    {
        PageAnimationFinished = false;

        book.SetBool("Previous", false);
        book.SetBool("Next", false);
        book.SetBool("BookCoverEnd", false);
        book.SetBool("BookBackEnd", false);

        switch (direction)
        {
            case PageDirection.Previous:

                if (previousPagePrefab == null)
                {
                    book.SetBool("BookCoverEnd", true);
                    Debug.Log("TO BOOK COVER");
                }
                else
                {
                    book.SetBool("Previous", true);
                    Debug.Log("TO PREVIOUS PAGE");
                }

                ChangeThePages(direction);
                break;

            case PageDirection.Next:

                if (nextPagePrefab == null)
                {
                    book.SetBool("BookBackEnd", true);
                    Debug.Log("TO BOOK BACK");
                }
                else
                {
                    book.SetBool("Next", true);
                    Debug.Log("TO NEXT PAGE");
                }

                ChangeThePages(direction);
                break;
        }
    }

    public void ChangeThePages(PageDirection direction)
    {
        switch (direction)
        {
            case PageDirection.Previous:
                currentPage--;
                break;

            case PageDirection.Next:
                currentPage++;
                break;
        }
    }

    private void UpdatePagePrefabs()
    {
        currentPagePrefab = currentPage >= 0 && currentPage < pages.Length
            ? pages[currentPage]
            : null;

        previousPagePrefab = currentPage > 0
            ? pages[currentPage - 1]
            : null;

        nextPagePrefab = currentPage < pages.Length - 1
            ? pages[currentPage + 1]
            : null;

        Destroy(previousPageObject);
        Destroy(currentPageObject);
        Destroy(nextPageObject);

        if (previousPagePrefab != null)
        {
            previousPageObject = Instantiate(
            previousPagePrefab,
            previousPageSlot.position,
            previousPageSlot.rotation
            );
        }

        if (currentPagePrefab != null)
        {
            currentPageObject = Instantiate(
            currentPagePrefab,
            currentPageSlot.position,
            currentPageSlot.rotation
            );
        }

        if (nextPagePrefab != null)
        {
            nextPageObject = Instantiate(
            nextPagePrefab,
            nextPageSlot.position,
            nextPageSlot.rotation
            );
        }
    }

    public void CreatePlayerPageProjection()
    {
        if (player == null)
            return;

        previousPagePlayer = Instantiate(
            playerDummy,
            player.transform.position - new Vector3(previousPagePlayerXDisplacment, 0f, 0f),
            player.transform.rotation
        );

        nextPagePlayer = Instantiate(
            playerDummy,
            player.transform.position - new Vector3(nextPagePlayerXDisplacment, 0f, 0f),
            player.transform.rotation
        );
    }

    public void DestroyPlayerPageProjection()
    {
        if (previousPagePlayer != null)
        {
            Destroy(previousPagePlayer);
            previousPagePlayer = null;
        }

        if (nextPagePlayer != null)
        {
            Destroy(nextPagePlayer);
            nextPagePlayer = null;
        }
    }

    public void AnimationFinished()
    {
        StartCoroutine(FinishAnimation());
        //Coroutining to make sure the animation has enough time to finish before
        //prefabs are updated to prevent the incorrect prefab from being shown for one frame
    }

    private IEnumerator FinishAnimation()
    {
        yield return new WaitForEndOfFrame();

        UpdatePagePrefabs();
        DestroyPlayerPageProjection();

        book.SetBool("Previous", false);
        book.SetBool("Next", false);
        book.SetBool("BookCoverEnd", false);
        book.SetBool("BookBackEnd", false);

        PageAnimationFinished = true;

        atBookEnd = currentPage == -1 || currentPage >= pages.Length;
        //CIRCLE BACK TO FIX THIS LINE LATER WHEN DONE WITH OTHER STUFF
        //NEED IT IN ORDER TO STOP THE PLAYER FROM MOVING WHEN OPENING THE BOOK
    }
}
