using UnityEngine;

public sealed class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }

    [SerializeField] private bool _autoInitialize;
    [SerializeField] private View[] _views;
    [SerializeField] private View _defaultView;

    private View _currentView;


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
            _currentView = _defaultView;
        }
    }

    public void Show<T>(string args = null) where T : View
    {
        foreach (View view in _views)
        {
            if (view is T)
            {
                view.Show(args);
                _currentView = view;
            }
            else
            {
                view.Hide();
            }
        }
    }

    public void Show(View view, object args = null)
    {
        if (_currentView != null)
        {
            _currentView.Hide();
        }

        view.Show(args);
        _currentView = view;
    }
}
