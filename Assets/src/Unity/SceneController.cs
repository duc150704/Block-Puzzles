using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    public enum EScene
    {
        MenuScene,
        PlayScene
    }

    [SerializeField] private SquareTransition _squareTransition;

    private bool _isLoading;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public async UniTask LoadSceneAsync(EScene scene)
    {
        if (_isLoading)
            return;
        _isLoading = true;

        await _squareTransition.TransitionIn();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        if (asyncOperation == null)
        {
            Debug.Log("Khong tai duoc: " + scene.ToString());
            return;
        }

        while (asyncOperation.progress < 0.9f)
        {
            await UniTask.Yield();
        }

        //asyncOperation.allowSceneActivation = true;
        await _squareTransition.TransitionOut();

        _isLoading = false;
    }
}
