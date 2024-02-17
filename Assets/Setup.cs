using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using static UnityEditor.AssetDatabase;

namespace MyTools {
    public static class Setup {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders() {
            Folders.CreateDefault("_Project", "Animation", "Art", "Input", "Materials", "Prefabs", "Scripts/ScriptableObjects", "Scripts/UI", "Scripts/Player");
            Refresh();
        }

        [MenuItem("Tools/Setup/Import Favorite Assets")]
        public static void ImportFavoriteAssets() {
            Assets.ImportAsset("PrimeTween High-Performance Animations and Sequences.unitypackage", "Kyrylo Kuzyk/Editor ExtensionsAnimation");
        }

        [MenuItem("Tools/Setup/Install Unity Input System")]
        public static void InstallUnityInputSystem() {
            Packages.InstallPackages(new[] {
                "com.unity.inputsystem"
            });
        }

        [MenuItem("Tools/Setup/Install Favorite Open Source")]
        public static void InstallOpenSource() {
            Packages.InstallPackages(new[] {
                "git+https://github.com/KyleBanks/scene-ref-attribute",
                "git+https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask"
            });
        }

        static class Folders {
            public static void CreateDefault(string root, params string[] folders) {
                var fullpath = Path.Combine(Application.dataPath, root);
                if (!Directory.Exists(fullpath)) {
                    Directory.CreateDirectory(fullpath);
                }
                foreach (var folder in folders) {
                    CreateSubFolders(fullpath, folder);
                }
            }
    
            private static void CreateSubFolders(string rootPath, string folderHierarchy) {
                var folders = folderHierarchy.Split('/');
                var currentPath = rootPath;
                foreach (var folder in folders) {
                    currentPath = Path.Combine(currentPath, folder);
                    if (!Directory.Exists(currentPath)) {
                        Directory.CreateDirectory(currentPath);
                    }
                }
            }
        }

        static class Packages {
            static AddRequest Request;
            static Queue<string> PackagesToInstall = new();

            public static void InstallPackages(string[] packages) {
                foreach (var package in packages) {
                    PackagesToInstall.Enqueue(package);
                }

                // Start the installation of the first package
                if (PackagesToInstall.Count > 0) {
                    Request = Client.Add(PackagesToInstall.Dequeue());
                    EditorApplication.update += Progress;
                }
            }

            static async void Progress() {
                if (Request.IsCompleted) {
                    if (Request.Status == StatusCode.Success)
                        Debug.Log("Installed: " + Request.Result.packageId);
                    else if (Request.Status >= StatusCode.Failure)
                        Debug.Log(Request.Error.message);

                    EditorApplication.update -= Progress;

                    // If there are more packages to install, start the next one
                    if (PackagesToInstall.Count > 0) {
                        // Add delay before next package install
                        await Task.Delay(1000);
                        Request = Client.Add(PackagesToInstall.Dequeue());
                        EditorApplication.update += Progress;
                    }
                }
            }
        }

        static class Assets {
            public static void ImportAsset(string asset, string subfolder,
                string rootFolder = "C:/Users/rcflo/AppData/Roaming/Unity/Asset Store-5.x") {
                ImportPackage(Path.Combine(rootFolder, subfolder, asset), false);
            }
        }
    }
}
