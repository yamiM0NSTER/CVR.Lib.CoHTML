using ABI_RC.Core.Player;
using cohtml;
using cohtml.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVR.Lib.CoHTML {
  public class CoHTMLWrapper {
    GameObject _gameObject = null;
    Transform _transform = null;
    Animator _animator = null;
    CohtmlView _cohtmlView = null;
    View _view = null;
    int _width;
    int _height;

    public bool Visible {
      get {
        return _animator.GetBool("Open");
      }
      set {
        _animator.SetBool("Open", value);
      }
    }

    public View View => _view;
    public CohtmlView CohtmlView => _cohtmlView;
    public Transform Transform => _transform;


    static Animator _originalAnimator = null;

    static bool ApplicationStarted { get; set; }
    static Animator OriginalAnimator { 
      get {
        if(_originalAnimator is not null)
          return _originalAnimator;

        var gameObject = GameObject.Find("Cohtml/CohtmlWorldView");
        _originalAnimator = gameObject.GetComponent<Animator>();

        return _originalAnimator;
      }
    }

    // Attach to existing cohtml object
    public CoHTMLWrapper(GameObject gameObject) {
      if(!ApplicationStarted)
        throw new Exception("Unity is not ready yet");
      _gameObject = gameObject;
      _transform = gameObject.transform;
      _animator = gameObject.GetComponent<Animator>();
      _cohtmlView = gameObject.GetComponent<CohtmlView>();
      _width = _cohtmlView.Width;
      _height = _cohtmlView.Height;
    }

    // Create custom cohtml object
    public CoHTMLWrapper(CoHTMLCreationInfo creationInfo) {
      if(!ApplicationStarted)
        throw new Exception("Unity is not ready yet");
      _gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
      _gameObject.hideFlags = HideFlags.HideAndDontSave;
      _transform = _gameObject.transform;

      if(creationInfo.Parent is not null)
        _transform.SetParent(creationInfo.Parent);

      _animator = _gameObject.AddComponent<Animator>();
      _animator.runtimeAnimatorController = OriginalAnimator.runtimeAnimatorController;

      _cohtmlView = _gameObject.AddComponent<CohtmlView>();
      _cohtmlView.Width = creationInfo.Width;
      _cohtmlView.Height = creationInfo.Height;
      _cohtmlView.Page = creationInfo.URL;

      _width = creationInfo.Width;
      _height = creationInfo.Height;
      SetScale(creationInfo.Scale);

      //Transform rotationPivot = PlayerSetup.Instance._movementSystem.rotationPivot;
      //_transform.eulerAngles = new Vector3(rotationPivot.eulerAngles.x, rotationPivot.eulerAngles.y, rotationPivot.eulerAngles.z);
      //_transform.position = rotationPivot.position + rotationPivot.forward * 1f;

      //Visible = true;
    }

    public static void OnApplicationStarted() {
      ApplicationStarted = true;
    }

    void SetScale(float scale) {
      if(_width == _height)
        _transform.localScale = new Vector3(scale, scale, scale);
      else
        _transform.localScale = new Vector3((float)_width / (float)_height * scale, scale, scale);
    }

    public void Reload() {
      _cohtmlView.View.Reload();
    }
  }

  public struct CoHTMLCreationInfo {
    public CoHTMLCreationInfo() {
    }

    public string URL { get; set; } = "https://www.google.com";
    public int Width { get; set; } = 1920;
    public int Height { get; set; } = 1080;
    public float Scale { get; set; } = 1.0f;
    public Transform Parent { get; set; } = null;
  }
}
