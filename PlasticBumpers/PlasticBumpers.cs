using JaLoader;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlasticBumpers
{
    public class PlasticBumpers : Mod
    {
        public override string ModID => "PlasticBumpers";
        public override string ModName => "Plastic Bumpers";
        public override string ModAuthor => "Leaxx";
        public override string ModDescription => "Makes the bumpers a dark black plastic.";
        public override string ModVersion => "1.0";
        public override string GitHubLink => "https://github.com/Jalopy-Mods/PlasticBumpers";
        public override string NexusModsLink => "";

        public override WhenToInit WhenToInit => WhenToInit.InGame;
        public override List<(string, string, string)> Dependencies => new List<(string, string, string)>()
        {
            ("JaLoader", "Leaxx", "4.0.3")
        };

        public override List<(string, string, string)> Incompatibilities => new List<(string, string, string)>()
        {

        };

        public override bool UseAssets => false;


        public override void CustomObjectsRegistration()
        {
            base.CustomObjectsRegistration();

            ModHelper.Instance.SetExtraToUseDefaultIcon("plasticBumpers");

            CustomObjectsManager.Instance.RegisterObject(ModHelper.Instance.CreateCustomExtraObject(
                BoxSizes.Big,
                "Plastic Bumpers",
                "Replaces the bumpers with some black plastic ones.",
                30,
                -10, // reduces weight when installed
                "plasticBumpers",
                AttachExtraTo.Body,
                this), "plasticBumpers");
        }

        public override void OnExtraAttached(string extraName)
        {
            base.OnExtraAttached(extraName);

            if (extraName == "plasticBumpers")
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    var bumper = FindChildWithMeshName(ModHelper.Instance.carFrame.transform, "FrontBumper");
                    var bumperRenderer = bumper.GetComponent<Renderer>();
                    bumperRenderer.material.color = new Color32(50, 50, 50, 255);

                    return;
                }

                var frame = GameObject.Find("FrameHolder").transform;
                frame = frame.Find("TweenHolder/Frame");

                var frontBumperRenderer = frame.transform.Find("Frontbumper").GetComponent<Renderer>();
                var rearBumperRenderer = frame.transform.Find("Rearbumper").GetComponent<Renderer>();

                frontBumperRenderer.material.color = new Color32(50, 50, 50, 255);
                rearBumperRenderer.material.color = new Color32(50, 50, 50, 255);
            }
        }

        private Transform FindChildWithMeshName(Transform parent, string meshName)
        {
            foreach (Transform child in parent)
            {
                var meshFilter = child.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.sharedMesh != null && meshFilter.sharedMesh.name == meshName)
                {
                    return child;
                }
                var result = FindChildWithMeshName(child, meshName);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
