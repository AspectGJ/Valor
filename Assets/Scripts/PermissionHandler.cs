using UnityEngine;
using UnityEngine.Android;

public class PermissionHandler : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                Debug.Log("izin istendi 1");
            }

            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                Debug.Log("izin istendi 2");
                           
            }
        }
    }
}

