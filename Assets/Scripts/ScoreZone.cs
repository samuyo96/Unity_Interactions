using TMPro;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    public TMP_Text scoreText;

    [SerializeField] private int remaining = 0;

    private void Awake()
    {
        scoreText.text = remaining.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Target"))
        {
            return;
        }
        remaining--;
        scoreText.text = remaining.ToString();
        Destroy(other.gameObject);
    }

#if UNITY_EDITOR

    private void SetScore()
    {
        remaining = GameObject.FindGameObjectsWithTag("Target").Length;
    }

    [UnityEditor.CustomEditor(typeof(ScoreZone))]
    private class ScoreZoneEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ScoreZone scoreZone = (ScoreZone)target;
            base.OnInspectorGUI();
            if (GUILayout.Button("ReadElements"))
            {
                scoreZone.SetScore();
            }
        }
    }

#endif
}
