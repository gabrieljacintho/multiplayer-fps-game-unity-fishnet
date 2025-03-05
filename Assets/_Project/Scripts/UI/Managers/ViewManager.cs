using UnityEngine;

public sealed class ViewManager : MonoBehaviour
{
    [SerializeField] private bool _autoInitialize;
    [SerializeField] private View[] _views;
    [SerializeField] private View _defaultView;

    public static ViewManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_autoInitialize)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        foreach (View view in _views)
        {
            view.Initialize();
            view.Hide();
        }

        if (_defaultView != null)
        {
            _defaultView.Show();
        }
    }

    public void Show<T>(string args = null) where T : View
    {
        foreach (View view in _views)
        {
            if (view is T)
            {
                view.Show(args);
            }
            else
            {
                view.Hide();
            }
        }
    }
}
