using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Logs tracked device node data (head & hands, optionally eyes) each FixedUpdate to a CSV file.
/// Simplified version with single API calls and mixed precision output.
/// </summary>
public class DeviceNodeLogger : MonoBehaviour
{
    [Tooltip("If true, the save path will be overridden with the specified savePath. Only in Editor.")]
    public bool overrideSavePath = false;

    [Tooltip("Custom folder path used when overrideSavePath is true (Editor only).")]
    public string savePath = "C:\\Users\\bergstein2\\Documents\\DeviceNodeLoggerOutput";

    [Tooltip("Nodes to log each frame (OVRPlugin.Node list).")]
    public OVRPlugin.Node[] nodesToLog = new[]
    {
        OVRPlugin.Node.Head,
        OVRPlugin.Node.HandLeft,
        OVRPlugin.Node.HandRight
        // Add more (EyeLeft, EyeRight, EyeCenter, ControllerLeft, ControllerRight, etc.) if needed
    };

    private string _filePath;
    private bool _csvHeaderWritten;

    private void Start()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string filename = $"{timestamp}_DeviceNodes.csv";
        _filePath = Path.Combine(Application.persistentDataPath, filename);

#if UNITY_EDITOR
        if (overrideSavePath)
        {
            // Ensure the directory exists - create it if it doesn't
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                Debug.Log($"Created directory: {savePath}");
            }
            _filePath = Path.Combine(savePath, filename);
        }
#endif
        Debug.Log($"DeviceNodeLogger CSV Logging to: {_filePath}");
    }

    private void FixedUpdate()
    {
        // Ensure OVR system initialized before attempting to query
        if (!OVRManager.OVRManagerinitialized)
        {
            return;
        }
        
        EnsureCsvHeader();
        WriteCurrentRow();
    }

    private void EnsureCsvHeader()
    {
        if (_csvHeaderWritten) return;

        var headers = new List<string> { "LogTime" }; // Unity Time.time seconds

        // For each node add tracking flags + pose + velocity + angular velocity columns
        foreach (var node in nodesToLog)
        {
            string n = node.ToString();
            headers.AddRange(new[]
            {
                // Tracking state flags (1=true, 0=false)
                $"{n}_Present", $"{n}_PosTracked", $"{n}_RotTracked", $"{n}_PosValid", $"{n}_RotValid",
                // Position data
                $"{n}_PosX", $"{n}_PosY", $"{n}_PosZ",
                // Rotation data  
                $"{n}_RotX", $"{n}_RotY", $"{n}_RotZ", $"{n}_RotW",
                // Velocity data
                $"{n}_VelX", $"{n}_VelY", $"{n}_VelZ",
                // Angular velocity data
                $"{n}_AngVelX", $"{n}_AngVelY", $"{n}_AngVelZ"
            });
        }
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", headers));
        File.AppendAllText(_filePath, sb.ToString());
        _csvHeaderWritten = true;
    }

    private void WriteCurrentRow()
    {
        var values = new List<string> { Time.time.ToString("G17") }; // Full precision time

        foreach (var node in nodesToLog)
        {
            // Single API calls - no fallbacks, simple and fast
            var posef = OVRPlugin.GetNodePose(node, OVRPlugin.Step.Render);
            var velocity = OVRPlugin.GetNodeVelocity(node, OVRPlugin.Step.Render);
            var angularVelocity = OVRPlugin.GetNodeAngularVelocity(node, OVRPlugin.Step.Render);

            // Get tracking state flags for this node
            var nodePresent = OVRPlugin.GetNodePresent(node);
            var positionTracked = OVRPlugin.GetNodePositionTracked(node);
            var orientationTracked = OVRPlugin.GetNodeOrientationTracked(node);
            var positionValid = OVRPlugin.GetNodePositionValid(node);
            var orientationValid = OVRPlugin.GetNodeOrientationValid(node);

            // Convert to Unity coordinate system
            var position = posef.Position.FromFlippedZVector3f();
            var rotation = posef.Orientation.FromFlippedZQuatf();
            var vel = velocity.FromFlippedZVector3f();
            var angVel = angularVelocity.FromFlippedZVector3f();

            // Write tracking state flags (as 1/0 for true/false)
            values.Add(nodePresent ? "1" : "0");
            values.Add(positionTracked ? "1" : "0");
            values.Add(orientationTracked ? "1" : "0");
            values.Add(positionValid ? "1" : "0");
            values.Add(orientationValid ? "1" : "0");

            // Write position data with full precision (G17)
            values.Add(position.x.ToString("G17"));
            values.Add(position.y.ToString("G17"));
            values.Add(position.z.ToString("G17"));

            // Write rotation data with regular precision (default ToString)
            values.Add(rotation.x.ToString());
            values.Add(rotation.y.ToString());
            values.Add(rotation.z.ToString());
            values.Add(rotation.w.ToString());

            // Write velocity data with full precision (G17)
            values.Add(vel.x.ToString("G17"));
            values.Add(vel.y.ToString("G17"));
            values.Add(vel.z.ToString("G17"));
            values.Add(angVel.x.ToString("G17"));
            values.Add(angVel.y.ToString("G17"));
            values.Add(angVel.z.ToString("G17"));
        }

        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", values));
        File.AppendAllText(_filePath, sb.ToString());
    }
}
