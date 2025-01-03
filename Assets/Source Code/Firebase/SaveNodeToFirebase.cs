using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SaveNodeToFirebase : MonoBehaviour
{
    [Header("GUI")]
    [SerializeField] TMPro.TMP_Dropdown tMP_Dropdown;
    [SerializeField] GameObject buttonBackToHome;
    [SerializeField] GameObject buttonSaveToLocal;
    [SerializeField] GameObject buttonLoadFromLocal;
    [SerializeField] GameObject buttonDeleteFromLocal;
    [SerializeField] GameObject buttonResetLevel;

    [Header("GUI Saving")]
    [SerializeField] TextMeshProUGUI textNotify;
    [SerializeField] TextMeshProUGUI textUploadSucessfully;
    [SerializeField] GameObject pannelSaveToFirebase;
    [SerializeField] GameObject buttonSaveToFirebase;

    [Header("Pathing")]
    [SerializeField] string linkFirebase = "gs://tower-defend-3d-unity-84f17.appspot.com";

    [Header("Stats")]
    [SerializeField] bool isSaving = false;
    [SerializeField] bool isStartSaving = false;
    [SerializeField] int maxNodePath = 12;
    [SerializeField] float timeSaving;
    [SerializeField] float defaultTimeSaving = 5f;
    [SerializeField] string level;
    public void Start()
    {
        this.timeSaving = this.defaultTimeSaving;
        this.textUploadSucessfully.text = string.Empty;
        this.pannelSaveToFirebase.gameObject.SetActive(false);
    }
    public void Update()
    {
        if (this.isSaving == true)
        {
            StartCoroutine(nameof(this.CoroutinePannelSaveDone));
            this.isSaving = false;
        }
        this.SetTimeSaving();
    }
    public void ButtonSaveToFirebase()
    {
        this.pannelSaveToFirebase.SetActive(true);
        this.buttonSaveToFirebase.SetActive(false);

        this.isSaving = false;
        this.isStartSaving = true;
        this.timeSaving = this.defaultTimeSaving;
        this.level = tMP_Dropdown.captionText.text.ToString();

        this.SetActiveButtonGUI(false);
        return;
    }
    public void ButtonCancelSaveToFireBase()
    {
        this.pannelSaveToFirebase.SetActive(false);
        this.buttonSaveToFirebase.SetActive(true);

        this.isSaving = false;
        this.isStartSaving = false;
        this.timeSaving = this.defaultTimeSaving;

        this.SetActiveButtonGUI(true);
        return;
    }
    private void SetTimeSaving()
    {
        if (this.timeSaving <= 0)
        {
            this.timeSaving = this.defaultTimeSaving;
            this.isSaving = true;
            this.isStartSaving = false;
        }

        if (this.isStartSaving == true)
            this.timeSaving -= Time.deltaTime;

        this.textNotify.text = $"{this.level} uploading...{Mathf.Round(this.timeSaving)}";
    }
    private void UploadNodeToFirebase()
    {
        string _linkLocalNodePath = FileLocalLink.DesignerFolderNodePath + this.level;
        string _linkLocalNodeBuilding = FileLocalLink.DesignerFolderNodeBuilding + this.level + "/" + this.level;

        if (!File.Exists(_linkLocalNodeBuilding) || Directory.GetFiles(_linkLocalNodePath).Length == 0)
        {
            this.textUploadSucessfully.text = $"Some {this.level} files not found!";
            return;
        }

        string _linkFirebaseNodePath = this.linkFirebase + "/" + FileLocalLink.NameFolderNodePath + "/" + this.level;
        string _linkFirebaseNodeBuilding = this.linkFirebase + "/" + FileLocalLink.NameFolderNodeBuilding + "/" + this.level;

        FirebaseStorage firebaseStorage = FirebaseStorage.DefaultInstance;
        StorageReference storageReference = null;

        this.UploadFileNodePath(firebaseStorage, storageReference, _linkFirebaseNodePath, _linkLocalNodePath);
        this.UploadnodeBuilding(firebaseStorage, storageReference, _linkFirebaseNodeBuilding, _linkLocalNodeBuilding);

        this.textUploadSucessfully.text = $"{this.level} Uploaded!";
    }
    private void UploadFileNodePath(FirebaseStorage firebaseStorage, StorageReference storageReference, string linkFirebase, string linkLocal)
    {
        int count = 1;
        storageReference = firebaseStorage.GetReferenceFromUrl(linkFirebase);

        for (int i = 1; i <= this.maxNodePath; i++)
            storageReference.Child(this.level + i).DeleteAsync();

        foreach (var file in Directory.GetFiles(linkLocal))
        {
            storageReference.Child(this.level + count).PutFileAsync(file).ContinueWith((Task<StorageMetadata> task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                    Debug.Log(task.Exception.Message.ToString());
            });
            count++;
        }
    }
    private void UploadnodeBuilding(FirebaseStorage firebaseStorage, StorageReference storageReference, string linkFirebase, string linkLocal)
    {
        storageReference = firebaseStorage.GetReferenceFromUrl(linkFirebase);
        storageReference.Child($"{this.level}").PutFileAsync($"{linkLocal}").ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
                Debug.Log(task.Exception.Message.ToString());
        });
    }
    IEnumerator CoroutinePannelSaveDone()
    {
        yield return new WaitForEndOfFrame();
        this.UploadNodeToFirebase();
        this.pannelSaveToFirebase.SetActive(false);

        yield return new WaitForSeconds(1f);
        this.SetActiveButtonGUI(true);
        this.buttonSaveToFirebase.SetActive(true);

        yield return new WaitForSeconds(3f);
        this.textUploadSucessfully.text = string.Empty;
    }
    private void SetActiveButtonGUI(bool isActive)
    {
        tMP_Dropdown.gameObject.SetActive(isActive);
        buttonSaveToLocal.gameObject.SetActive(isActive);
        buttonLoadFromLocal.gameObject.SetActive(isActive);
        buttonDeleteFromLocal.gameObject.SetActive(isActive);
        buttonBackToHome.gameObject.SetActive(isActive);
        buttonResetLevel.gameObject.SetActive(isActive);
    }
}
